using UnityEngine;

public struct Boundaries
{
    public readonly float Bottom;
    public readonly float Right;
    public readonly float Left;
    public readonly float Top;

    public Boundaries(BoxCollider boxCollider, Vector3 localScale)
    {
        Bottom = boxCollider.size.z / -2f * localScale.z;
        Right = boxCollider.size.x / 2f * localScale.x;
        Left = boxCollider.size.x / -2f * localScale.x;
        Top = boxCollider.size.z / 2f * localScale.z;
    }
}