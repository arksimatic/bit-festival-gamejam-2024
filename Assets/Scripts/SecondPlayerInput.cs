using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondPlayerInput : MonoBehaviour
{
    private InputSystem_Actions _input;
    public EventHandler OnInteract;


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
    public float ReadHorizontalMovement()
    {
        return _input.SecondPlayer.Move.ReadValue<Vector2>().x;
    }
}