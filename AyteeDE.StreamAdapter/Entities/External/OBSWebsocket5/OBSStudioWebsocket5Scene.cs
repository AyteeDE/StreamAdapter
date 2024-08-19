using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Entities.External;

public class OBSStudioWebsocket5Scene : Scene, IEquatable<OBSStudioWebsocket5Scene>
{
    public string ID { get; init; }
    public override string Name { get ; set; }

    public bool Equals(OBSStudioWebsocket5Scene? other)
    {
        if(other != null)
        {
            return this.ID == other.ID;
        }

        return false;
    }
}
