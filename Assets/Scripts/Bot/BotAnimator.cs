﻿using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    private readonly int IsRunCommand = Animator.StringToHash("IsRun");

    [SerializeField] private Animator _animator;

    private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        _animator.SetBool(IsRunCommand, _lastPosition != currentPosition);
        _lastPosition = currentPosition;
    }
}
