using AyteeDE.StreamAdapter.Communication.Websocket;
using AyteeDE.StreamAdapter.Configuration;

namespace AyteeDE.StreamAdapter.Communication;

public static class RequestFactory
{
    public static IRequest BuildRequest(EndpointConfiguration configuration)
    {
        switch(configuration.ConnectionType)
        {
            case ConnectionType.OBSStudioWebsocket5:
                return new OBSStudioWebsocket5Request(configuration);
            case ConnectionType.StreamlabsWebsocket:
                return new StreamlabsWebsocketRequest(configuration);
            default:
                return null;
        }
    }
}
