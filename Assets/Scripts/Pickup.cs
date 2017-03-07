using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public enum PickupType{
		none,
		speedUp,
		speedDown,
		//TODO: Implement more buffs
	}
	public PickupType pickup;
}
