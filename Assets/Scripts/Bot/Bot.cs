using System;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Bag))]
public class Bot : PoolObject
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Bag _bug;
    [SerializeField] BotColorStorage _botColorStorage;

    public event Action<Bot> Freed;
    public Action<Bot> BaseChanged;

    private Target _target;
    private Target _warehouse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Target target) && target == _target)
        {
            switch (target)
            {
                case SpawnPoint spawnPoint:
                    TryTakeResource(spawnPoint);
                    break;

                case Warehouse warehouse:
                    TryGiveResource(warehouse);
                    break;

                case Flag bilder:
                    bilder.Build(this);
                    break;
            }
        }
    }

    public void SetBase(Base newBase)
    {
        _warehouse = newBase.GetComponent<Warehouse>();
        _botColorStorage.SetColor(newBase.Color);
    }

    public void SetTarget(Target storage)
    {
        _target = storage;
        _mover.SetDirection(_target.transform.position);

        if (storage is Flag)
            BaseChanged.Invoke(this);
    }

    private void TryTakeResource(SpawnPoint spawnPoint)
    {
        if (spawnPoint.TryGet(out Resource resourceTaken))
        {
            _bug.Take(resourceTaken);
            _target = _warehouse;
            _mover.SetDirection(_target.transform.position);
        }
        else
        {
            Freed.Invoke(this);
        }
    }

    private void TryGiveResource(Warehouse warehouse)
    {
        if (_bug.TryGet(out Resource resourceGiven))
        {
            _mover.Stop();
            warehouse.Take(resourceGiven);
            _target = null;
        }

        Freed.Invoke(this);
    }
}
