using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace AyteeDE.StreamAdapter.Communication.Websocket;

public class WebsocketConnection
{
    private static ClientWebSocket _ws = new ClientWebSocket();
    public event EventHandler<WebsocketMessageEventArgs> MessageReceived;
    private bool IsConnected
    {
        get
        {
            return _ws.State == WebSocketState.Open ? true : false;
        }
    }
    public async Task<bool> ConnectAsync(string endpoint)
    {
        if(!IsConnected)
        {
            Uri uri = BuildUri(endpoint);
            try
            {
                await _ws.ConnectAsync(uri, CancellationToken.None);
                ListenAsync();
            }
            catch
            {
                return false;
            }
        }

        return IsConnected;
    }
    public async Task<bool> DisconnectAsync()
    {
        if(IsConnected)
        {
            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing", CancellationToken.None);
            return true;
        }
        return false;
    }
    public async Task SendAsync(string message)
    {
        var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
        if(IsConnected)
        {
            await _ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        else
        {
            throw new Exception("Connection closed.");
        }
    }
    public async Task SendAsync(WebsocketMessage message)
    {
        var json = JsonSerializer.Serialize(message, message.GetType(), DefaultJsonSerializerOptions.Options);
        await SendAsync(json);
    }
    public async Task ListenAsync()
    {
        var receiveBuffer = new ArraySegment<byte>(new byte[4096]);
        while(IsConnected)
        {
            var result = await _ws.ReceiveAsync(receiveBuffer, CancellationToken.None);
            if(result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(receiveBuffer.Array, 0, result.Count);
                HandleResponseMessage(message);
            }
        }    
    }
    private void HandleResponseMessage(string message)
    {
        MessageReceived?.Invoke(this, new WebsocketMessageEventArgs(message));
    }
    private Uri BuildUri(string endpoint)
    {
        return new Uri(endpoint);
    }
}

public class WebsocketMessageEventArgs : EventArgs
{
    public string Message { get; private set; }
    public WebsocketMessageEventArgs(string message)
    {
        Message = message;
    }
}