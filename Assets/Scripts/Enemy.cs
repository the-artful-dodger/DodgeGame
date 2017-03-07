using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, Buffable
{
    public float MoveSpeed = 4;             // Move speed
    public float WaitTime = 3;          // Time to wait before changing direction
    public float WanderRadius = 30;     // The radius in which the creature is bound

    // Bounds to stay within
    private float minX;
    private float minY;
    private float maxX;
    private float maxY;

    // Random destination to move towards
    private Vector2 destination;

    // Time to change destination
    private float nextMoveTime;

    // Level bounds
    Vector2 lowerBounds;
    Vector2 upperBounds;

    Vector2 Buffable.Destination
    {
        get
        {
            return destination;
        }
        set
        {
            destination = value;
        }
    }

    Sprite Buffable.StartSprite
    {
        get
        {
            return ((SpriteRenderer)GetComponent(typeof(SpriteRenderer))).sprite;
        }
        set
        {
            ((SpriteRenderer)GetComponent(typeof(SpriteRenderer))).sprite = value;
        }
    }

	float Buffable.Speed
	{
		get
		{
			return MoveSpeed;
		}
		set
		{
			MoveSpeed = value;
		}
	}
	float Buffable.Size
	{
		get
		{
			return transform.localScale.magnitude;
		}
		set
		{
			transform.localScale = new Vector3(value,value,value);
		}
	}

    // Use this for initialization
    void Start()
    {
        // Find the background to use for bounds
        GameObject background = GameObject.Find("Background");
        if (!background)
            Debug.LogError("Background not found!");

        // Set bounds based on the background renderer
        minX = transform.position.x - WanderRadius;
        minY = transform.position.y - WanderRadius;
        maxX = transform.position.x + WanderRadius;
        maxY = transform.position.y + WanderRadius;

        // Find the lower and upper bounds of the map
        lowerBounds = new Vector2(background.renderer.bounds.min.x, background.renderer.bounds.min.y);
        upperBounds = new Vector2(background.renderer.bounds.max.x, background.renderer.bounds.max.y);
    }

    void Update()
    {
        // If the enemy hits a level boundary immediately move to a new destination
        if (transform.position.x == lowerBounds.x || transform.position.x == upperBounds.x ||
            transform.position.y == lowerBounds.y || transform.position.y == upperBounds.y)
            SetDestination(true);
        else
            SetDestination();

        transform.position = Vector2.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
    }

    void SetDestination(bool immediate = false)
    {
        // Move to a random location every X seconds
        if (Time.time >= nextMoveTime || immediate)
        {
            nextMoveTime = Time.time + WaitTime;
            float x = Mathf.Clamp(Random.Range(minX, maxX), lowerBounds.x, upperBounds.x);
            float y = Mathf.Clamp(Random.Range(minY, maxY), lowerBounds.y, upperBounds.y);
            destination = new Vector2(x, y);
        }
    }
}
