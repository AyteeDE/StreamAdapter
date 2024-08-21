using System;
using AyteeDE.StreamAdapter.Entities.StreamAdapter;

namespace AyteeDE.StreamAdapter.Entities.External;

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
