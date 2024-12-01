using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StartX;
    [SerializeField] private PlayerInput GamePlayerInput;
    [SerializeField] private Floor _currentFloor;
    private Door _doorToInteract;

    private void Start()
    {
        transform.position = new Vector3(StartX, 0, 0);
        GamePlayerInput.OnInteract += InteractOnstarted;
    }

    void Update()
    {
        HandleMovement();
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
    private void InteractOnstarted(object sender, EventArgs e)
    {
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        if (_doorToInteract != null)
        {
            transform.position = _doorToInteract.TargetDoor.transform.position;
            if (_currentFloor != null)
            {
                _currentFloor.PlayerCount--;
            }

            _currentFloor = _doorToInteract.TargetDoor.Floor;
            if (_currentFloor != null)
            {
                _currentFloor.PlayerCount++;
                _currentFloor.StartShuffle();
            }

        }

    }
    private void HandleMovement()
    {
        var horizontalMovement = GamePlayerInput.ReadHorizontalMovement();

        var movement = new Vector3(horizontalMovement, 0.0f, transform.position.z);

        transform.Translate(movement * (Time.deltaTime * Speed));
    }
}