using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUISkin guiSkin;
	public AudioSource backgroundTrack;
	public GameObject teli;

	Vector2 swipeStart;
	Vector2 swipeEnd;
	Vector2 swipeThresh = new Vector2(20, 20);
	Vector2 swipeCeiling = new Vector2(600, 600);

	// Use this for initialization
	void Start () {
		// Get teli game object
		teli = GameObject.Find("Teli");
		// Get teli's child object (to access Teli_Animation.cs)
		teli = teli.transform.FindChild("AnimatedSprite").gameObject;
		backgroundTrack = GameObject.Find("Main Camera").transform.Find("Listener").audio;
	}
	
	// Update is called once per frame
	void Update () {
		// If the game is not already Paused
		if (paused == false){
			// Check for pause on PC
			if (Input.GetKeyDown(KeyCode.Escape)) {
				TriggerPause();
			}

			// Check for Pause in general
			if (Input.GetAxis("Pause") != 0){
				TriggerPause();
			}
		}
	}

	void TriggerPause(){
		paused = true;
		Time.timeScale = 0;
		backgroundTrack.Pause();
		teli.SendMessage("PauseMovement", true);
	}

	// Show menu when paused
	void OnGUI() {
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		GUI.skin = guiSkin;

		// If we are not already paused
		if (paused == false){
			// Show a Pause GUI button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Pause", buttonStyle)){
				TriggerPause();
			}
		}
		
		if (paused == true) {
			// If paused, set black pause GUI texture
			blackPauseTexture.color = new Color(0, 0, 0, 1);

			// Resume button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Resume", buttonStyle)){
				Time.timeScale = 1;
				paused = false;
				backgroundTrack.Play();
				teli.SendMessage("PauseMovement", false);
				blackPauseTexture.color = new Color(0, 0, 0, 0);
			}

			// Quit button
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 + Screen.height/4, 200, 100), "Quit to \nMain Menu", buttonStyle)){
				Application.LoadLevel("MainMenu");
				paused = false;
				Time.timeScale = 1;
			}
		}
    }
}
