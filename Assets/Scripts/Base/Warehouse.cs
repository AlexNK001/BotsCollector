using System;

public class Warehouse : Target
{
    private int _resourceCount;

    public Action<int> ResourceCountChanged;
    public Action<Resource> ResourceAccepteed;

    public bool TrySpend(PoolTask poolTask)
    {
        bool canSpend = _resourceCount >= poolTask.Price;

        if (canSpend)
        {
            _resourceCount -= poolTask.Price;
            ResourceCountChanged?.Invoke(_resourceCount);
        }

        return canSpend;
    }

    public void Take(Resource resourceGiven)
    {
        ResourceCountChanged?.Invoke(++_resourceCount);
        ResourceAccepteed.Invoke(resourceGiven);
    }
}
