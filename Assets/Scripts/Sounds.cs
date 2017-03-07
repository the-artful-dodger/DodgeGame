using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	AudioSource fxSound;

	public AudioClip backMusic; 
	public AudioClip deathEffect;
	public AudioClip pickupEffect;
	public AudioClip safezoneEffect;
	public AudioClip levelUpEffect;

	public static bool isMusicOn = true;
	
	// Use this for initialization
	void Start () {
		fxSound = GetComponent<AudioSource> ();
		fxSound.clip = backMusic;
		fxSound.Play ();
	}
	public void levelUp(){
		fxSound.PlayOneShot (levelUpEffect);
		if(fxSound.pitch < 1.19f)
			fxSound.pitch += .02f;
	}
	public void death(){
		fxSound.PlayOneShot (deathEffect);
	}
	public void pickup(){
		fxSound.PlayOneShot (pickupEffect);
	}
	public void enterSafeZone(){
		fxSound.PlayOneShot (safezoneEffect);
	}

	void FixedUpdate(){
		fxSound.mute = !isMusicOn;
	}
}
