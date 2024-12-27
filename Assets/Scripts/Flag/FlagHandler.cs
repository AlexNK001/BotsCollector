using System;
using UnityEngine;

public class FlagHandler : MonoBehaviour, IMouseFollower
{
    [SerializeField] private Flag _flag;
    [SerializeField] private BuildingPreview _buildingPreview;

    private Vector3 _lastFlagPosition;

    public Action FlagIsSetting;
    public Action ConstructionIsCompleted;
    public Action FlagRemoved;

    private void OnDestroy()
    {
        _flag.ConstructionIsCompleted -= OnReturnFlag;
    }

    public void Init(Boundaries ground)
    {
        _buildingPreview.Init(ground);
        _flag.ConstructionIsCompleted += OnReturnFlag;
        _flag.gameObject.SetActive(false);
    }

    public void Click()
    {
        _lastFlagPosition = _flag.transform.position;
        _flag.gameObject.SetActive(true);
        _buildingPreview.gameObject.SetActive(true);
    }

    public void Move(Vector3 position)
    {
        _flag.transform.position = position;
    }

    public void Stop()
    {
        if (_buildingPreview.CanBuild)
            FlagIsSetting.Invoke();
        else
            _flag.transform.position = _lastFlagPosition;

        _buildingPreview.gameObject.SetActive(false);
    }

    public void Cancel()
    {
        if (_lastFlagPosition == transform.position)
            _flag.gameObject.SetActive(false);

        _flag.transform.position = _lastFlagPosition;
        _buildingPreview.gameObject.SetActive(false);
    }

    private void OnReturnFlag()
    {
        _flag.transform.position = transform.position;
        _flag.gameObject.SetActive(false);
    }
}
