using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Buffable))]
public class BuffManager : MonoBehaviour
{
    public float BuffDuration = 3;
    List<Component> buffList;
    SpriteRenderer spriteRenderer;
    Sprite originalSprite;
    Buffable buffable;
    float originalSpeed;
	float originalSize;
    float originalWanderRadius;
    float nextBuffTime;

    // Use this for initialization
    void Start()
    {
        buffList = new List<Component>(GetComponents(typeof(Buff)));
        buffable = (Buffable)GetComponent(typeof(Buffable));
        originalSpeed = buffable.Speed;
		originalSize = 1;

        spriteRenderer = (SpriteRenderer)GetComponent(typeof(SpriteRenderer));
        originalSprite = spriteRenderer.sprite;
    }

    void Update()
    {
        if (Time.time >= nextBuffTime)
        {
            int rand = Random.Range(0, buffList.Count + 5);
            Buff b;

            RemoveBuff();

            if (rand < buffList.Count)
            {
                b = (Buff)buffList[rand];
                b.ApplyBuff();
            }

            nextBuffTime = Time.time + BuffDuration;
        }
    }

    void RemoveBuff()
    {
        buffable.Speed = originalSpeed;
		buffable.Size = originalSize;
        spriteRenderer.sprite = originalSprite;
    }
}
