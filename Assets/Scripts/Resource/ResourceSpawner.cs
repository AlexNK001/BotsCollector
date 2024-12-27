using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour, IPoolUser
{
    [SerializeField] private PoolTask _poolTask;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Timer _timer;

    private Queue<SpawnPoint> _freePoints;

    public event Func<PoolObject, PoolObject> RequestPoolObject;

    private void Start()
    {
        _freePoints = new Queue<SpawnPoint>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            SpawnPoint currentSpawnPoint = _spawnPoints[i];
            currentSpawnPoint.Freed += AddToFreePoints;

            _freePoints.Enqueue(currentSpawnPoint);
        }

        _timer.TimeHasCome += Spawn;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _spawnPoints[i].Freed -= AddToFreePoints;
        }

        _timer.TimeHasCome -= Spawn;
    }

    private void AddToFreePoints(SpawnPoint spawnPoints)
    {
        if (_freePoints.Contains(spawnPoints) == false)
            _freePoints.Enqueue(spawnPoints);
    }

    private void Spawn()
    {
        if (_freePoints.Count > 0)
        {
            SpawnPoint spawnPoint = _freePoints.Dequeue();
            Resource resource = RequestPoolObject.Invoke(_poolTask.PoolObject) as Resource;
            spawnPoint.Take(resource);
        }
    }
}