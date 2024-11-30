using System;
using UnityEngine;

public class SecondPlayer : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StartX;
    [SerializeField] private SecondPlayerInput Input;

    private Door _doorToInteract;
    private void Start()
    {
        transform.position = new Vector3(StartX, 0, 0);
        Input.OnInteract += InteractOnstarted;
    }
    private void InteractOnstarted(object sender, EventArgs e)
    {
        HandleInteraction();
    }


    private void Update()
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
        var horizontalMovement = Input.ReadHorizontalMovement();

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