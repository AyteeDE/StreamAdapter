using AyteeDE.StreamAdapter.Communication.Websocket;
using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Entities.External;

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
