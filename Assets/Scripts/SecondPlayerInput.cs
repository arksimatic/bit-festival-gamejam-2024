using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondPlayerInput : PlayerInput
{
    private InputSystem_Actions _input;
    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.SecondPlayer.Enable();
        _input.SecondPlayer.Interact.started += InteractOnstarted;
    }
    private void InteractOnstarted(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
    public override float ReadHorizontalMovement()
    {
        return _input.SecondPlayer.Move.ReadValue<Vector2>().x;
    }
}