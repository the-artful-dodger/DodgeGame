using UnityEngine;
using System.Collections;

public class GUIHandler : MonoBehaviour {

	public GameObject soundManager;

	public enum MyButton{
		play,
		music,
		help,
		exit
	}
	public MyButton button;
	public Texture alternateTexture = null;

	//private bool isMusicPlaying = true;
	private Texture originalTexture = null;

	void Start(){
	}

	void Awake(){
		originalTexture = this.gameObject.guiTexture.texture;
	}
	void OnMouseDown(){
		if(button == MyButton.play){
			Application.LoadLevel("Level01");
		}else if (button == MyButton.music){
			Sounds.isMusicOn = !Sounds.isMusicOn;
		} else if(button == MyButton.exit){
			Application.Quit();
			Debug.Log("Exiting");
		} else if(button == MyButton.help) {
			
		}
	}
	void FixedUpdate(){
		if (button == MyButton.music) {
						if (Sounds.isMusicOn) {
								this.gameObject.guiTexture.texture = originalTexture;
						} else {
								this.gameObject.guiTexture.texture = alternateTexture;
						}
				}
	}
}
