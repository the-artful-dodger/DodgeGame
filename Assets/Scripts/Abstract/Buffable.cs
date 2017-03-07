using UnityEngine;
using System.Collections;

public interface Buffable
{
    float Speed { get; set; }
	float Size {get; set; }
    Sprite StartSprite { get; set; }
    Vector2 Destination { get; set; }
}
