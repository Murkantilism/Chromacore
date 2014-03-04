using UnityEngine;
using System;
using System.Collections;

// Used to change tutorial text based on platform
public class Tutorial_Text : MonoBehaviour {
	public TextMesh tutorialTextJump;
	public TextMesh tutorialTextPunch;
	public TextMesh tutorialTextPause;

	// Use this for initialization
	void Start () {
		// Determine Platform at compile time

		#if UNITY_STANDALONE
		tutorialTextJump.text = "Press 'space' to jump";
		tutorialTextPunch.text = "Press 'A' to punch";
		tutorialTextPause.text = "Press 'ESC' to pause";
		#endif
		
		#if UNITY_IPHONE
		tutorialTextJump.text = "Tap screen to jump";
		tutorialTextPunch.text = "2 fingers to punch";
		tutorialTextPause.text = "3 fingers to pause";
		#endif

		#if UNITY_ANDROID
		// If joystick input exists, show joystick specific tutorial text
		try{
			if ((Input.GetJoystickNames().Length > 0)){
				tutorialTextJump.text = "Press A to jump";
				tutorialTextPunch.text = "Press X to punch";
				tutorialTextPause.text = "Press B to pause";
			}
		// Otherwise assume it's a touchscreen
		}catch(Exception e){
			Debug.Log(e.ToString());
			tutorialTextJump.text = "Tap screen to jump";
			tutorialTextPunch.text = "2 fingers to punch";
			tutorialTextPause.text = "3 fingers to pause";
		}
		#endif
	}

	// Update is called once per frame
	void Update () {
	
	}
}
