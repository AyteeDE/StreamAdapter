using AyteeDE.StreamAdapter.Core.Communication;

namespace AyteeDE.StreamAdapter.Core.Configuration;

public class EndpointConfiguration 
{
    private Type _connectionType;
    public Type ConnectionType
    { 
        get => _connectionType;
        set
        {
            if(value.IsAssignableTo(typeof(IStreamAdapter)))
            {
                _connectionType = value;
            }
            else
            {
                throw new Exception("Connection type must implement the IStreamAdapter interface.");
            }
        }
    }
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
