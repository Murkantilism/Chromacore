using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	
	public bool paused = false;
	public GUITexture blackPauseTexture;
	public GUISkin guiSkin;
	public AudioSource backgroundTrack;
	public GameObject teli;
	public bool mobileP = false;
	bool respawningP = false;

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
				TriggerPause(false);
			}

			// Check for Pause in general
			if (Input.GetAxis("Pause") != 0){
				TriggerPause(false);
			}
		}
	}

	// Used to trigger pause from local Update() or Teli_Animation.cs
	void TriggerPause(bool boool){
		paused = true;
		respawningP = boool;
		Time.timeScale = 0;
		backgroundTrack.Pause();
		teli.SendMessage("PauseMovement", true);
	}

	// Used to trigger unpause exclusively from Teli_Animation.cs
	void Unpause(){
		Invoke("UnpauseHelper", 6);
	}

	void UnpauseHelper(){
		// Wait 4 seconds for respawn animation to finish
		//yield return new WaitForSeconds(4);
		
		Debug.Log("Unpause");
		// Unpause
		paused = false;
		Time.timeScale = 1;
		backgroundTrack.Play();
		teli.SendMessage("PauseMovement", false);

	}

	// Show menu when paused
	void OnGUI() {
		GUI.backgroundColor = Color.magenta;

		GUI.skin = guiSkin;
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fontSize = 60;

		GUIStyle quitStyle = new GUIStyle("button");
		quitStyle.fontSize = 40;

		GUIStyle printStyle = new GUIStyle("label");
		printStyle.fontSize = 40;

		// If we are not already paused and on mobile
		if (paused == false && mobileP == true){
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 270, 150), "Pause", buttonStyle)){
				TriggerPause(false);
			}
		}
		
		if (paused == true && respawningP == false) {
			GUI.Label(new Rect(Screen.width/2, Screen.height/2, 500, 200), "Press 'P' to save screenshot \n to application data path", printStyle);

			// If paused, set black pause GUI texture
			blackPauseTexture.color = new Color(0, 0, 0, 1);

			// Resume button
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 270, 150), "Resume", buttonStyle)){
				paused = false;
				Time.timeScale = 1;
				backgroundTrack.Play();
				teli.SendMessage("PauseMovement", false);
				blackPauseTexture.color = new Color(0, 0, 0, 0);
			}

			// Quit button
			if (GUI.Button(new Rect(Screen.width/2, Screen.height/2 + Screen.height/4, 270, 150), "Quit to \nMain Menu", quitStyle)){
				Application.LoadLevel("MainMenu");
				paused = false;
				Time.timeScale = 1;
			}
		}
    }
}