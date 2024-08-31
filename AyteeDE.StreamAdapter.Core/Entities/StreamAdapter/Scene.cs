using System.Text.Json.Serialization;

namespace AyteeDE.StreamAdapter.Core.Entities;

public class Scene : IEquatable<Scene>
{
    [JsonInclude]
    public string Name { get; protected set; }
    [JsonInclude]
    public string UniqueIdentifier { get; protected set; }
    public bool Equals(Scene? other)
    {
        if(other == null)
        {
            return false;
        }
        return this.UniqueIdentifier.Equals(other.UniqueIdentifier);
    }

    public override string ToString()
    {
        return Name;
    }
}
