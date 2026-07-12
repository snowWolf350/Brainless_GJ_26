using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 mouseWorldPosition = Vector3.zero;

    [SerializeField] LayerMask aimLayer;

    Builder _selectedBuilder;

    private void Start()
    {
        GameInput.Instance.OnLeftClick += GameInput_OnLeftClick;
    }
    private void OnDisable()
    {
        GameInput.Instance.OnLeftClick -= GameInput_OnLeftClick;
    }


    private void GameInput_OnLeftClick(object sender, System.EventArgs e)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 99f, aimLayer))
        {
            mouseWorldPosition = raycastHit.point;

            if (raycastHit.transform.TryGetComponent(out Builder builder))
            {
                _selectedBuilder = builder;
            }
            if (raycastHit.transform.TryGetComponent(out Fence fence))
            {
                if (_selectedBuilder == null)
                {
                    //there is no selected builder just fence he selected
                    return;
                }
                _selectedBuilder.SendBuilderToFence(fence);
            }
        }
    }

}
