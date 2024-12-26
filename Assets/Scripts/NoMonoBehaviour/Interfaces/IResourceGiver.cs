public interface IResourceGiver:IBotTarget
{
    public abstract bool TryGet(out Resource resource);
}

