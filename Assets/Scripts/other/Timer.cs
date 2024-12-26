using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _timeBetweenAppearances = 0.2f;

    private float _currentTime;

    public Action TimeHasCome;

    public void Start()
    {
        _currentTime = _timeBetweenAppearances;
    }

    private void Update()
    {
        if(_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            _currentTime = _timeBetweenAppearances;
            TimeHasCome?.Invoke();
        }
    }
}
