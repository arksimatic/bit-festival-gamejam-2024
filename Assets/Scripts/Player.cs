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
        if (canMove)
        {
            HandleInteraction();
        }

    }
    private void HandleInteraction()
    {
        if (_doorToInteract != null)
        {
            _doorToInteract.OpenClose();
            var targetDoor = _doorToInteract.TargetDoor;
            canMove = false;
            LeanTween.moveY(gameObject, gameObject.transform.position.y + 1f, 0.49f).setEaseInOutSine();
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            var color = spriteRenderer.color;
            LeanTween.value(gameObject, color, Color.black, 0.5f).setOnUpdate(color1 => { spriteRenderer.color = color1; }).setEaseInOutSine();
            LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInOutSine().setOnComplete(() =>
            {
                transform.position = targetDoor.transform.position;
                targetDoor.OpenClose();
                LeanTween.value(gameObject, Color.black, color, 0.5f).setOnUpdate(color1 => { spriteRenderer.color = color1; }).setEaseInOutSine();
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