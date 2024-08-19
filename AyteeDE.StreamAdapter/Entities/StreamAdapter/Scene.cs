namespace AyteeDE.StreamAdapter.Entities.StreamAdapter;

public abstract class Scene
{
    public abstract string Name { get; init; }
    public override string ToString()
    {
        return Name;
    }
}
