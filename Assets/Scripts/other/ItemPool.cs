using UnityEngine;
using UnityEngine.Pool;

public class ItemPool<T> where T : PoolObject
{
    private readonly T _prefab;
    private readonly ObjectPool<T> _objectPool;

    public ItemPool(
        T prefab,
        bool collectionCheck = true,
        int defaultCapacity = 100,
        int maxSize = 10000)
    {
        _prefab = prefab;

        _objectPool = new ObjectPool<T>(
            createFunc: () => CreateFunc(),
            actionOnGet: (item) => item.gameObject.SetActive(true),
            actionOnRelease: (item) => item.gameObject.SetActive(false),
            collectionCheck: collectionCheck,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize);
    }

    public T Get()
    {
        return _objectPool.Get();
    }

    public void Relise(T item)
    {
        _objectPool.Release(item);
    }

    private T CreateFunc()
    {
        T item = MonoBehaviour.Instantiate(_prefab);
        item.gameObject.SetActive(false);
        return item;
    }
}