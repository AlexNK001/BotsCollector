using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Scanner : MonoBehaviour
{
    public event UnityAction<Resource> ResourceFound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            if (resource.transform.parent != null)
            {
                if (resource.transform.parent.TryGetComponent<HidingResource>(out _) == false)
                {
                    ResourceFound?.Invoke(resource);
                }
            }
            else
            {
                ResourceFound?.Invoke(resource);
            }
        }
    }
}