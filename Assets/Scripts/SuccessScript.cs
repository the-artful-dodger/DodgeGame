using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]

public class SuccessScript : MonoBehaviour
{
	static int SuccessCount = 0;
	static GUIText counter;
	void Start(){
		counter = this.GetComponent<GUIText> ();
		counter.text = "Touch Downs: " + SuccessCount;
	}
	
	public static void IncrementSuccess()
	{
		SuccessCount++;
		counter.text = "Touch Downs: " + SuccessCount;
		Spawner.IncrementSpawnCount (SuccessCount);
		//Debug.Log(SuccessCount);
	}
}
