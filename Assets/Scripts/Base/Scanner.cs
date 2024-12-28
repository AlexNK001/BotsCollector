using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Scanner : MonoBehaviour
{
    public Action<Target> ResourceFound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SpawnPoint resource))
        {
            if (resource.HaveResource)
                ResourceFound?.Invoke(resource);
        }
    }
}
