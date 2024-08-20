using AyteeDE.StreamAdapter.Communication;
using AyteeDE.StreamAdapter.Communication.Websocket;
using AyteeDE.StreamAdapter.Configuration;
using AyteeDE.StreamAdapter.Entities.External;

namespace AyteeDE.StreamAdapter.Communication;

public static class RequestFactory
{
    public static IRequest BuildRequest(EndpointConfiguration configuration)
    {
        switch(configuration.ConnectionType)
        {
            case ConnectionType.OBSStudioWebsocket5:
                return new OBSStudioWebsocket5Request(configuration);
            default:
                return null;
        }
    }
}
