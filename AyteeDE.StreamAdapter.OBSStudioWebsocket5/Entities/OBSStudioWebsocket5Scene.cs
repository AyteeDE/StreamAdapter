using AyteeDE.StreamAdapter.Core.Entities;
using AyteeDE.StreamAdapter.OBSStudioWebsocket5.Communication;

namespace AyteeDE.StreamAdapter.OBSStudioWebsocket5.Entities;

public class OBSStudioWebsocket5Scene : Scene
{
    public OBSStudioWebsocket5Scene(){}
    public OBSStudioWebsocket5Scene(string name)
    {
        Name = name;
        UniqueIdentifier = name;
    }
    public OBSStudioWebsocket5Scene(OBSStudioWebsocket5MessageScene scene)
    {
        Name = scene.SceneName;
        UniqueIdentifier = scene.SceneName;
    }
}
