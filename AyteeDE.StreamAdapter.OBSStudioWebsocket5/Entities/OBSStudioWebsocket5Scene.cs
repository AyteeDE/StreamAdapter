using AyteeDE.StreamAdapter.Entities.StreamAdapter;
using AyteeDE.StreamAdapter.OBSStudioWebsocket5.Communication;

namespace AyteeDE.StreamAdapter.OBSStudioWebsocket5.Entities;

public class OBSStudioWebsocket5Scene : Scene
{
    public override string Name { get ; set; } = String.Empty;
    public override string UniqueIdentifier => Name;
    public OBSStudioWebsocket5Scene(string name)
    {
        Name = name;
    }
    public OBSStudioWebsocket5Scene(OBSStudioWebsocket5MessageScene scene)
    {
        Name = scene.SceneName;
    }
}
