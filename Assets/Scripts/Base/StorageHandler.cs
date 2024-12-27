public class StorageHandler : IUnsubscriber
{
    private readonly BotStorage _botStorage;
    private readonly TargetStorage _targetStorage;
    private readonly Scanner _scanner;

    public int BotCount => _botStorage.Count;

    public StorageHandler(Scanner scaner)
    {
        _scanner = scaner;

        _botStorage = new BotStorage();
        _targetStorage = new TargetStorage();

        _botStorage.BotFreed += OnCheсkTask;
        _scanner.ResourceFound += OnCheсkFreeBot;
    }

    public void EnqueueTarget(Target task)
    {
        _targetStorage.Enqueue(task);
    }

    public void SetBot(Bot bot)
    {
        _botStorage.SetBot(bot);
    }

    public void Unsubscribe()
    {
        _botStorage.BotFreed -= OnCheсkTask;
        _scanner.ResourceFound -= OnCheсkFreeBot;
        _botStorage.Unsubscribe();
    }

    private void OnCheсkTask(Bot bot)
    {
        if (_targetStorage.TryGetTarget(out Target task))
            bot.SetTarget(task);
        else
            _botStorage.EnqueueBot(bot);
    }

    private void OnCheсkFreeBot(Target task)
    {
        if (_botStorage.TryGetFreeBot(out Bot bot))
            bot.SetTarget(task);
        else
            _targetStorage.Enqueue(task);
    }
}
