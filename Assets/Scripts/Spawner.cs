using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Resource _resorce;
    [SerializeField] private int _maxNumberResources = 30;
    [SerializeField] private float _timeBetweenCreation;

    private WaitForSeconds _waitingBetweenCreation;
    private Coroutine _resourceCreation;
    private int _currentNumberResources = 0;


    private void Start()
    {
        _waitingBetweenCreation = new WaitForSeconds(_timeBetweenCreation);

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Instantiate(_resorce, _spawnPoints[i].transform);
            _currentNumberResources++;
        }

        _resourceCreation = StartCoroutine(CreateResources());
    }

    private IEnumerator CreateResources()
    {
        while (_maxNumberResources > _currentNumberResources)
        {
            foreach (SpawnPoint point in _spawnPoints)
            {
                if (point.transform.childCount <= 0)
                {
                    Instantiate(_resorce, point.transform);
                    _currentNumberResources++;
                }
            }

            yield return _waitingBetweenCreation;
        }

        if (_resourceCreation != null)
        {
            StopCoroutine(_resourceCreation);
        }
    }
}