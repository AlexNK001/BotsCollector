using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    private Resource _resource;

    public void Take(Resource resource)
    {
        _resource = resource;
        resource.transform.SetParent(_hand.transform);
        resource.transform.position = _hand.transform.position;
    }

    public bool TryGet(out Resource resource)
    {
        bool haveGet = _resource != null;
        resource = haveGet ? _resource : null;
        return haveGet;
    }
}


