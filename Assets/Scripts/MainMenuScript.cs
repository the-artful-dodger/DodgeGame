using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Texture))]
public class MainMenuScript : MonoBehaviour
{
    public Texture BannerTexture;
    public Texture PlayTexture;
    public Texture OptionsTexture;
    public Texture QuitTexture;

    delegate void GUIMethod();
    GUIMethod currentGUIMethod;

    void Start()
    {
        if (!PlayTexture || !OptionsTexture || !QuitTexture)
            Debug.LogError("Button texture is missing!");

        currentGUIMethod = MainMenu;
    }

    void MainMenu()
    {
        int w = Screen.width;
        int h = Screen.height;

        // Draw banner
        GUI.DrawTexture(new Rect(w * .380f,     // Horizontal Position
                                 h * .010f,     // Vertical Position
                                 w * .200f,     // Width
                                 h * .200f),    // Height
                                 BannerTexture);

        // Create buttons
        bool PlayButton = GUI.Button(new Rect(w * .250f,    // Horizontal Position
                                              h * .333f,    // Vertical Position
                                              w * .105f,    // Width
                                              h * .092f),   // Height
                                              PlayTexture);

        bool OptionsButton = GUI.Button(new Rect(w * .250f,     // Horizontal Position
                                                 h * .500f,     // Vertical Position
                                                 w * .200f,     // Width
                                                 h * .092f),    // Height
                                                 OptionsTexture);

        bool QuitButton = GUI.Button(new Rect(w * .250f,    // Horizontal Position
                                              h * .600f,    // Vertical Position
                                              w * .100f,    // Width
                                              h * .100f),   // Height
                                              QuitTexture);

        // Handle button presses
        if (PlayButton)
            currentGUIMethod = Play;

        if (OptionsButton)
            currentGUIMethod = Options;

        if (QuitButton)
            currentGUIMethod = Quit;
    }

    void Play()
    {
        Application.LoadLevel(1);
    }

    void Options()
    {
        Application.LoadLevel(0);
    }

    void Quit()
    {
        Application.Quit();
    }

    public void OnGUI()
    {
        this.currentGUIMethod();
    } 
}
