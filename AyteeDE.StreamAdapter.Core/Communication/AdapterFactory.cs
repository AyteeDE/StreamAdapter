using AyteeDE.StreamAdapter.Core.Configuration;

namespace AyteeDE.StreamAdapter.Core.Communication;

public static class AdapterFactory
{
    public static IStreamAdapter CreateInstance(EndpointConfiguration configuration) 
    {
        return (IStreamAdapter)Activator.CreateInstance(configuration.ConnectionType, configuration);
    }
}
