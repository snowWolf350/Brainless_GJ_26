using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 mouseWorldPosition = Vector3.zero;

    [SerializeField] LayerMask aimLayer;

    Bot _selectedBot;

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

            if (raycastHit.transform.TryGetComponent(out Bot bot))
            {
                if (_selectedBot != null)
                {
                    _selectedBot.DisableSelectedVisual();
                }

                _selectedBot = bot;
                _selectedBot.EnableSelectedVisual();
                return;
            }
            if (raycastHit.transform.TryGetComponent(out Fence fence))
            {
                if (_selectedBot == null)
                {
                    //there is no selected builder just fence he selected
                    return;
                }
                _selectedBot.SendBotToFence(fence);
                _selectedBot.DisableSelectedVisual();
                _selectedBot = null;
                return;
            }

            //not a fence is clicked nor a bot is clicked
            if(_selectedBot != null)
            {
                _selectedBot.SendBotToPosition(mouseWorldPosition);
            }
        }
    }

}
