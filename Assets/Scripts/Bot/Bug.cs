using UnityEngine;

namespace Bots
{
    internal sealed class Bug : SingleStorage
    {
        [SerializeField] private Transform _hand;

        public override void Take(Resource resource)
        {
            Resource = resource;
            resource.transform.SetParent(_hand.transform);
            resource.transform.position = _hand.transform.position;
        }
    }
}

