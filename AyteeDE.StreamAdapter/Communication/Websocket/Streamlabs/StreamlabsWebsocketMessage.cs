using System.Text.Json.Serialization;

namespace AyteeDE.StreamAdapter.Communication.Websocket;

public class StreamlabsWebsocketMessage : WebsocketMessage
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc {get;set;}
    [JsonPropertyName("id")]
    public string Id {get;set;}
    [JsonPropertyName("method")]
    public string Method {get;set;}
    [JsonPropertyName("params")]
    public StreamlabsWebsocketMessageParams Params {get;set;}
    [JsonPropertyName("result")]
    public object Result {get;set;}
}
public class StreamlabsWebsocketMessageParams
{
    [JsonPropertyName("resource")]
    public string Resource {get;set;}
    [JsonPropertyName("args")]
    public List<string> Args {get;set;}
}
public class StreamlabsWebsocketMessageResult
{
    [JsonPropertyName("_type")]
    public string Type {get;set;}
    [JsonPropertyName("id")]
    public string Id {get;set;}
    [JsonPropertyName("resourceId")]
    public string ResourceId {get;set;}
    [JsonPropertyName("name")]
    public string Name {get;set;}
    [JsonPropertyName("data")]
    public object Data {get;set;}
}