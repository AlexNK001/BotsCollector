using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private BaseColorStorage _colorStorage;

    private Transform _transform;
    private List<Collider> _collisions;
    private Boundaries _boundariesBuilding;
    private Boundaries _boundariesGround;

    public bool CanBuild { get; private set; }

    public void Init(Boundaries groundBoundaries)
    {
        _transform = transform;
        _collisions = new List<Collider>();
        _boundariesBuilding = new Boundaries(_boxCollider, transform.localScale);
        _boundariesGround = groundBoundaries;
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisions.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _collisions.Remove(other);
    }

    private void OnDisable()
    {
        _collisions.Clear();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        CanBuild = _collisions.Count <= 0 && IsWithinBoundaries();
        Color color = CanBuild ? Color.green : Color.red;
        _colorStorage.SetColor(color);
    }

    private bool IsWithinBoundaries()
    {
        if (_boundariesBuilding.Top + _transform.position.z > _boundariesGround.Top)
            return false;

        if (_boundariesBuilding.Bottom + _transform.position.z < _boundariesGround.Bottom)
            return false;

        if (_boundariesBuilding.Right + _transform.position.x > _boundariesGround.Right)
            return false;

        if (_boundariesBuilding.Left + _transform.position.x < _boundariesGround.Left)
            return false;

        return true;
    }
}
