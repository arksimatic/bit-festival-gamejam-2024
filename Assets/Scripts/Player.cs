using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StartX;
    [SerializeField] private PlayerInput GamePlayerInput;
    [SerializeField] private Floor _currentFloor;
    private Door _doorToInteract;
    private bool canMove;

    private void Start()
    {
        transform.position = new Vector3(StartX, 0, 0);
        GamePlayerInput.OnInteract += InteractOnstarted;
        canMove = true;
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
            var targetPosition = _doorToInteract.TargetDoor.transform.position;
            canMove = false;
            LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(() =>
            {
                transform.position = targetPosition;
                LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseInOutSine().setOnComplete(() =>
                {
                    canMove = true;
                });
            });

            _currentFloor = _doorToInteract.TargetDoor.Floor;
            _currentFloor.StartShuffle();

        }

    }

    private void HandleMovement()
    {
        if (canMove)
        {
            var horizontalMovement = GamePlayerInput.ReadHorizontalMovement();

            var movement = new Vector3(horizontalMovement, 0.0f, transform.position.z);

            transform.Translate(movement * (Time.deltaTime * Speed));
        }

    }
}