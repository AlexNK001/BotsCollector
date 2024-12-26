using System;

public sealed class SpawnPoint : SingleStorage
{
    public event Action<SpawnPoint> Freed;

    public override bool TryGet(out Resource resource)
    {
        bool isFreed = base.TryGet(out resource);

        if (isFreed)
            Freed.Invoke(this);

        return isFreed;
    }
}

