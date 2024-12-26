using UnityEngine;

[CreateAssetMenu(fileName = nameof(SummonedTaskSO), menuName = nameof(SummonedTaskSO), order = 1)]
public class SummonedTaskSO : ScriptableObject
{
    [SerializeField] private int _price;
    [SerializeField] private PoolObject _poolObject;

    public int Price => _price;
    public PoolObject PoolObject => _poolObject;
}
