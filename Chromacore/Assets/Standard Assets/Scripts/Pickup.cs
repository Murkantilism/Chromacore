using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Pickup : MonoBehaviour {
	
	// Corresponding sound to the musical track attached to 
	// the object
	private AudioClip collectSound;
	
	// Get the parent of this collectible
	GameObject parent;
	
	public GameObject particleEffect;
	
	// The animation for this Note
	public tk2dSpriteAnimator noteAnimation;
	
	// A list of possible animations
	tk2dSpriteAnimationClip[] listOfClips;
	
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;

		// Get the animator attached to this Note
		noteAnimation = GetComponent<tk2dSpriteAnimator>();
		// Get the list of possible animations
		listOfClips = noteAnimation.Library.clips;

		particleEffect = GameObject.Find("NotePickupParticleEffect");
		
		AssignAnimation();
	}

	// Assign each Note a random animation
	void AssignAnimation(){
		int randomNum = UnityEngine.Random.Range(0, listOfClips.Length);
		noteAnimation.Play(listOfClips[randomNum]);
	}
	
	// Upon picking up this object, trigger events
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player")
		{
			// Move the particle effect to this location
			particleEffect.transform.position = gameObject.transform.position;
			
			// Change the color of the textures on pickup
			parent.SendMessage("ChangeColor");
			// A note has been collected so increment the score
			col.SendMessage("CollectNote");
			// Make this object invisible
			gameObject.renderer.enabled = false;
			// Play the corresponding sound
			audio.Play();
		}
	}
		
}
