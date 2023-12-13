using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private bool _isSetResourceActive = false;

    private List<Resource> _collectedResources = new List<Resource>();
    private Transform _transformForResourse;

    private void Start()
    {
        _transformForResourse = GetComponent<Transform>();
    }

    public bool TryGetResource(out Resource resource)
    {
        if (_collectedResources.Count > 0)
        {
            resource = _collectedResources.First();
            _collectedResources.Remove(resource);
            resource.transform.SetParent(null);
        }
        else
        {
            resource = null;
        }

        return resource != null;
    }

    public void SetResource(Resource resource)
    {
        _collectedResources.Add(resource);
        resource.transform.SetParent(_transformForResourse, true);
        resource.transform.position = _transformForResourse.position;
        resource.gameObject.SetActive(_isSetResourceActive);
    }

    public void SetHand(Hand hand)
    {
        _transformForResourse = hand.transform;
    }
}