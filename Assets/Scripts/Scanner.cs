using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private Dictionary<Resource, bool> _resources = new();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            CheakingResource(resource);
        }
    }

    public bool TryGetResource(out Resource resource)
    {
        resource = null;

        if (_resources.Any(resourceKey => resourceKey.Value == true))
        {
            resource = _resources.First(resourceKey => resourceKey.Value).Key;
            _resources[resource] = false;
            return true;
        }

        return false;
    }

    private void CheakingResource(Resource resource)
    {
        if (_resources.ContainsKey(resource) == false)
        {
            _resources.Add(resource, true);
        }
    }
}