using UnityEngine;

public class SpriteAnimatorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject animatedSprite;


    private void Update()
    {
        if (spriteRenderer.sprite.name.Contains("tlo2"))
        {
            spriteRenderer.enabled = false;
            animatedSprite.SetActive(true);
        }
    }
}