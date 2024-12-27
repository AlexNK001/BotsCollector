using UnityEngine;

public class UserInput : MonoBehaviour
{
    private const KeyCode LeftMouseButton = KeyCode.Mouse0;
    private const KeyCode RigthMouseButton = KeyCode.Mouse1;

    [SerializeField] private Ground _ground;

    private Camera _mainCamera;
    private BoxCollider _boxCollider;
    private IMouseFollower _mouseFollower;

    private void Start()
    {
        _mainCamera = Camera.main;
        _boxCollider = _ground.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (_mouseFollower != null)
        {
            if (Input.GetKeyDown(LeftMouseButton))
            {
                _mouseFollower.Stop();
                _mouseFollower = null;
            }

            if (Input.GetKeyDown(RigthMouseButton))
            {
                _mouseFollower.Cancel();
                _mouseFollower = null;
            }

            if (_mouseFollower != null && _boxCollider.Raycast(ray, out RaycastHit info, float.MaxValue))
            {
                _mouseFollower.Move(info.point);
            }
        }

        if (Input.GetKeyDown(LeftMouseButton))
        {
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out IMouseFollower mouseInteraction))
                {
                    _mouseFollower = mouseInteraction;
                    _mouseFollower.Click();
                }
            }
        }
    }
}
