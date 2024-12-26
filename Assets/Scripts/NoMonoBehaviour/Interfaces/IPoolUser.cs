using System;

public interface IPoolUser
{
    public event Func<PoolObject, PoolObject> Summoned;
}