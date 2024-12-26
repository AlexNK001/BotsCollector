using UnityEngine;

namespace Bots
{
    internal class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed = 4f;

        private Vector3 _direction;

        private void Update()
        {
            if (transform.position != _direction)
                transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
        }

        internal void SetDirection(Transform direction)
        {
            float height = transform.position.y;

            _direction = direction.position;
            _direction.y = height;

            Vector3 lookDirection = direction.position;
            lookDirection.y = height;
            transform.LookAt(lookDirection);
        }
    }
}
