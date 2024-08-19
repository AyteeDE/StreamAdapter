using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AyteeDE.StreamAdapter.Communication;
using AyteeDE.StreamAdapter.Communication.Websocket;
using AyteeDE.StreamAdapter.Configuration;
using AyteeDE.StreamAdapter.Entities.External;
using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Communication.Websocket;

public class OBSStudioWebsocket5Request : IRequest
{
    private readonly EndpointConfiguration _configuration;
    private static WebsocketConnection? _websocketConnection = new WebsocketConnection();
    private static bool _isIdentified;
    private List<OBSStudioWebsocket5Message> _receivedMessages = new List<OBSStudioWebsocket5Message>();
    private Dictionary<string, OBSStudioWebsocket5Message> _requestsAwaitingResponse = new Dictionary<string, OBSStudioWebsocket5Message>();
    private string Endpoint
    {
        get => $"ws://{_configuration.Host}:{_configuration.Port}";
    }
    public OBSStudioWebsocket5Request(EndpointConfiguration configuration)
    {
        _configuration = configuration;
        _websocketConnection.MessageReceived += OnMessageReceived;  
    }
#region Outgoing Message Handling
    private async Task<OBSStudioWebsocket5Message> SendMessageAndWaitForResponse(OBSStudioWebsocket5Message message)
    {
        _requestsAwaitingResponse.Add(message.D.RequestId, null);
        await _websocketConnection.SendAsync(message);

        return await GetExpectedResponse(message);
    }
    private async Task<OBSStudioWebsocket5Message> PrepareAndSendNonParameterMessage(string requestType)
    {
        var message = await PrepareRequestMessage();
        if(message != null)
        {
            message.D.RequestType = requestType;
            var response = await SendMessageAndWaitForResponse(message);

            return response;
        }

        return null;
    }
    private async Task<OBSStudioWebsocket5Message> PrepareRequestMessage()
    {
        if (await _websocketConnection.ConnectAsync(Endpoint))
        {
            if(!_isIdentified)
            {
                var helloMessage = await GetHelloMessage();

                string challenge = helloMessage.D.AuthenticationData.Challenge;
                string salt = helloMessage.D.AuthenticationData.Salt;

                await Identify(challenge, salt);
            }

            OBSStudioWebsocket5Message message = new OBSStudioWebsocket5Message();
            message.Op = 6;
            message.D = new OBSStudioWebsocket5MessageData();
            message.D.RequestId = Guid.NewGuid().ToString();

            return message;
        }
        else
        {
            return null;
        }
    }
    private async Task<bool> Identify(string challenge, string salt)
    {
        OBSStudioWebsocket5Message message = new OBSStudioWebsocket5Message();
        message.Op = 1;
        message.D = new OBSStudioWebsocket5MessageData();
        message.D.RpcVersion = 1;

        if(_configuration.AuthenticationEnabled)
        {
            message.D.Authentication = BuildAuthenticationString(challenge, salt);
        }

        await _websocketConnection.SendAsync(message);
        return true;
    }
    private async Task<OBSStudioWebsocket5Message> GetHelloMessage()
    {
        while(_receivedMessages.FirstOrDefault(m=> m.Op == 0) == null)
        {
            //Delay polling by 50ms
            await Task.Delay(25);
        }

        var helloMessage = _receivedMessages.FirstOrDefault(m => m.Op == 0);
        _receivedMessages.Remove(helloMessage);
        return helloMessage;
    }
    private string BuildAuthenticationString(string challenge, string salt)
    {
        IEnumerable<char> saltConcat = String.Empty;
        if(_configuration != null && _configuration.Token != null)
        {
            saltConcat = _configuration.Token.Concat(salt);
        }
        var secret = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(saltConcat.ToArray())));
        var secretConcat = secret.Concat(challenge);
        var authString = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(secretConcat.ToArray())));
        return authString;
    }
#endregion
#region Incoming Message Handling
    private void OnMessageReceived(object? sender, WebsocketMessageEventArgs e)
    {
        if(e.Message != null)
        {
            HandleReceivedMessage(e.Message);
        }
    }
    private void HandleReceivedMessage(string message)
    {
        var messageObj = JsonSerializer.Deserialize<OBSStudioWebsocket5Message>(message, DefaultJsonSerializerOptions.Options);

        switch (messageObj.Op)
        {
            case 2:
                _isIdentified = true;
                break;
            case 5:
                HandleEvents(messageObj);
                break;
            case 7:
                MapExpectedResponse(messageObj);
                break;
            default:
                _receivedMessages.Add(messageObj);
                break;
        }
    }
    private void HandleEvents(OBSStudioWebsocket5Message message)
    {
        switch(message.D.EventType)
        {
            default:
                break;
        }
    }
    private void MapExpectedResponse(OBSStudioWebsocket5Message message)
    {
        if(_requestsAwaitingResponse.ContainsKey(message.D.RequestId))
        {
            _requestsAwaitingResponse[message.D.RequestId] = message;
        }
    }
    private async Task<OBSStudioWebsocket5Message> GetExpectedResponse(OBSStudioWebsocket5Message requestMessage)
    {
        if(_requestsAwaitingResponse.ContainsKey(requestMessage.D.RequestId))
        {
            while(_requestsAwaitingResponse[requestMessage.D.RequestId] == null)
            {
                //Delay polling by 50ms
                await Task.Delay(25);
            }
            return _requestsAwaitingResponse[requestMessage.D.RequestId];
        }
        else
        {
            throw new Exception("Request missing in AwaitingResponse-Dictionary.");
        }
    }
#endregion
#region Interface Implementation
    public async Task<Scene> GetCurrentProgramScene()
    {
        var response = await PrepareAndSendNonParameterMessage("GetCurrentProgramScene");
        if(response == null)
        {
            return null;
        }

        OBSStudioWebsocket5Scene scene = new OBSStudioWebsocket5Scene(response.D.ResponseData.CurrentProgramSceneName);
        return scene;
    }

    public async Task<List<Scene>> GetScenes()
    {
        var response = await PrepareAndSendNonParameterMessage("GetSceneList");
        if(response == null)
        {
            return null;
        }

        List<Scene> scenes = new List<Scene>();
        foreach(var respScene in response.D.ResponseData.Scenes)
        {
            OBSStudioWebsocket5Scene scene = new OBSStudioWebsocket5Scene(respScene);
            scenes.Add(scene);
        }
        return scenes;
    }

    public async Task<bool> SetCurrentProgramScene(Scene scene)
    {
        var message = await PrepareRequestMessage();
        message.D.RequestType = "SetCurrentProgramScene";
        message.D.RequestData = new OBSStudioWebsocket5MessageRequestData
        {
            SceneName = scene.UniqueIdentifier
        };

        var response = await SendMessageAndWaitForResponse(message);

        if(response.D.RequestStatus.Result == true)
        {
            return true;
        }
        return false;
    }
#endregion
}
