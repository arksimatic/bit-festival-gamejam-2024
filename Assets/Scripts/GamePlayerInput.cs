using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePlayerInput : MonoBehaviour
{
    private InputSystem_Actions _input;
    public EventHandler OnInteract;


    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Player.Enable();
        _input.Player.Interact.started += InteractOnstarted;
    }
    private void InteractOnstarted(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
    public float ReadHorizontalMovement()
    {
        return _input.Player.Move.ReadValue<Vector2>().x;
    }
}