using System;
using UnityEngine;

public class SpawnPoint : Target
{
    private Resource _resource;

    public Action<SpawnPoint> Freed;

    public bool HaveResource => _resource != null;

    public bool TryGet(out Resource resource)
    {
        if (_resource != null)
        {
            resource = _resource;
            _resource = null;
            Freed.Invoke(this);
            return true;
        }
        else
        {
            resource = null;
            return false;
        }
    }

    public void Take(Resource resource)
    {
        _resource = resource;
        resource.transform.SetParent(transform);
        resource.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    }
}

