using System.Text.Json;
using System.Text.Json.Serialization;
using AyteeDE.StreamAdapter.Communication.Websocket;

namespace AyteeDE.StreamAdapter;

public class OBSStudioWebsocket5Message : WebsocketMessage
{
    public int Op { get; set; }
    public OBSStudioWebsocket5MessageData D { get; set; }
}
public class OBSStudioWebsocket5MessageData
{
    public string ObsWebsocketVersion { get; set; }
    public int? RpcVersion { get; set; }
    public int? NegotiatedRpcVersion { get; set; }
    public object Authentication { get; set; }
    [JsonIgnore]
    public string AuthenticationString
    {
        get
        {
            return Authentication.ToString();
        }
    }
    [JsonIgnore]
    public OBSStudioWebsocket5MessageAuthenticationData AuthenticationData
    {
        get
        {
            return JsonSerializer.Deserialize<OBSStudioWebsocket5MessageAuthenticationData>(AuthenticationString);
        }
    }
    public int? EventSubscriptions { get; set; }
    public int? EventIntent { get; set; }
    public string EventType { get; set; }
    public OBSStudioWebsocket5MessageEventData EventData { get; set; }
    public string RequestType { get; set; }
    public string RequestId { get; set; }
    public OBSStudioWebsocket5MessageRequestData RequestData { get; set; }
    public OBSStudioWebsocket5MessageRequestStatus RequestStatus { get; set; }
    public OBSStudioWebsocket5MessageResponseData ResponseData { get; set; }
}
public class OBSStudioWebsocket5MessageAuthenticationData
{
    public string Challenge {get;set;}
    public string Salt {get;set;}
}
public class OBSStudioWebsocket5MessageEventData
{
    public string SceneName {get;set;}
}
public class OBSStudioWebsocket5MessageRequestData
{
    public string SceneName {get;set;}
}
public class OBSStudioWebsocket5MessageRequestStatus
{
    public int? Code {get;set;}
    public bool? Result {get;set;}
}
public class OBSStudioWebsocket5MessageResponseData
{
    public string CurrentProgramSceneName {get;set;}
    public string CurrentPreviewSceneName {get;set;}
    public List<OBSStudioWebsocket5MessageScene> Scenes {get;set;}
}
public class OBSStudioWebsocket5MessageScene
{
    public int? SceneIndex {get;set;}
    public string SceneName {get;set;}
}
