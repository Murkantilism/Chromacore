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
		backgroundTrack = GameObject.Find("Main Camera").audio;
	}
	
	// Update is called once per frame
	void Update () {
		// Check for pause on PC
		if (Input.GetKeyDown(KeyCode.Escape)) {
			TriggerPause();
		}

		// Check for swipe on mobile. Ignore more or less than 1 finger input
		/*if (Input.touches.Length != 1){
			return;
		}*/

		int fingerCnt = 0; // Initialize finger counter

		// For each touch that is detected
		foreach(Touch touch in Input.touches){

			// Set the starting Vector to starting position
			if(touch.phase == TouchPhase.Began){
				swipeStart = touch.position;
			}

			// If the touch hasn't ended yet, increment counter
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled){
				fingerCnt++;

				// If one finger is touching and the touch is still going or has ended
				if(fingerCnt == 1 && touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended){
					// Subtract starting position from the current touch position, or if the
					// touch has ended from the last touch position, and normalize the vector
					Vector2 touchFacing  = (swipeStart - touch.position).normalized;

					if(Vector2.Dot(touchFacing, Vector2.up) > 0.8 && Vector2.Distance(swipeStart, touch.position) > 20){
						// This is swipe Down
						TriggerPause();
					}

					if(Vector2.Dot(touchFacing, -Vector2.up) > 0.8 && Vector2.Distance(swipeStart, touch.position) > 20){
						// This is swipe Up
						TriggerPause();
					}

					if(Vector2.Dot(touchFacing, Vector2.right) > 0.8 && Vector2.Distance(swipeStart, touch.position) > 20){
						// This is swipe Left
						TriggerPause();
					}

					if(Vector2.Dot(touchFacing, -Vector2.right) > 0.8 && Vector2.Distance(swipeStart, touch.position) > 20){
						// This is swipe Right
						TriggerPause();
					}
				}
			}
		}

		/*
		// At the start of a swipe, save the position
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
			swipeStart = Input.GetTouch(0).position;
		}
		// At the end of a swipe, save the position
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended){
			swipeEnd = Input.GetTouch(0).position;
			CalcSwipeLength();
		}

		// Check for pause on mobile : Check that finger(s) are not stationary,
		// check that the finger(s) are in movement.
		if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Stationary && Input.GetTouch(0).phase == TouchPhase.Moved){
			// Check if they are within the swipe threshold to actually trigger pause
			if (CalcSwipeLength().x > swipeThresh.x && CalcSwipeLength().y > swipeThresh.y &&
			    CalcSwipeLength().x < swipeCeiling.x && CalcSwipeLength().y < swipeCeiling.y){
				Debug.Log(CalcSwipeLength());
				TriggerPause();
			}
		}*/
	}

	void TriggerPause(){
		paused = true;
		Time.timeScale = 0;
		backgroundTrack.Pause();
		teli.SendMessage("PauseMovement", true);
	}

	/*
	// Calculate the length of a swipe
	Vector2 CalcSwipeLength(){
		return new Vector2(Mathf.Abs((swipeEnd.x - swipeStart.x)), Mathf.Abs(swipeEnd.y - swipeStart.y));
	}*/
	
	// show menu when paused
	void OnGUI() {
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		GUI.skin = guiSkin;
		
		if (paused) {
			// If paused, set black pause GUI texture
			blackPauseTexture.color = new Color(0, 0, 0, 1);

			// Resume button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Resume", buttonStyle)){
				// If unpaused, set black pause GUI texture
				blackPauseTexture.color = new Color(0, 0, 0, 0);
				paused = false;
				Time.timeScale = 1;
				backgroundTrack.Play();
				teli.SendMessage("PauseMovement", false);
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
