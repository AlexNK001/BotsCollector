using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Scanner : MonoBehaviour
{
    public Action<Target> ResourceFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Resource resource))
        {
            SpawnPoint spawnPoint = resource.GetComponentInParent<SpawnPoint>();

            if (spawnPoint != null)
                ResourceFound?.Invoke(spawnPoint);
        }
    }
}
