using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, Buffable
{
    public float MoveSpeed = 8f;
	public float buffDecayTime = 5f;
	public GameObject soundManager;
    
	private Vector2 target;
	private bool isSafe = true;
	private Vector2 upperBounds;
	private Vector2 lowerBounds;
	private Buffable buffable;
	private DoubleSpeedBuff doubleSpeedBuff;
	private HalfSpeedBuff halfSpeedBuff;
	private float originalSpeed;
	private float originalSize;
	private Pickup.PickupType currentPickup = Pickup.PickupType.none;
	private Sprite originalSprite;
	private SpriteRenderer spriteRenderer;
	private Vector3 lastSafeZone;
	private Vector3 start;
	private Sounds soundEffect;

    Vector2 Buffable.Destination
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
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
        target = transform.position;
		start = transform.position;

        GameObject background = GameObject.Find("Background");
        if (!background)
            Debug.LogError("Background could not be found.");

        upperBounds = background.renderer.bounds.max;
        lowerBounds = background.renderer.bounds.min;

		originalSpeed = MoveSpeed;
		originalSize = transform.localScale.x;
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		originalSprite = spriteRenderer.sprite;

		soundEffect = soundManager.GetComponent<Sounds> ();

		buffable = (Buffable)GetComponent(typeof(Buffable));
		doubleSpeedBuff = this.gameObject.GetComponent<DoubleSpeedBuff>();
		halfSpeedBuff = this.gameObject.GetComponent<HalfSpeedBuff>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") || Input.touchCount > 0)
			if(Application.platform == RuntimePlatform.Android)
				target = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			else
            	target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        else
        {
            if (Input.GetAxis("Vertical") >= 0.2 || Input.GetAxis("Vertical") <= -0.2)
                target.y = Input.GetAxis("Vertical") * .2f + transform.position.y;
            if (Input.GetAxis("Horizontal") >= 0.2 || Input.GetAxis("Horizontal") <= -0.2)
                target.x = Input.GetAxis("Horizontal") * .2f + transform.position.x;
        }

        transform.position = Vector2.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Keep player on the map
        if (transform.position.x > upperBounds.x)
            transform.position = new Vector2(upperBounds.x, transform.position.y);
        else if (transform.position.x < lowerBounds.x)
            transform.position = new Vector2(lowerBounds.x, transform.position.y);

        if (transform.position.y > upperBounds.y)
            transform.position = new Vector2(transform.position.x, upperBounds.y);
        else if (transform.position.y < lowerBounds.y)
            transform.position = new Vector2(transform.position.x, lowerBounds.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            SafeZone safeZone = (SafeZone)other.gameObject.GetComponent(typeof(SafeZone));
            safeZone.Entered();
            isSafe = true;
			lastSafeZone = other.transform.position;
			soundEffect.enterSafeZone();
        }

        if (other.gameObject.name == "SafeZone Top")
        {
            //Application.LoadLevel(Application.loadedLevel);
			soundEffect.enterSafeZone();
			Success();
        }

        if (other.gameObject.tag == "Enemy" && !isSafe)
        {
            Dead();
            //Application.LoadLevel(Application.loadedLevel);
        }
		if(other.gameObject.tag == "Pickup"){
			soundEffect.pickup();
			Debug.Log(other.gameObject.GetComponent<Pickup>().pickup);
			if(other.gameObject.GetComponent<Pickup>().pickup == Pickup.PickupType.speedUp 
			   && currentPickup!=Pickup.PickupType.speedUp){
				doubleSpeedBuff.ApplyBuff();
				currentPickup = Pickup.PickupType.speedUp;
			}
			if(other.gameObject.GetComponent<Pickup>().pickup == Pickup.PickupType.speedDown
			   && currentPickup!=Pickup.PickupType.speedDown){
				halfSpeedBuff.ApplyBuff();
				currentPickup = Pickup.PickupType.speedDown;
			}
			Invoke("RemoveBuffs",buffDecayTime);
			Destroy(other.gameObject);
		}
    }

	void RemoveBuffs()
	{
		buffable.Speed = originalSpeed;
		buffable.Size = originalSize;
		currentPickup = Pickup.PickupType.none;
		spriteRenderer.sprite = originalSprite;
		Debug.Log("Buffs removed");
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SafeZone")
        {
            SafeZone safeZone = (SafeZone)other.gameObject.GetComponent(typeof(SafeZone));
            safeZone.Left();
            isSafe = false;
        }
    }
	void Success(){
		transform.position = start;
		target = start;
		SuccessScript.IncrementSuccess ();
		RemoveBuffs ();
		soundEffect.levelUp ();
	}
    void Dead()
    {
		transform.position = lastSafeZone;
		target = lastSafeZone;
        DeathScript.IncrementDeath();
		RemoveBuffs ();
		soundEffect.death ();
    }
}
