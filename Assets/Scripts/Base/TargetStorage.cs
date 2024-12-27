using System.Collections.Generic;

public class TargetStorage 
{
    private Queue<Target> _tasks;

    public TargetStorage()
    {
        _tasks = new Queue<Target>();
    }

    public void Enqueue(Target task)
    {
        if (task is Flag)
        {
            Queue<Target> newQue = new();
            newQue.Enqueue(task);

            foreach (Target item in _tasks)
                newQue.Enqueue(item);

            _tasks = newQue;
        }

        if (_tasks.Contains(task) == false)
            _tasks.Enqueue(task);
    }

    public bool TryGetTarget(out Target task)
    {
        return _tasks.TryDequeue(out task);
    }
}
