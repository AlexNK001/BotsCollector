using System;
using System.Collections.Generic;

public class TasksStorage
{
    private Queue<IBotTarget> _tasks;
    public Action TaskAdded;

    public TasksStorage()
    {
        _tasks = new Queue<IBotTarget>();
    }

    public bool TryGetFreeTask(out IBotTarget task)
    {
        bool isTry = _tasks.Count > 0;
        task = isTry ? _tasks.Dequeue() : null;
        return isTry;
    }

    public void AddQueue(IBotTarget spawnPoint)
    {
        _tasks.Enqueue(spawnPoint);
    }
}


