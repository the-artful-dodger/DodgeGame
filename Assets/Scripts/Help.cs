using UnityEngine;
using System.Collections;

public class Help : MonoBehaviour {
	public float fadeDuration = 3.0f;
	
	private void Start ()
	{
		StartCoroutine (StartFading ());
	}
	
	private IEnumerator StartFading()
	{
		yield return StartCoroutine(Fade(0.0f, 1.0f, fadeDuration));
		//yield return StartCoroutine(Fade(1.0f, 0.0f, fadeDuration));
		//Destroy(gameObject);
	}
	static float t = 0.0f;
	
	private IEnumerator Fade (float startLevel, float endLevel, float time)	{

			while (guiText.material.color.a > 0.0f){
			Color c = guiText.color;
				c.a -= 0.1f * Time.deltaTime * 2.0f;
			this.GetComponent<GUIText>().color = c;
				yield return 0;
			}
			Destroy (gameObject);
	}
}
