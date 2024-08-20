namespace AyteeDE.StreamAdapter.Configuration;

public class EndpointConfiguration
{
    public ConnectionType ConnectionType { get; set; }
    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? Token { get; set; }
    private bool _authenticationEnabled;
    public bool AuthenticationEnabled
    {
        get => _authenticationEnabled;
        set
        {
            if(!value)
            {
                Token = null;
            }
            _authenticationEnabled = value;
        }
    }
}
