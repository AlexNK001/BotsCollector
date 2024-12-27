using System;
using System.Collections.Generic;

public class BotStorage : IUnsubscriber
{
    private readonly List<Bot> _bots;
    private readonly Queue<Bot> _freeBots;

    public BotStorage()
    {
        _bots = new List<Bot>();
        _freeBots = new Queue<Bot>();
    }

    public Action<Bot> BotFreed;

    public int Count => _bots.Count;

    public void SetBot(Bot bot)
    {
        bot.Freed += OnReportFreeBot;
        bot.BaseChanged += OnRemoveBot;

        _bots.Add(bot);
        EnqueueBot(bot);
    }

    public bool TryGetFreeBot(out Bot bot)
    {
        bool haveFreeBot = _freeBots.Count > 0;
        bot = haveFreeBot ? _freeBots.Dequeue() : null;
        return haveFreeBot;
    }

    public void EnqueueBot(Bot bot)
    {
        _freeBots.Enqueue(bot);
    }

    public void Unsubscribe()
    {
        foreach (Bot bot in _bots)
            UnsubscribeBot(bot);
    }

    private void OnRemoveBot(Bot bot)
    {
        _bots.Remove(bot);
        UnsubscribeBot(bot);
    }

    private void OnReportFreeBot(Bot bot)
    {
        BotFreed.Invoke(bot);
    }

    private void UnsubscribeBot(Bot bot)
    {
        bot.Freed -= OnReportFreeBot;
        bot.BaseChanged -= OnRemoveBot;
    }
}
