using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    //Input 
    PlayerInput _playerInput;
    public event EventHandler OnLeftClick;
    public event EventHandler OnEscapePressed;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        Instance = this;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Start()
    {
        _playerInput.player.leftClick.performed += LeftClick_performed;
        _playerInput.player.esc.performed += Esc_performed;
    }


    private void OnDestroy()
    {
        _playerInput.player.leftClick.performed -= LeftClick_performed;
        _playerInput.player.esc.performed -= Esc_performed;
    }
    private void Esc_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnEscapePressed?.Invoke(this, EventArgs.Empty);
    }
    private void LeftClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnLeftClick?.Invoke(this, EventArgs.Empty);
    }
}
