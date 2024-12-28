using System;

public class Flag : Target
{
    private Base _base;
    
    public Action ConstructionIsCompleted;

    public void Build(Bot bot)
    {
        _base.Enabled();
        _base.SetBot(bot);
        _base.transform.position = transform.position;
        _base = null;
        ConstructionIsCompleted.Invoke();
    }

    public void SetBase(Base baseUnderConstruction)
    {
        _base = baseUnderConstruction;
        _base.Disable();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
