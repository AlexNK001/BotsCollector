using UnityEngine;
using System;
using Bots;
using Flags;

public class Base : PoolObject, IResourceTaker
{
    [SerializeField] private Bot[] _bots;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Flag _flag;

    public Func<PoolObject, PoolObject> SummonedPoolObject;
    public Action<int> ResourceCountChanged;
    public Action<Resource> ResourceAccepted;

    private BotStorage _botHandler;
    private TasksStorage _tasksHandler;
    private Wallet _wallet;

    [SerializeField] private SummonedTaskSO _currentSummonedTask;

    private void Start()
    {
        _botHandler = new BotStorage();
        _tasksHandler = new TasksStorage();
        _wallet = new Wallet();

        for (int i = 0; i < _bots.Length; i++)
        {
            Bot currentBot = _bots[i];
            currentBot.SetBase(this);
            _botHandler.SetBot(currentBot);
        }

        _botHandler.BotFreed += CheakFreeTask;
        _scanner.ResourceFound += CheakFreeBot;
    }

    internal void SetBot(Bot bot)
    {
        _botHandler.SetBot(bot);
        bot.SetBase(this);
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

    public void Take(Resource resource)
    {
        ResourceAccepted?.Invoke(resource);
        ResourceCountChanged?.Invoke(_wallet.Add());

        if (_wallet.TrySpend(_currentSummonedTask))
        {
            switch (SummonedPoolObject.Invoke(_currentSummonedTask.PoolObject))
            {
                case Bot bot:
                    SetBot(bot);
                    break;

                case Base building:
                    _flag.SetBase(building);
                    break;
            }
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
