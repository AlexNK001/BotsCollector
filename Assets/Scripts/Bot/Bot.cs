using System;
using UnityEngine;

namespace Bots
{
    [RequireComponent(typeof(Mover), typeof(Bug))]
    public class Bot : PoolObject
    {
        [SerializeField] private Mover _mover;
        [SerializeField] private Bug _bug;

        [SerializeField] private IBotTarget _target;
        [SerializeField] private IResourceTaker _base;

        public event Action<Bot> Freed;

        public void SetBase(IResourceTaker storageWarehouse)
        {
            _base = storageWarehouse;
        }

        private void OnTriggerEnter(Collider other)//вынести в отдельный класс??
        {
            if (other.transform.TryGetComponent(out IBotTarget botTarget) && botTarget == _target)
            {
                switch (botTarget)
                {
                    case IResourceGiver spawnPoint:
                        if (spawnPoint.TryGet(out Resource resourceTaken))//убрать Try??
                        {
                            _bug.Take(resourceTaken);
                            _target = _base;
                        }

                        _mover.SetDirection(_base.GetTransform());
                        break;

                    case IResourceTaker warehouse:
                        if (_bug.TryGet(out Resource resourceGiven))//убрать Try??
                        {
                            warehouse.Take(resourceGiven);
                            Freed?.Invoke(this);
                        }
                        break;

                    case IBotBilder bilder:
                        bilder.Build(this);
                        break;
                }
            }
        }

        public void SetTarget(IBotTarget storage)
        {
            _target = storage;
            _mover.SetDirection(storage.GetTransform());
        }
    }
}
