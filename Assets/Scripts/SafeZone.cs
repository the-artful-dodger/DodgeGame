using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Sprite))]
[RequireComponent(typeof(SpriteRenderer))]
public class SafeZone : MonoBehaviour
{
    public Sprite EnteredSprite;
	//public bool isFinal = false;

    Sprite defaultSprite;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
        defaultSprite = spriteRenderer.sprite;
    }

    public void Entered()
    {
        spriteRenderer.sprite = EnteredSprite;
    }

    public void Left()
    {
        spriteRenderer.sprite = defaultSprite;
    }
}
