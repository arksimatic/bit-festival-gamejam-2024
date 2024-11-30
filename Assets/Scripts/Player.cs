using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private InputSystem_Actions input;
    private void Awake()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
    }
    void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        var isInteract = input.Player.Interact.IsPressed();

        Debug.Log($"Is interact: {isInteract}");
    }
    private void HandleMovement()
    {

        var horizontalMovement = input.Player.Move.ReadValue<Vector2>().x;

        var movement = new Vector3(horizontalMovement, 0.0f, transform.position.z);

        transform.Translate(movement * (Time.deltaTime * speed));
    }
}