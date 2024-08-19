using System.Security.Principal;

namespace AyteeDE.StreamAdapter.Configuration;

public class EndpointConfiguration
{
    public ConnectionType ConnectionType { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? Token { get; set; }
    private bool _enableAuthentication;
    public bool EnableAuthentication
    {
        get => _enableAuthentication;
        set
        {
            if(!value)
            {
                Token = null;
            }
            _enableAuthentication = value;
        }
    }
}
