using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour {

	public GameObject[] pickupList;
	public float radius = 20; 
	public float maxNumberOfPickups = 10;
	public float minNumberOfPickups = 5;
	public GameObject player;

	void Start(){

		int numberOfPickups = (int)Random.Range(minNumberOfPickups,maxNumberOfPickups+1f);
		Instantiate (pickupList [0], player.transform.position, Quaternion.identity);
		for(int i = 0; i < numberOfPickups; i++){
			Vector2 position = Random.insideUnitCircle*radius;
			GameObject obj = pickupList[Random.Range(0,pickupList.Length)];
			Instantiate(obj,new Vector3(position.x,position.y,1f),Quaternion.identity);
		}
		//player.GetComponent<Player> ().RemoveBuffs ();
	}
}
