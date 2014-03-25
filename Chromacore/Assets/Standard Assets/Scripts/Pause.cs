using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUISkin guiSkin;
	public AudioSource backgroundTrack;
	public GameObject teli;
	public bool mobileP = false;

	// Use this for initialization
	void Start () {
		// Get teli game object
		teli = GameObject.Find("Teli");
		// Get teli's child object (to access Teli_Animation.cs)
		teli = teli.transform.FindChild("AnimatedSprite").gameObject;
		backgroundTrack = GameObject.Find("Main Camera").transform.Find("Listener").audio;

		guiSkin = Resources.Load("customBtnSkin") as GUISkin;

		#if UNITY_STANDALONE
		mobileP = false;
		#endif
		
		#if UNITY_IPHONE
		mobileP = true;
		#endif
		
		#if UNITY_ANDROID
		mobileP = true;
		#endif
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
		GUI.backgroundColor = Color.magenta;

		GUI.skin = guiSkin;
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fontSize = 60;

		GUIStyle quitStyle = new GUIStyle("button");
		quitStyle.fontSize = 40;

		// If we are not already paused and on mobile
		if (paused == false && mobileP == true){
			// Draw a Pause GUI button
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
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 + Screen.height/4, 200, 100), "Quit to \nMain Menu", quitStyle)){
				Application.LoadLevel("MainMenu");
				paused = false;
				Time.timeScale = 1;
			}
		}
    }
}