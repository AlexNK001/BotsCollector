using UnityEngine;

//public abstract class Spawner<T> : MonoBehaviour  where T : PoolObject
//{
//    [SerializeField] private IPoolUser _spawnPoint;
//    [SerializeField] private T _prefab;

//    private ItemPool<T> _pool;

//    public void Init(IPoolUser spawnPoint)
//    {
//        _spawnPoint = spawnPoint;
//        _pool = new ItemPool<T>(_prefab);
//        //_spawnPoint.Summoned += Get;
//    }

//    public virtual PoolObject Get()
//    {
//        return _pool.Get();
//    }

//    public virtual void Reliase(T item)
//    {
//        _pool.Relise(item);
//    }
//} 
