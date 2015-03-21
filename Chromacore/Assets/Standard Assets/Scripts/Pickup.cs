using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	// Corresponding sound to the musical track attached to 
	// the object
	private AudioClip collectSound;

	// Use this for initialization
	void Start () {

	}
	
	// Upon picking up this object, trigger events
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Teli")
		{
			Debug.Log ("The player took a mistic ball");

			col.gameObject.SendMessage("CollectMisticBall");

			// Make this object invisible
			GetComponent<Renderer>().enabled = false;
			// Play the corresponding sound
			GetComponent<AudioSource>().Play();
		}
	}
		
}
