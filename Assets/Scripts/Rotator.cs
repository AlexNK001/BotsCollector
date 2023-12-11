using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _speed = 60;

    private void Update()
    {
        transform.Rotate(Vector3.up, _speed * Time.deltaTime, Space.Self);
    }
}