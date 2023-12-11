using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarehouseSpace : MonoBehaviour
{
    [SerializeField] private Scanner _scanningArea;
    [SerializeField] private BotCollector[] _collectors = new BotCollector[3];
    [SerializeField] private List<Resource> _resources = new List<Resource>();

    private void FixedUpdate()
    {
        if (_collectors.Any(bot => bot.IsFree))
        {
            BotCollector bot = _collectors.First(bot => bot.IsFree);

            if (bot != null && _scanningArea.TryGetResource(out Resource resource))
            {
                bot.SetTarget(resource);
            }
        }
    }

    public void SetResource(Resource resource)
    {
        _resources.Add(resource);
        resource.transform.SetParent(transform, true);
        resource.gameObject.SetActive(false);
    }
}