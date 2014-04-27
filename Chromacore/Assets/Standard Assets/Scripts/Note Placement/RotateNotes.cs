using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// Used to assign Notes a random Note animation to give them variety
public class RotateNotes : MonoBehaviour {

	// The animation for this Note
	public tk2dSpriteAnimator noteAnimation;

	// A list of possible animations
	tk2dSpriteAnimationClip[] listOfClips;


	// Use this for initialization
	void Start () {
		// Get the animator attached to this Note
		noteAnimation = GetComponent<tk2dSpriteAnimator>();
		// Get the list of possible animations
		listOfClips = noteAnimation.Library.clips;

		AssignAnimation();
	}

	// Assign each Note a random animation
	void AssignAnimation(){
		int randomNum = UnityEngine.Random.Range(0, listOfClips.Length);
		noteAnimation.Play(listOfClips[randomNum]);
	}
}
