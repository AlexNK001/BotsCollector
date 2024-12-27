using TMPro;
using UnityEngine;

public class ShowingCountResources : MonoBehaviour
{
    [SerializeField] private Warehouse _warehouse;
    [SerializeField] private TMP_Text _resourceCount;

    private void Start()
    {
        _warehouse.ResourceCountChanged += OnShowCount;
    }

    private void OnDestroy()
    {
        _warehouse.ResourceCountChanged -= OnShowCount;
    }

    private void OnShowCount(int resourceCount)
    {
        _resourceCount.text = resourceCount.ToString();
    }
}
