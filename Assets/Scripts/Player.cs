using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StartX;
    [SerializeField] private PlayerInput GamePlayerInput;
    [SerializeField] private Floor _currentFloor;

    [SerializeField] private SpriteRenderer SpriteRenderer;

    [SerializeField] private float rotationSpeed;


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
            var doorToInteract = _doorToInteract.transform.position;
            _doorToInteract.OpenClose();
            var targetDoor = _doorToInteract.TargetDoor;
            canMove = false;
            LeanTween.move(gameObject, new Vector3(doorToInteract.x, gameObject.transform.position.y + 1f, 0f), 0.49f).setEaseInOutSine();
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
            if (horizontalMovement < 0)
            {
                SpriteRenderer.flipX = false;

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 6f), Time.deltaTime * rotationSpeed);
            }
            else if (horizontalMovement > 0)
            {
                SpriteRenderer.flipX = true;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -6f), Time.deltaTime * rotationSpeed);
            }


            var movement = new Vector3(horizontalMovement, 0.0f, transform.position.z);

            transform.Translate(movement * (Time.deltaTime * Speed));

        }

    }
}