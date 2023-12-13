using UnityEngine;

public class AnimatedController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Movement _movement;

    private void OnEnable()
    {
        _movement.SpeedChanged += SetSpeed;
    }

    private void OnDisable()
    {
        _movement.SpeedChanged -= SetSpeed;
    }

    private void SetSpeed(float speed)
    {
        _animator.SetFloat(AnimatorCommands.CommandSpeed, speed);
    }
}
