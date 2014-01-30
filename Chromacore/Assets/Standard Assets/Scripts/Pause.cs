using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUISkin guiSkin;
	public AudioSource backgroundTrack;
	public GameObject teli;

	// Used for swipe input
	float swipespeed;
	float swipeTresh = 1.2f;
	Vector2 speedVec = Vector2.zero;
	Touch myTouch = Input.touches[0];

	// Use this for initialization
	void Start () {
		// Get teli game object
		teli = GameObject.Find("Teli");
		// Get teli's child object (to access Teli_Animation.cs)
		teli = teli.transform.FindChild("AnimatedSprite").gameObject;
		backgroundTrack = GameObject.Find("Main Camera").audio;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Check for pause on PC
		if (Input.GetKeyDown(KeyCode.Escape)) {
			paused = true;
			Time.timeScale = 0;
			backgroundTrack.Pause();
			teli.SendMessage("PauseMovement", true);
		}

		// Check for pause on mobile by checking for swipe and less
		// than 2 fingers.
		if (Input.touchCount > 0 && Input.touchCount < 2 && Input.GetTouch(0).phase == TouchPhase.Moved){
			// Calculate the swipe vector and speed
			speedVec = new Vector2(myTouch.deltaPosition.x * myTouch.deltaTime, myTouch.deltaPosition.y * myTouch.deltaTime);
			swipespeed = speedVec.magnitude;
			Debug.Log("Swipe speed: " + speedVec.magnitude);

			// Compare against threshold
			if (swipespeed > swipeTresh){
				paused = true;
				Time.timeScale = 0;
				backgroundTrack.Pause();
				teli.SendMessage("PauseMovement", true);
				Debug.Log("SWIPER NO SWIPING");
			}
		}
	}
	
	// show menu when paused
	void OnGUI() {
		GUI.skin = guiSkin;
		
		if (paused) {
			// If paused, set black pause GUI texture
			blackPauseTexture.color = new Color(0, 0, 0, 1);

			// Resume button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Resume")){
				// If unpaused, set black pause GUI texture
				blackPauseTexture.color = new Color(0, 0, 0, 0);
				paused = false;
				Time.timeScale = 1;
				backgroundTrack.Play();
				teli.SendMessage("PauseMovement", false);
			}

			// Quit button
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 + Screen.height/4, 200, 100), "Quit to Main Menu")){
				Application.LoadLevel("MainMenu");
				paused = false;
				Time.timeScale = 1;
			}
		}
    }
}
