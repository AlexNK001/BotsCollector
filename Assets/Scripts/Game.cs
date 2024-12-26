using Bots;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    private SpawnPointsHandler _spawnPointsHandler;

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
        _resourcePool = new ItemPool<Resource>(_resourcePrefab,collectionCheck: true);
        _botPool = new ItemPool<Bot>(_botPrefab);
        _basePool = new ItemPool<Base>(_basePrefab);

        _bases = new List<Base>();

        for (int i = 0; i < _basesInScene.Length; i++)
        {
            Base currentBase = _basesInScene[i];
            currentBase.SummonedPoolObject += Get;
            currentBase.ResourceAccepted += Reliase;
            _bases.Add(currentBase);
        }

        _spawnPointsHandler.Summoned += Get;
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
                return _basePool.Get();

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
