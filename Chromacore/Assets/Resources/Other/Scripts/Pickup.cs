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
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Teli")
		{
			Debug.Log ("The player took a mistic ball");

			col.gameObject.SendMessage("CollectMisticBall");

			// Make this object invisible
			Destroy(gameObject, 3f);
			Destroy(GetComponent<CircleCollider2D>());
			Destroy(GetComponent<SpriteRenderer>());

			// Play the corresponding sound
			GetComponent<AudioSource>().Play();
		}
	}
		
}
