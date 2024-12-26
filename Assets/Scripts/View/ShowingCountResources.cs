using TMPro;
using UnityEngine;

public class ShowingCountResources : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private TMP_Text _resourceCount;

    private void Start()
    {
        _base.ResourceCountChanged += ShowCount;
    }

    private void OnDestroy()
    {
        _base.ResourceCountChanged -= ShowCount;
    }

    private void ShowCount(int resourceCount)
    {
        _resourceCount.text = resourceCount.ToString();
    }
}
