using Bots;

public class StorageHandler
{
    private BotStorage _botHandler;
    private TasksStorage _tasksHandler;
    private Scanner _scanner;

    public StorageHandler(Scanner scaner)
    {
        _scanner = scaner;

        _botHandler = new BotStorage();
        _tasksHandler = new TasksStorage();

        _botHandler.BotFreed += CheakFreeTask;
        _scanner.ResourceFound += CheakFreeBot;
    }

    private void CheakFreeTask(Bot bot)
    {
        if (_tasksHandler.TryGetFreeTask(out IBotTarget result))
        {
            bot.SetTarget(result);
        }
        else
        {
            _botHandler.AddQueue(bot);
        }
    }

    private void CheakFreeBot(IBotTarget task)
    {
        if (_botHandler.TryGetFreeBot(out Bot bot))
        {
            bot.SetTarget(task);
        }
        else
        {
            _tasksHandler.AddQueue(task);
        }
    }
}
