using System;

public class Flag : Target
{
    private Base _base;
    
    public Action ConstructionIsCompleted;

    public void Build(Bot bot)
    {
        _base.gameObject.SetActive(true);
        _base.SetBot(bot);
        _base.transform.position = transform.position;
        _base = null;
        ConstructionIsCompleted.Invoke();
    }

    public void SetBase(Base baseUnder—onstruction)
    {
        _base = baseUnder—onstruction;
        _base.gameObject.SetActive(false);
    }
}
