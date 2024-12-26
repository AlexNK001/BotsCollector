public class Wallet
{
    private int _resourceCount;

    public Wallet(int startingResourceCount = 0)
    {
        _resourceCount = startingResourceCount;
    }

    public int Add()
    {
        _resourceCount++;
        return _resourceCount;
    }

    public bool TrySpend(SummonedTaskSO buildingTask)
    {
        bool haveSpend = _resourceCount >= buildingTask.Price;

        if (haveSpend)
            _resourceCount -= buildingTask.Price;

        return haveSpend;
    }
}
