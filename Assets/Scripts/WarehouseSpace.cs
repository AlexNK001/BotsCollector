using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Storage))]
public class WarehouseSpace : MonoBehaviour
{
    [SerializeField] private BotCollector[] _collectors = new BotCollector[3];
    [SerializeField] private Scanner _scanner;

    private Storage _storage;
    private Queue<Resource> _bringResources = new Queue<Resource>();

    private event UnityAction<Resource> ResourceLoaded;

    public Storage Storage => _storage;

    private void Start()
    {
        _storage = GetComponent<Storage>();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _collectors.Length; i++)
        {
            _collectors[i].StorageFree += TryTakeTask;
        }

        _scanner.ResourceFound += CheakingResource;
        ResourceLoaded += TryGivingTaskFreeBot;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _collectors.Length; i++)
        {
            _collectors[i].StorageFree -= TryTakeTask;
        }

        _scanner.ResourceFound -= CheakingResource;
        ResourceLoaded -= TryGivingTaskFreeBot;
    }

    private void CheakingResource(Resource resource)
    {
        if (_collectors.Any(bot => bot.Target == resource) == false)
        {
            if (_bringResources.Contains(resource) == false)
            {
                _bringResources.Enqueue(resource);
                ResourceLoaded?.Invoke(resource);
            }
        }
    }

    private void TryGivingTaskFreeBot(Resource resource)
    {
        if (_bringResources.Count > 0)
        {
            foreach (BotCollector bot in _collectors)
            {
                if (bot.Target == null)
                {
                    bot.SetTarget(_bringResources.Dequeue());
                    break;
                }
            }
        }
    }

    public void TryTakeTask(BotCollector botCollector)
    {
        if (_bringResources.Count > 0)
        {
            botCollector.SetTarget(_bringResources.Dequeue());
        }
    }
}