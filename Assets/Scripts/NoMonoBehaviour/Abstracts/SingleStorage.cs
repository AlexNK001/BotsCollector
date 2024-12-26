using System;
using UnityEngine;

public abstract class SingleStorage : MonoBehaviour, IResourceTaker, IResourceGiver
{
    private protected Resource Resource;

    public Transform GetTransform()
    {
        return transform;
    }

    public virtual void Take(Resource resource)
    {
        Resource = resource;
        resource.transform.SetParent(transform);
        resource.transform.position = transform.position;
    }

    public virtual bool TryGet(out Resource resource)
    {
        bool haveGet = Resource != null;
        resource = haveGet ? Resource : null;
        return haveGet;
    }
}

