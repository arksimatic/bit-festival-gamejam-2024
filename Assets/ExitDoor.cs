using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody.gameObject.TryGetComponent<Player>(out var player))
        {
            var playerName = player.gameObject.name;
            FindFirstObjectByType<WinScreenManager>().OnShowWinScreen(playerName);
        }
    }
}