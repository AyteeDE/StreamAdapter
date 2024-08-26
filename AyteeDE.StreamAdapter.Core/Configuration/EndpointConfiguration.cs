using System.Reflection;
using System.Text.Json.Serialization;
using AyteeDE.StreamAdapter.Core.Communication;

namespace AyteeDE.StreamAdapter.Core.Configuration;

public class EndpointConfiguration 
{
    [JsonIgnore]
    public Type ConnectionType
    { 
        get
        {
            var assembly = Assembly.Load(ConnectionTypeAssemblyName);
            var type = assembly.GetType(ConnectionTypeName);
            return type;
        }
        set
        {
            if(value.IsAssignableTo(typeof(IStreamAdapter)))
            {
                ConnectionTypeName = value.FullName;
                ConnectionTypeAssemblyName = value.Assembly.GetName().Name;
            }
            else
            {
                throw new Exception("Connection type must implement the IStreamAdapter interface.");
            }
        }
    }
    [JsonInclude]
    public string ConnectionTypeName { get; private set; }
    [JsonInclude]
    public string ConnectionTypeAssemblyName { get; private set; }
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
