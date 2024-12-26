using System;
using System.Collections.Generic;
using Bots;

public class BotStorage
{
    private List<Bot> _bots;
    private Queue<Bot> _freeBots;

    public Action<Bot> BotFreed;

    public BotStorage()
    {
        _bots = new List<Bot>();
        _freeBots = new Queue<Bot>();
    }

    public void SetBot(Bot bot)
    {
        bot.Freed += SayFree;
        _bots.Add(bot);
        AddQueue(bot);
    }

    private void SayFree(Bot bot)
    {
        BotFreed.Invoke(bot);
    }

    public bool TryGetFreeBot(out Bot bot)
    {
        bool haveFreeBot = _freeBots.Count > 0;
        bot = haveFreeBot ? _freeBots.Dequeue() : null;
        return haveFreeBot;
    }

    public void AddQueue(Bot bot)
    {
        _freeBots.Enqueue(bot);
    }
}


