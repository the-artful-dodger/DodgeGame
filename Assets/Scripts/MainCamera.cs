using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    public float EdgeBuffer = 70f;  // The border width at the screen edge in which the movement will work
    public float Speed = 15f;       // Speed of the camera movement

    float width;
    float height;
    Vector2 lowerBounds;
    Vector2 upperBounds;
    bool toggleCam;
    GameObject player;
    GameObject background;

    // Use this for initialization
    void Start()
    {
        // Start the camera centered on the player
        Vector2 PlayerPos = GameObject.Find("Player").transform.position;
        transform.position = new Vector3(PlayerPos.x, PlayerPos.y, transform.position.z);

        // Find the cameras height and width
        height = camera.orthographicSize;
        width = height * camera.aspect;

        // Get the background to use for the camera bounds
        background = GameObject.Find("Background");
        if (!background)
            Debug.LogError("Background could not be found!");

        player = GameObject.Find("Player");
        toggleCam = true;
    }

    // Update is called once per frame
    void Update()
    {/*
		if (Screen.orientation == ScreenOrientation.Portrait) {
			transform.position = new Vector3(0,0,20);
		} else {
			transform.position = new Vector3(0,0,-10);
		}*/
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Debug.Log("Going to main menu");
			Application.LoadLevel (0);
		}

        // Check if on the right edge
        if (Input.mousePosition.x >= Screen.width - EdgeBuffer &&
            Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y <= Screen.height &&
            Input.mousePosition.y >= 0)
            transform.position += Vector3.right * Time.deltaTime * Speed;

        // Check if on the left edge
        if (Input.mousePosition.x <= EdgeBuffer &&
            Input.mousePosition.x >= 0 &&
            Input.mousePosition.y <= Screen.height &&
            Input.mousePosition.y >= 0)
            transform.position += Vector3.left * Time.deltaTime * Speed;

        // Check if on the top edge
        if (Input.mousePosition.x >= 0 &&
            Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= Screen.height - EdgeBuffer &&
            Input.mousePosition.y <= Screen.height)
            transform.position += Vector3.up * Time.deltaTime * Speed;

        // Check if on the bottom edge
        if (Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.x >= 0 &&
            Input.mousePosition.y <= EdgeBuffer &&
            Input.mousePosition.y >= 0)
            transform.position += Vector3.down * Time.deltaTime * Speed;

        //camera move via CamH/CamV
        if (Input.GetAxis("CamH") > 0.2 || Input.GetAxis("CamH") < -0.2)
            transform.position += Input.GetAxis("CamH") * Vector3.right * Time.deltaTime * Speed * 0.6f;

        if (Input.GetAxis("CamV") > 0.2 || Input.GetAxis("CamV") < -0.2)
            transform.position += Input.GetAxis("CamV") * Vector3.up * Time.deltaTime * Speed * 0.6f;
    }

    void LateUpdate()
    {
        // Toggle camera
        //if (Input.GetButtonDown("Fire1"))
        //    toggleCam = !toggleCam;

        // Center on player
        if (Input.GetButton("Jump") || toggleCam == true)
        {
            // If cam joystick is centereed
            if (Input.GetAxis("CamH") < 0.2 &&
                Input.GetAxis("CamH") > -0.2 &&
                Input.GetAxis("CamV") < 0.2 &&
                Input.GetAxis("CamV") > -0.2)

                // Keep camera centered
                transform.position = new Vector3(player.transform.position.x,
                                                 player.transform.position.y,
                                                 transform.position.z);
            else
            {
                // Allow the camera to be moved a short distance using the joystick while the camera is locked.
                transform.position = new Vector3(player.transform.position.x + (1.5f * Input.GetAxis("CamH")),
                                                 player.transform.position.y + (1.5f * Input.GetAxis("CamV")),
                                                 transform.position.z);
            }
        }

        // Find the lower and upper bounds of the map
        lowerBounds = new Vector2(background.renderer.bounds.min.x, background.renderer.bounds.min.y);
        upperBounds = new Vector2(background.renderer.bounds.max.x, background.renderer.bounds.max.y);

        // Make sure the camera doesn't go further left or right than the size of the map
        if (transform.position.x - width <= lowerBounds.x)
            transform.position = new Vector3(lowerBounds.x + width, transform.position.y, transform.position.z);
        else if (transform.position.x + width >= upperBounds.x)
            transform.position = new Vector3(upperBounds.x - width, transform.position.y, transform.position.z);

        // Make sure the camera doesn't go further up or down than the size of the map
        if (transform.position.y + height >= upperBounds.y)
            transform.position = new Vector3(transform.position.x, upperBounds.y - height, transform.position.z);
        else if (transform.position.y - height <= lowerBounds.y)
            transform.position = new Vector3(transform.position.x, lowerBounds.y + height, transform.position.z);
    }
}
