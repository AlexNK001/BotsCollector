using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;

    public Boundaries GetBoundaries()
    {
        return new Boundaries(_boxCollider,transform.localScale);
    }
}
