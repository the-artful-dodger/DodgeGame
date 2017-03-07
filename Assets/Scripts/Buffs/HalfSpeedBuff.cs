using UnityEngine;
using System.Collections;

public class HalfSpeedBuff : Buff
{
    public Sprite buffSprite;

    SpriteRenderer spriteRenderer;
    Buffable buffable;

    // Use this for initialization
    void Start()
    {
        buffable = (Buffable)gameObject.GetComponent(typeof(Buffable));
        spriteRenderer = (SpriteRenderer)gameObject.GetComponent(typeof(SpriteRenderer));
    }

    public override void ApplyBuff()
    {
        buffable.Speed *= 0.5f;
        spriteRenderer.sprite = buffSprite;
    }

    public override string BuffName
    {
        get { return "Half Speed"; }
    }
}
