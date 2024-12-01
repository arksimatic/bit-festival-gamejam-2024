using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<Player>(out _))
        {
            Debug.Log("Player RED wins");
        }

        if (other.attachedRigidbody.gameObject.TryGetComponent<SecondPlayer>(out _))
        {
            Debug.Log("Player BLUE wins");
        }

        Application.Quit();
    }
}