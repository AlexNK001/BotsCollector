using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;

    private Vector3 _direction;

    private void Start()
    {
        _direction = transform.position;
    }

    private void Update()
    {
        if (transform.position != _direction)
            transform.position = Vector3.MoveTowards(
                transform.position, 
                _direction, 
                _speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        _direction.y = transform.position.y;
        transform.LookAt(_direction);
    }

    public void Stop()
    {
        _direction = transform.position;
    }
}
