using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	// Corresponding sound to the musical track attached to 
	// the object
	private AudioClip collectSound;
	GameObject scoringSystem;

	// Use this for initialization
	void Start () {
		scoringSystem = GameObject.FindGameObjectWithTag ("ScoringSystem");
	}
	
	// Upon picking up this object, trigger events
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Teli")
		{
			// Make this object invisible
			Destroy(GetComponent<CircleCollider2D>());
			Destroy(GetComponent<SpriteRenderer>());
			Destroy(gameObject, 3f);

			scoringSystem.SendMessage ("UpdateScore");

			// Play the corresponding sound
			GetComponent<AudioSource>().Play();
		}
	}
		
}
