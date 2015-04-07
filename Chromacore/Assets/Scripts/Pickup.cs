using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	// Corresponding sound to the musical track attached to 
	// the object
	private AudioClip collectSound;
	GameObject scoringSystem;
	CircleCollider2D BallCollider;
	SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		BallCollider = GetComponent<CircleCollider2D> ();
		scoringSystem = GameObject.FindGameObjectWithTag ("ScoringSystem");
	}
	
	// Upon picking up this object, trigger events
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Teli") {
			// Make this object invisible
			Destroy(BallCollider);
			Destroy(spriteRenderer);
			Destroy(gameObject, 3f);
			
			scoringSystem.SendMessage ("UpdateScore");
			
			// Play the corresponding sound
			GetComponent<AudioSource>().Play();
		}
	}
}
