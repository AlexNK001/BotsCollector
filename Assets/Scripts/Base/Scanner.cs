using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Scanner : MonoBehaviour
{
    public event UnityAction<IBotTarget> ResourceFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            SpawnPoint spawnPoint = resource.GetComponentInParent<SpawnPoint>();

            if (spawnPoint != null)
            {
                ResourceFound?.Invoke(spawnPoint);
            }
        }
    }
}