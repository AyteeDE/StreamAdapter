using System.Text.Json;
using System.Text.Json.Serialization;
using AyteeDE.StreamAdapter.Core.Communication;
using AyteeDE.StreamAdapter.Core.Communication.Websocket;

namespace AyteeDE.StreamAdapter.OBSStudioWebsocket5.Communication;

public class OBSStudioWebsocket5Message : WebsocketMessage
{
    [JsonPropertyName("op")]
    public int Op { get; set; }
    [JsonPropertyName("d")]
    public OBSStudioWebsocket5MessageData D { get; set; }
}
public class OBSStudioWebsocket5MessageData
{
    [JsonPropertyName("obsWebsocketVersion")]
    public string ObsWebsocketVersion { get; set; }
    [JsonPropertyName("rpcVersion")]
    public int? RpcVersion { get; set; }
    [JsonPropertyName("negotiatedRpcVersion")]
    public int? NegotiatedRpcVersion { get; set; }
    [JsonPropertyName("authentication")]
    public object Authentication { get; set; }
    [JsonIgnore]
    public string AuthenticationString
    {
        get
        {
            return Authentication == null ? String.Empty : Authentication.ToString();
        }
    }
    [JsonIgnore]
    public OBSStudioWebsocket5MessageAuthenticationData AuthenticationData
    {
        get
        {
            return AuthenticationString == String.Empty ? null : JsonSerializer.Deserialize<OBSStudioWebsocket5MessageAuthenticationData>(AuthenticationString, DefaultJsonSerializerOptions.Options);
        }
    }
    [JsonPropertyName("eventSubscriptions")]
    public int? EventSubscriptions { get; set; }
    [JsonPropertyName("eventIntent")]
    public int? EventIntent { get; set; }
    [JsonPropertyName("eventType")]
    public string EventType { get; set; }
    [JsonPropertyName("eventData")]
    public OBSStudioWebsocket5MessageEventData EventData { get; set; }
    [JsonPropertyName("requestType")]
    public string RequestType { get; set; }
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; }
    [JsonPropertyName("requestData")]
    public OBSStudioWebsocket5MessageRequestData RequestData { get; set; }
    [JsonPropertyName("requestStatus")]
    public OBSStudioWebsocket5MessageRequestStatus RequestStatus { get; set; }
    [JsonPropertyName("responseData")]
    public OBSStudioWebsocket5MessageResponseData ResponseData { get; set; }
}
public class OBSStudioWebsocket5MessageAuthenticationData
{
    [JsonPropertyName("challenge")]
    public string Challenge {get;set;}
    [JsonPropertyName("salt")]
    public string Salt {get;set;}
}
public class OBSStudioWebsocket5MessageEventData
{
    [JsonPropertyName("sceneName")]
    public string SceneName {get;set;}
}
public class OBSStudioWebsocket5MessageRequestData
{
    [JsonPropertyName("sceneName")]
    public string SceneName {get;set;}
    [JsonPropertyName("captionText")]
    public string CaptionText {get;set;}
}
public class OBSStudioWebsocket5MessageRequestStatus
{
    [JsonPropertyName("code")]
    public int? Code {get;set;}
    [JsonPropertyName("result")]
    public bool? Result {get;set;}
}
public class OBSStudioWebsocket5MessageResponseData
{
    [JsonPropertyName("currentProgramSceneName")]
    public string CurrentProgramSceneName {get;set;}
    [JsonPropertyName("currentPreviewSceneName")]
    public string CurrentPreviewSceneName {get;set;}
    [JsonPropertyName("scenes")]
    public List<OBSStudioWebsocket5MessageScene> Scenes {get;set;}
    [JsonPropertyName("outputActive")]
    public bool OutputActive {get;set;}
    [JsonPropertyName("outputPaused")]
    public bool OutputPaused {get;set;}
    [JsonPropertyName("outputReconnecting")]
    public bool OutputReconnecting {get;set;}
    [JsonPropertyName("outputTimecode")]
    public string OutputTimecode {get;set;}
    [JsonPropertyName("outputDuration")]
    public double OutputDuration {get;set;}
    [JsonPropertyName("outputCongestion")]
    public double OutputCongestion {get;set;}
    [JsonPropertyName("outputBytes")]
    public double OutputBytes {get;set;}
    [JsonPropertyName("outputSkippedFrames")]
    public int OutputSkippedFrames {get;set;}
    [JsonPropertyName("outputTotalFrames")]
    public int OutputTotalFrames {get;set;}
}
public class OBSStudioWebsocket5MessageScene
{
    [JsonPropertyName("sceneIndex")]
    public int? SceneIndex {get;set;}
    [JsonPropertyName("sceneName")]
    public string SceneName {get;set;}
}
