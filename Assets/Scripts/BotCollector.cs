using UnityEngine;

public class BotCollector : MonoBehaviour
{
    [SerializeField] private WarehouseSpace _warehouse;
    [SerializeField] private float _speed = 0.02f;

    private Vector3 _direction;
    private Resource _target = null;

    public bool IsFree => _target == null;

    private void Update()
    {
        if (IsFree == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _direction, _speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Resource resource1))
        {
            if (resource1 == _target)
            {
                resource1.transform.SetParent(transform, true);
                _direction = _warehouse.transform.position;
            }
        }

        if (collision.collider.TryGetComponent(out WarehouseSpace warehouseSpace))//collision.gameObject == _warehouse.gameObject
        {
            if (TryGiveResource(out Resource resource))
            {
                warehouseSpace.SetResource(resource);
            }
        }
    }

    public bool TryGiveResource(out Resource resource)
    {
        resource = GetComponentInChildren<Resource>();
        _target = null;
        return resource != null;
    }

    public void SetTarget(Resource resource)
    {
        _target = resource;
        _direction = resource.transform.position;
    }
}