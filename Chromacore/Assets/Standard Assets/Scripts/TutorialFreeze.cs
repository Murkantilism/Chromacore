using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class TutorialFreeze : MonoBehaviour {
	public bool paused = false;
	public AudioSource backgroundTrack;
	public GameObject teli;
	public GUIText pressAnyKeyText;
	
	public TextMesh tutorialText;

	// Ping sounds played when paused
	public AudioClip pingSound1;
	public AudioClip pingSound2;

	// Complete sounds played when key is pressed
	public AudioClip completeSound;

	// Use this for initialization
	void Start () {
		// Get teli game object
		teli = GameObject.Find("Teli");
		// Get teli's child object (to access Teli_Animation.cs)
		teli = teli.transform.FindChild("AnimatedSprite").gameObject;
		backgroundTrack = GameObject.Find("Main Camera").audio;

		pressAnyKeyText = GameObject.Find("tutorialResumeText").guiText;
		pressAnyKeyText.enabled = false;
		pressAnyKeyText.pixelOffset = new Vector2(-Screen.width / 6, -Screen.height  / 4);
	}
	
	// Update is called once per frame
	void Update () {
		// Resume the game once the user is ready and inputs anything
		if(Input.anyKey && paused == true){
			paused = false;
			Time.timeScale = 1;
			backgroundTrack.Play();
			teli.SendMessage("PauseMovement", false);
			pressAnyKeyText.enabled = false;
			audio.clip = completeSound;
			audio.Play();
		}
	}

	// Upon entering a tutorial freeze trigger, pause game
	void OnTriggerEnter(Collider col){
		Debug.Log("Tutorial Freeze Triggered");
		if(col.gameObject.tag == "Player"){
			// Pause everything
			paused = true;
			Time.timeScale = 0;
			backgroundTrack.Pause();
			teli.SendMessage("PauseMovement", true);

			// Play one of 2 tutorial ping sounds
			int i = UnityEngine.Random.Range(0, 2);
			if (i == 0){
				audio.clip = pingSound1;
			}else{
				audio.clip = pingSound2;
			}
			audio.Play();

			// Show press any key text
			pressAnyKeyText.enabled = true;

			// Turn the assigned billboard tutorial text on
			// (assigned in the Editor)
			tutorialText.renderer.enabled = true;
		}
	}
}