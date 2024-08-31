using AyteeDE.StreamAdapter.Core.Entities;

namespace AyteeDE.StreamAdapter.StreamlabsWebsocket.Entities;

public class StreamlabsScene : Scene
{
    public StreamlabsScene(string name, string id)
    {
        Name = name;
        UniqueIdentifier = id;
    }
}
