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
        _flag.Disable();
    }

    public void Click()
    {
        _buildingPreview.Enable();

        _flag.Enable();
        _lastFlagPosition = _flag.transform.position;
    }

    public void Move(Vector3 position)
    {
        _flag.transform.position = position;
    }

    public void Stop()
    {
        _buildingPreview.Disable();

        if (_buildingPreview.CanBuild)
            FlagIsSetting.Invoke();
        else
            _flag.transform.position = _lastFlagPosition;
    }

    public void Cancel()
    {
        _buildingPreview.Disable();

        if (_lastFlagPosition == transform.position)
            _flag.Disable();

        _flag.transform.position = _lastFlagPosition;
    }

    private void OnReturnFlag()
    {
        _flag.Enable();
        _flag.transform.position = transform.position;
    }
}
