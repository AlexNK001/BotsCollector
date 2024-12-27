using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Ground _ground;
    [SerializeField] private ResourceSpawner _spawnPointsHandler;
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Base[] _basesInScene;

    private ItemPool<Resource> _resourcePool;
    private ItemPool<Bot> _botPool;
    private ItemPool<Base> _basePool;

    private List<Base> _bases;

    private void Start()
    {
        _resourcePool = new ItemPool<Resource>(_resourcePrefab);
        _botPool = new ItemPool<Bot>(_botPrefab);
        _basePool = new ItemPool<Base>(_basePrefab);

        _bases = new List<Base>();

        for (int i = 0; i < _basesInScene.Length; i++)
            AddToListBase(_basesInScene[i]);

        _spawnPointsHandler.RequestPoolObject += Get;
    }

    private void AddToListBase(Base createdBase)
    {
        createdBase.Init(_ground.GetBoundaries(), Random.ColorHSV());


        createdBase.RequestPoolObject += Get;
        createdBase.ResourceAccepted += Reliase;
        _bases.Add(createdBase);
    }

    private PoolObject Get(PoolObject requestedPoolObject)
    {
        switch (requestedPoolObject)
        {
            case Resource:
                return _resourcePool.Get();

            case Bot:
                return _botPool.Get();

            case Base:
                Base newBase = _basePool.Get();
                AddToListBase(newBase);
                return newBase;

            default:
                return null;
        }
    }

    private void Reliase(PoolObject poolObject)
    {
        switch (poolObject)
        {
            case Resource resource:
                _resourcePool.Relise(resource);
                break;

            case Bot bot:
                _botPool.Relise(bot);
                break;

            case Base returnedBase:
                _basePool.Relise(returnedBase);
                break;
        }
    }
}
