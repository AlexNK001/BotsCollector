using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private bool _fixationAxisY = true;

    public event UnityAction<float> SpeedChanged;

    private Vector3 _direction;
    private float _currentSpeed = 0;
    private float _startPosition;

    private void Start()
    {
        _startPosition = transform.position.y;
    }

    private void Update()
    {
        if (_direction != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, _direction, _currentSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            _currentSpeed = 0;
        }
        else
        {
            transform.LookAt(direction);
            _currentSpeed = _speed;
        }

        if (_fixationAxisY)
        {
            direction.y= _startPosition;
        }

        _direction = direction;
        SpeedChanged?.Invoke(_currentSpeed);
    }

    public void Stop()
    {
        _currentSpeed = 0;
        SpeedChanged?.Invoke(0f);
    }
}