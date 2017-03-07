using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]

public class DeathScript : MonoBehaviour
{
    static int DeathCount = 0;
	static GUIText counter;

	void Start(){
		counter = this.GetComponent<GUIText> ();
		counter.text = "Deaths: " + DeathCount;
	}
	void Awake(){
	}

    public static void IncrementDeath()
    {
        DeathCount++;
		counter.text = "Deaths: " + DeathCount;
    }
}
