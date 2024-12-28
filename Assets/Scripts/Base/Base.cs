using UnityEngine;
using System;
using System.Collections;

public class Base : PoolObject, IPoolUser
{
    [SerializeField, Min(1)] private int _minimumNumberBotsToBuild = 1;
    [SerializeField] private Bot[] _botsInScene;
    [SerializeField] private BaseColorStorage _baseColorStorage;
    [SerializeField] private PoolTask _botPoolTask;
    [SerializeField] private PoolTask _basePoolTask;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private FlagHandler _flagHandler;
    [SerializeField] private Flag _flag;

    internal void Disable()
    {
        gameObject.SetActive(false);
    }

    internal void Enabled()
    {
        gameObject.SetActive(true);
    }

    [SerializeField] private Warehouse _warehouse;

    private PoolTask _currentPoolTask;
    private StorageHandler _storageHandler;

    public Action<Resource> ResourceAccepted;
    public event Func<PoolObject, PoolObject> RequestPoolObject;

    public Color Color { get; private set; }

    public void Init(Boundaries ground, Color color)
    {
        Color = color;

        _storageHandler = new StorageHandler(_scanner);
        _baseColorStorage.SetColor(color);
        AcceptBotsFromScene();

        _flagHandler.Init(ground);

        _warehouse.ResourceAccepteed += OnTryCreate;
        _flagHandler.FlagIsSetting += OnBuildBase;
        _flagHandler.enabled = false;

        _currentPoolTask = _botPoolTask;
    }

    private void OnDestroy()
    {
        if (_storageHandler != null)
            _storageHandler.Unsubscribe();

        _warehouse.ResourceAccepteed -= OnTryCreate;
        _flagHandler.FlagIsSetting -= OnBuildBase;
    }

    public void SetBot(Bot bot)
    {
        bot.SetBase(this);
        _storageHandler.SetBot(bot);

        if (_storageHandler.BotCount >= _minimumNumberBotsToBuild)
        {
            _flagHandler.enabled = true;
        }
    }

    private void OnTryCreate(Resource resource)
    {
        ResourceAccepted.Invoke(resource);
        TryCreate();
    }

    private void TryCreate()
    {
        if (_warehouse.TrySpend(_currentPoolTask))
        {
            switch (RequestPoolObject.Invoke(_currentPoolTask.PoolObject))
            {
                case Bot bot:
                    bot.SetBase(this);
                    SetBot(bot);
                    break;

                case Base building:
                    _flag.SetBase(building);
                    _currentPoolTask = _botPoolTask;
                    _storageHandler.EnqueueTarget(_flag);
                    break;
            }
        }
    }

    private void OnBuildBase()
    {
        StartCoroutine(WaitRequiredNumberBots());
    }

    private IEnumerator WaitRequiredNumberBots()
    {
        yield return new WaitUntil(() => _storageHandler.BotCount > _minimumNumberBotsToBuild);
        _currentPoolTask = _basePoolTask;
    }

    private void AcceptBotsFromScene()
    {
        for (int i = 0; i < _botsInScene.Length; i++)
        {
            Bot currentBot = _botsInScene[i];
            currentBot.SetBase(this);
            SetBot(currentBot);
        }
    }
}
