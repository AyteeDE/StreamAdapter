namespace AyteeDE.StreamAdapter.Entities.StreamAdapter;

public abstract class Scene
{
    public abstract string Name { get; set; }
    public abstract string UniqueIdentifier { get; }
    public override string ToString()
    {
        return Name;
    }
}
