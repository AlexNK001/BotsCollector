using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Storage))]
public class BotCollector : MonoBehaviour
{
    [SerializeField] private WarehouseSpace _warehouse;
    [SerializeField] private Hand _hand;

    public event UnityAction<BotCollector> StorageFree;

    private Movement _movement;
    private Storage _storage;
    private BotCollector _botCollector;

    public Resource Target { get; private set; }

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _storage = GetComponent<Storage>();
        _storage.SetHand(_hand);
        _botCollector = GetComponent<BotCollector>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Resource resource1))
        {
            if (resource1 == Target)
            {
                _storage.SetResource(resource1);
                _movement.SetDirection(_warehouse.transform.position);
            }
        }

        if (collision.collider.TryGetComponent(out WarehouseSpace warehouseSpace))
        {
            if (_storage.TryGetResource(out Resource resource))
            {
                warehouseSpace.Storage.SetResource(resource);
            }

            _movement.Stop();
            StorageFree?.Invoke(_botCollector);
        }
    }

    public void SetTarget(Resource resource)
    {
        Target = resource;
        _movement.SetDirection(Target.transform.position);
    }
}