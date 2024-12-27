using UnityEngine;

[CreateAssetMenu(fileName = nameof(PoolTask), menuName = nameof(PoolTask), order = 1)]
public class PoolTask : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private PoolObject _poolObject;

    public int Price => _price;
    public PoolObject PoolObject => _poolObject;
}
