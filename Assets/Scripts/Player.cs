using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StartX;
    [SerializeField] private GamePlayerInput GamePlayerInput;

    private Door _doorToInteract;
    private void Start()
    {
        transform.position = new Vector3(StartX, 0, 0);
        GamePlayerInput.OnInteract += InteractOnstarted;
    }
    private void InteractOnstarted(object sender, EventArgs e)
    {
        HandleInteraction();
    }


    void Update()
    {
        HandleMovement();
    }
    private void HandleInteraction()
    {
        if (_doorToInteract != null)
        {
            transform.position = _doorToInteract.TargetDoor.transform.position;
        }
        
    }
    private void HandleMovement()
    {
        var horizontalMovement = GamePlayerInput.ReadHorizontalMovement();

        var movement = new Vector3(horizontalMovement, 0.0f, transform.position.z);

        transform.Translate(movement * (Time.deltaTime * Speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<Door>(out var door))
        {
            _doorToInteract = door;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<Door>(out _))
        {
            _doorToInteract = null;
        }
    }
}