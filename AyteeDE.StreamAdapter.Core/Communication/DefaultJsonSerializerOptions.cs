using System.Text.Json;

namespace AyteeDE.StreamAdapter.Core.Communication;

public static class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Options
    {
        get
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            options.PropertyNameCaseInsensitive = true;
            return options;
        }
    }
}
