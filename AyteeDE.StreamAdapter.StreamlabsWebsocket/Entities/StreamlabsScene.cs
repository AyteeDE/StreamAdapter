using AyteeDE.StreamAdapter.Core.Entities;

namespace AyteeDE.StreamAdapter.StreamlabsWebsocket.Entities;

public class StreamlabsScene : Scene
{
    public string Id { get; set; }
    public override string Name { get; set; }
    public override string UniqueIdentifier => Id;
    public StreamlabsScene(string name, string id)
    {
        Name = name;
        Id = id;
    }
}
