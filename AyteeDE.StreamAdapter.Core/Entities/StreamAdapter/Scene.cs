namespace AyteeDE.StreamAdapter.Core.Entities;

public abstract class Scene : IEquatable<Scene>
{
    public abstract string Name { get; set; }
    public abstract string UniqueIdentifier { get; }

    public bool Equals(Scene? other)
    {
        if(other == null)
        {
            return false;
        }
        return this.UniqueIdentifier.Equals(other.UniqueIdentifier);
    }

    public override string ToString()
    {
        return Name;
    }
}
