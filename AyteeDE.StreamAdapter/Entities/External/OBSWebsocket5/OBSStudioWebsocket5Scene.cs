using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Entities.External;

public class OBSStudioWebsocket5Scene : Scene, IEquatable<OBSStudioWebsocket5Scene>
{
    public override string Name { get ; set; } = String.Empty;
    public override string UniqueIdentifier => Name;
    public bool Equals(OBSStudioWebsocket5Scene? other)
    {
        if(other != null)
        {
            return this.Name == other.Name;
        }

        return false;
    }
}
