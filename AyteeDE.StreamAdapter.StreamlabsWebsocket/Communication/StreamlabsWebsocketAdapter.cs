using System.Text.Json;
using AyteeDE.StreamAdapter.Core.Communication;
using AyteeDE.StreamAdapter.Core.Communication.Websocket;
using AyteeDE.StreamAdapter.Core.Configuration;
using AyteeDE.StreamAdapter.Core.Entities;
using AyteeDE.StreamAdapter.StreamlabsWebsocket.Communication.Websocket;
using AyteeDE.StreamAdapter.StreamlabsWebsocket.Entities;

namespace AyteeDE.StreamAdapter.StreamlabsWebsocket.Communication;

public class StreamlabsWebsocketAdapter : IStreamAdapter
{
    private readonly EndpointConfiguration _configuration;
    private static WebsocketConnection? _websocketConnection = new WebsocketConnection();
    private static bool _isIdentified;
    private List<StreamlabsWebsocketMessage> _receivedMessages = new List<StreamlabsWebsocketMessage>();
    private Dictionary<string, StreamlabsWebsocketMessage> _requestsAwaitingResponse = new Dictionary<string, StreamlabsWebsocketMessage>();
    private string Endpoint
    {
        get => $"ws://{_configuration.Host}:{_configuration.Port}/api/websocket";
    }
    public StreamlabsWebsocketAdapter(EndpointConfiguration configuration)
    {
        _configuration = configuration;
        _websocketConnection.OnMessageReceived += OnMessageReceived;
    }
    public async Task<bool> ConnectAsync()
    {
        var result = await _websocketConnection.ConnectAsync(Endpoint);
        await SubscribeEvents();

        return result;
    }
    private async Task SubscribeEvents()
    {
        await PrepareAndSendNonParameterMessage("sceneSwitched", "ScenesService");
    }
#region Outgoing Message Handling
    private async Task<StreamlabsWebsocketMessage> SendMessageAndWaitForResponse(StreamlabsWebsocketMessage message)
    {
        _requestsAwaitingResponse.Add(message.Id, null);
        await _websocketConnection.SendAsync(message);

        return await GetExpectedResponse(message);
    }
    private async Task<StreamlabsWebsocketMessage> PrepareAndSendNonParameterMessage(string method, string resource)
    {
        var message = await PrepareRequestMessage();
        if(message != null)
        {
            message.Method = method;
            message.Params = new StreamlabsWebsocketMessageParams()
            {
                Resource = resource
            };
            var response = await SendMessageAndWaitForResponse(message);

            return response;
        }

        return null;
    }
    private async Task<StreamlabsWebsocketMessage> PrepareRequestMessage()
    {
        if(await _websocketConnection.ConnectAsync(Endpoint))
        {
            if(!_isIdentified)
            {
                await Identify();
            }
            StreamlabsWebsocketMessage message = new StreamlabsWebsocketMessage();
            message.JsonRpc = "2.0";
            message.Id = Guid.NewGuid().ToString();

            return message;
        }
        else
        {
            return null;
        }
    }
    private async Task<bool> Identify()
    {
        if(await _websocketConnection.ConnectAsync(Endpoint))
        {
            StreamlabsWebsocketMessage identifyMessage = new StreamlabsWebsocketMessage();
            identifyMessage.JsonRpc = "2.0";
            identifyMessage.Id = Guid.NewGuid().ToString();
            identifyMessage.Method = "auth";
            identifyMessage.Params = new StreamlabsWebsocketMessageParams()
            {
                Resource = "TcpServerService",
                Args = _configuration.AuthenticationEnabled ? new List<string>()
                {
                    _configuration.Token
                } : new List<string>()
            };

            var result = await SendMessageAndWaitForResponse(identifyMessage);
            _isIdentified = result.Result.ToString() == "True" ? true : false;

            return true;
        }
        return false;
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
    private async void HandleReceivedMessage(string message)
    {
        var messageObj = JsonSerializer.Deserialize<StreamlabsWebsocketMessage>(message, DefaultJsonSerializerOptions.Options);

        if(messageObj.Id != null && _requestsAwaitingResponse.ContainsKey(messageObj.Id))
        {
            MapExpectedResponse(messageObj);
            return;
        }

        if(JsonSerializer.Deserialize<StreamlabsWebsocketMessageResult>(messageObj.Result.ToString(), DefaultJsonSerializerOptions.Options).Type == "EVENT")
        {
            HandleEvents(messageObj);
            return;
        }
    }
    private void HandleEvents(StreamlabsWebsocketMessage message)
    {
        var result = JsonSerializer.Deserialize<StreamlabsWebsocketMessageResult>(message.Result.ToString(), DefaultJsonSerializerOptions.Options);

        switch(result.ResourceId)
        {
            case "ScenesService.sceneSwitched":
                var resultScene = JsonSerializer.Deserialize<StreamlabsWebsocketMessageResult>(result.Data.ToString(), DefaultJsonSerializerOptions.Options);
                SubscribedEventHandler.InvokeSubscribedEvent(OnCurrentProgramSceneChanged, this, new StreamlabsScene(resultScene.Name, resultScene.Id));
                break;
            default:
                break;
        }
    }
    private void MapExpectedResponse(StreamlabsWebsocketMessage message)
    {
        if(_requestsAwaitingResponse.ContainsKey(message.Id))
        {
            _requestsAwaitingResponse[message.Id] = message;
        }
    }
    private async Task<StreamlabsWebsocketMessage> GetExpectedResponse(StreamlabsWebsocketMessage requestMessage)
    {
        if(_requestsAwaitingResponse.ContainsKey(requestMessage.Id))
        {
            while(_requestsAwaitingResponse[requestMessage.Id] == null)
            {
                //Delay polling by 50ms
                await Task.Delay(25);
            }
            
            var result = _requestsAwaitingResponse[requestMessage.Id];
            _requestsAwaitingResponse.Remove(requestMessage.Id);
            return result;
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
        var response = await PrepareAndSendNonParameterMessage("activeScene", "ScenesService");

        if(response == null)
        {
            return null;
        }
        var result = JsonSerializer.Deserialize<StreamlabsWebsocketMessageResult>(response.Result.ToString(), DefaultJsonSerializerOptions.Options);

        return new StreamlabsScene(result.Name, result.Id);
    }
    public async Task<List<Scene>> GetScenes()
    {
        var response = await PrepareAndSendNonParameterMessage("getScenes", "ScenesService");

        if(response == null)
        {
            return null;
        }

        List<Scene> scenes= new List<Scene>();
        foreach(var respScene in JsonSerializer.Deserialize<List<StreamlabsWebsocketMessageResult>>(response.Result.ToString(), DefaultJsonSerializerOptions.Options))
        {
            StreamlabsScene scene = new StreamlabsScene(respScene.Name, respScene.Id);
            scenes.Add(scene);
        }

        return scenes;
    }
    public async Task<bool> SetCurrentProgramScene(Scene scene)
    {
        var message = await PrepareRequestMessage();
        message.Method = "makeSceneActive";
        message.Params = new StreamlabsWebsocketMessageParams()
        {
            Resource = "ScenesService",
            Args = new List<string>() { scene.UniqueIdentifier }
        };

        var response = await SendMessageAndWaitForResponse(message);

        return true;
    }
    public event EventHandler<Scene> OnCurrentProgramSceneChanged;
#endregion
}
