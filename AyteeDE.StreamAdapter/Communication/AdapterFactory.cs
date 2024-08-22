using AyteeDE.StreamAdapter.Communication.Websocket;
using AyteeDE.StreamAdapter.Configuration;

namespace AyteeDE.StreamAdapter.Communication;

public static class AdapterFactory
{
    public static IStreamAdapter CreateInstance(EndpointConfiguration configuration) 
    {
        return (IStreamAdapter)Activator.CreateInstance(configuration.ConnectionType, configuration);
    }
}
