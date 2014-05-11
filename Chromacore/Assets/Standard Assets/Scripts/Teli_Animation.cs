using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[AddComponentMenu("2D Toolkit/Sprite/tk2dSpriteAnimator")]
public class Teli_Animation : MonoBehaviour {
	// Spawn point at beginning of level
	public GameObject spawn;
	public GameObject originalSpawn;
	
	// The main character's Charcter Controller	
	public CharacterController teliCharacter;

	// Player Character Animation GameObject (used to scale)
	GameObject pcAnimationGO;

	public bool scarfUnlockedp = false;

	public bool skullKidUnlockedp = false;

	public bool scarfEquippedp = false;

	public bool skullKidEquippedp = false;

	public bool teliEquippedp = true;


	public Camera mainCamera;
	
	// Value of character's Y velocity to begin falling animation at
	private float fallAnimVel = -0.25f;
	
	// Link to animated sprite
	private tk2dSpriteAnimator anim;
	
	// State variable to check if player is running (running by default)
	public bool runningP = true;

	// Check if game is paused
	bool gamePaused = false;

	// Check if Teli is punching
	bool punchingP = false;

	// Used to wait 1 sec after game is resumed before checking death condition again
	bool waitResume = false;
	
	// The background music track (used to reset after death)
	public AudioSource backgroundTrack;
	
	// An array of notes
	public GameObject[] Notes;
	
	// The latest timestamp to reset music track at right checkpoint
	float checkpoint_timestamp;

	public GUISkin guiSkin;

	// Are we on a mobile platform?
	bool mobileP = false;

	// Is this finger Touch valid?
	bool validTouch = false;

	// Is Teli dead?
	bool deadp = false;

	// Should we reset?
	bool resetp = false;

	// The y threshold used for detecting if Teli is below level
	int levelthresholdY = -5;

	// Used to offset GUI buttons
	private RectOffset rctOff;

	tk2dSpriteAnimationClip repspawnClip;

	GameObject pauseSystem_go;

	GameObject exclamationPoint;

	string skullKidPurchasedTxt = "skullKidPurchasedTxt.txt";
	string scarfPurchasedTxt = "scarfPurchasedTxt.txt";
	
	string skullKidEquippedTxt = "skullKidEquippedTxt.txt";
	string scarfEquippedTxt = "scarfEquippedTxt.txt";
	
	string teliEquippedTxt = "teliEquippedTxt.txt";

	// Use this for initialization
	void Start () {
		// This script must be attached to the sprite to work
		anim = GetComponent<tk2dSpriteAnimator>();
		
		Notes = GameObject.FindGameObjectsWithTag("Note");

		backgroundTrack = GameObject.Find("Main Camera").transform.Find("Listener").audio;

		guiSkin = Resources.Load("customBtnSkin") as GUISkin;

		mainCamera = GameObject.FindObjectOfType<Camera>() as Camera;

		pauseSystem_go = GameObject.Find("PauseSystem");

		repspawnClip = anim.GetClipByName("Respawning");

		exclamationPoint = GameObject.Find("exclamationPoint");

		exclamationPoint.renderer.enabled = false;

		pcAnimationGO = GameObject.Find("AnimatedSprite");

		// Read all of the text files for cosmetic skins
		ReadSkinsTextFiles();

		// Depending on which skins are unlocked and equipped, change
		// player character's sprite animation library and resize transform
		if(scarfUnlockedp && scarfEquippedp && teliEquippedp == false){
			anim.Library = Resources.Load("Scarf_Animation", typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
			pcAnimationGO.transform.localScale = new Vector3(0.6f, 0.6f, 1);
		}else if(skullKidUnlockedp && skullKidEquippedp && teliEquippedp == false){
			anim.Library = Resources.Load("Skull_Kid_Animation", typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
			pcAnimationGO.transform.localScale = new Vector3(0.5f, 0.5f, 1);
			// Move the skull kid animation back in X direction slightly
			pcAnimationGO.transform.localPosition = new Vector3(-1.525f, -1.22f, 0f);
		}else{
			anim.Library = Resources.Load("Teli_Animations", typeof(tk2dSpriteAnimation)) as tk2dSpriteAnimation;
			pcAnimationGO.transform.localScale = new Vector3(0.15f, 0.15f, 1);
		}

		if(Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level17"){
			levelthresholdY = -15;
		}

		#if UNITY_STANDALONE
		mobileP = false;
		#endif
		
		#if UNITY_IPHONE
		mobileP = true;
		#endif
		
		#if UNITY_ANDROID
		mobileP = true;
		#endif

		// Wait to invoke death animation function until a few
		// seconds after game has begun.
		InvokeRepeating("DeathAnimation", 3, 0.1f);
		//DeathAnimation();
	}

	// Read the text files to find out which skins are unlocked and equipped
	void ReadSkinsTextFiles(){

		// Read skull kid text files
		try{
			// Create an instance of StreamReader to read text file
			StreamReader srSkullKid = new StreamReader(Application.persistentDataPath + "\\" + skullKidPurchasedTxt);
			string line = srSkullKid.ReadToEnd();

			Debug.Log(line);

			// If the text file reads true, set boolean flag to true for this skin
			if(line == "true"){
				skullKidUnlockedp = true;
			}else{
				skullKidUnlockedp = false;
			}
			
		}catch(Exception e){
			Debug.Log(e.ToString());
		}

		try{
			// Create an instance of StreamReader to read text file
			StreamReader srSkullKid_equipped = new StreamReader(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
			string line = srSkullKid_equipped.ReadToEnd();

			// If the text file reads true, set boolean flag to true for this skin
			if(line == "true"){
				skullKidEquippedp = true;
			}else{
				skullKidEquippedp = false;
			}
			
		}catch(Exception e){
			Debug.Log(e.ToString());
		}


		// Read scarf text files
		try{
			// Create an instance of StreamReader to read text file
			StreamReader srScarf = new StreamReader(Application.persistentDataPath + "\\" + scarfPurchasedTxt);
			string line = srScarf.ReadToEnd();

			// If the text file reads true, set boolean flag to true for this skin
			if(line == "true"){
				scarfUnlockedp = true;
			}else{
				scarfUnlockedp = false;
			}
			
		}catch(Exception e){
			Debug.Log(e.ToString());
		}

		try{
			// Create an instance of StreamReader to read text file
			StreamReader srScarf_equipped = new StreamReader(Application.persistentDataPath + "\\" + scarfEquippedTxt);
			string line = srScarf_equipped.ReadToEnd();

			// If the text file reads true, set boolean flag to true for this skin
			if(line == "true"){
				scarfEquippedp = true;
			}else{
				scarfEquippedp = false;
			}
			
		}catch(Exception e){
			Debug.Log(e.ToString());
		}


		// Read Teli text files
		try{
			// Create an instance of StreamReader to read text file
			StreamReader srTeli_equipped = new StreamReader(Application.persistentDataPath + "\\" + teliEquippedTxt);
			string line = srTeli_equipped.ReadToEnd();

			// If the text file reads true, set boolean flag to true for this skin
			if(line == "true"){
				teliEquippedp = true;
			}else{
				teliEquippedp = false;
			}
			
		}catch(Exception e){
			Debug.Log(e.ToString());
		}


	}
		
	// Update is called once per frame
	void Update () {
		// Jump Animation
		if (Input.GetAxis("Jump") != 0){
			// Only play the clip if it is not already playing.
			// Calling play will restart the clip if it is already playing
			if (!anim.IsPlaying("Jump") && punchingP == false && !anim.IsPlaying("Punch") && gamePaused == false && deadp == false){
				anim.Play("Jump");
			}
		}
		
		// If the Player presses the punch button
		if (Input.GetAxis("Punch") != 0){
			// And the charcter isn't currently punching
			if(!anim.IsPlaying("Punch") && deadp == false){
				anim.Play("Punch");
			}
		}

		// Touch screen support:

		// Detect the number of fingers touching screen
		int fingerCount = 0;
		foreach(Touch touch in Input.touches){
			if(touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled){
				fingerCount++;
			}

			// If the finger touch is in the top 3/4 of the screen, it's valid
			if (touch.position.y > (Screen.height / 4)){
				validTouch = true;
			}else{
				validTouch = false; // Otherwise, it's not
			}// This is to avoid the Punch and Pause GUI buttons from triggering jump anim.
		}

		// Check to make sure non-zero fingers are touching & Teli isn't already punching
		if (fingerCount > 0 && Input.GetTouch(0).phase != TouchPhase.Moved){
			// If one finger is touching, jump
			if (fingerCount == 1){
				if (!anim.IsPlaying("Jump") && punchingP == false && !anim.IsPlaying("Punch") && gamePaused == false && validTouch == true && deadp == false){
					anim.Play("Jump");
				}
			}
		}
		
		// If none of the other animations are playing
		if (!anim.IsPlaying("Run") & !anim.IsPlaying("Jump") & !anim.IsPlaying("Run_Glow") & !anim.IsPlaying("Punch") & !anim.IsPlaying("Death") & !anim.IsPlaying("Respawning") && deadp == false){
			// Loop running animation
			anim.Play("Run");
		}
		
		// If the character is falling
		if (teliCharacter.velocity.y < fallAnimVel){
			// And the falling animation isn't already playing
			if (!anim.IsPlaying("Fall") & !anim.IsPlaying("Respawning")){
				anim.Play("Fall");
				//SendMessageUpwards("fallingDeath", true); //FIXME (fixed): Jettison bug here <---
			}
		}
	}

	void OnGUI() {
		GUI.skin = guiSkin;
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fontSize = 60;
		GUI.backgroundColor = Color.magenta;
		rctOff = GUI.skin.button.overflow;

		// If we are on a mobile platform
		if (mobileP == true){
			// Draw a Punch GUI button
			if (GUI.Button(new Rect(Screen.width/2 - Screen.width/2.75f, Screen.height/2 + Screen.height/4, 270, 150), "Punch", buttonStyle)){
				punchingP = true;
				SendMessageUpwards("punching", true);
				if(!anim.IsPlaying("Punch") && deadp == false){
					anim.Play("Punch");
					punchingP = false;
					SendMessageUpwards("punching", false);
				}
			}
		}
	}

	// Used to recieve message from Pause.cs to make sure
	// Teli doesn't die after game is resumed
	void PauseMovement(bool paused){
		// Set the gamePaused boolean to the value passed
		gamePaused = paused;
		waitResume = true;
		Debug.Log(gamePaused);
		Invoke("WaitResume", 1);
	}

	// After the game has been resumed, wait 1 second before checking
	// the death condition again
	void WaitResume(){
		waitResume = false;
	}

	// Handles death by Edges (death by obstacles is 
	// handled in ObstacleDeath() function)
	void DeathAnimation(){
		// If Teli's X-position stops increasing or the Y position is below the level
		// AND the game is not currently paused...
		if ((teliCharacter.velocity.x < 1 || teliCharacter.transform.position.y < levelthresholdY) && gamePaused == false && waitResume == false){
			Debug.Log("DEATH");
			deadp = true;
			resetp = true;

			// If Teli is below the level
			if (teliCharacter.transform.position.y < levelthresholdY){
				// Send a message to Movement_Gravity that Teli is falling to his death
				SendMessageUpwards("fallingDeath", true);
				// Stop the camera following
				mainCamera.SendMessage("death", true);
			}else{

				// If the death animation isn't already playing
				if(!anim.IsPlaying("Death") && deadp == true){
					exclamationPoint.renderer.enabled = true;

					// Play the death animation
					anim.Play("Death");
					SendMessageUpwards("death", true);


					deadp = false;
				}
			}

			if(!audio.isPlaying){
				audio.Play();
			}

			// Call reset function after 2 seconds
			Invoke("Reset", 2);

		}
	}

	// Recieve the checkpoint timestamp from ObstacleDeath.cs and set
	// checkpoint timestamp variable to the latest checkpoint's timestamp
	void getCheckpoint(float timestamp){
		checkpoint_timestamp = timestamp;
	}
	
	// Reset Teli's position, the background track, and respawn Notes
	void Reset(){
		//Debug.Log(resetp);
		if (resetp == true){
			Debug.Log("RESET");
			
			// Disable exclamation point
			exclamationPoint.renderer.enabled = false;
			// Send a message to restart Teli's movement
			SendMessageUpwards("death", false);
			// Send a message to restart Teli's movement
			SendMessageUpwards("fallingDeath", false);
			// Send a message to restart camera following
			mainCamera.SendMessage("death", false);
			// Reset score to last saved score
			SendMessageUpwards("ResetScore");
			// Reset position to spawn
			teliCharacter.transform.position = spawn.transform.position;
			// Reset music
			backgroundTrack.Stop();
			// Set music's start time to checkpoint's start time
			backgroundTrack.time = checkpoint_timestamp;
			// Restart music track
			backgroundTrack.Play();
			
			// Reset the renderer of all Notes
			for(int i = 0; i < Notes.Length; i++){
				Notes[i].renderer.enabled = true;
			}

			deadp = false;
			resetp = false;

			Debug.Log("We got here");

			// Invoke unpause ahead of time
			//pauseSystem_go.SendMessage("Unpause");

			// Pause immediately after repspawning
			//pauseSystem_go.SendMessage("TriggerPause", true);

			Debug.Log("And got here");

			// Trigger fade-in fade-out animation
			anim.Play("Respawning");

			Debug.Log("Here too");
		}
	}
	
	// Hanlde Glow Animation
	void GlowAnimation(){
		// If the glow and punch animations aren't already playing
		if(!anim.IsPlaying("Run_Glow") && !anim.IsPlaying("Punch") && deadp == false){
			anim.Stop();
			anim.Play("Run_Glow");
		}
	}
	
	// On death, stop movement and if the PunchAnimation isn't 
	// already playing	when Death broadcast is recieved, play it.
	void ObstacleDeath(){
		if(!anim.IsPlaying("Punch")){
			Debug.Log("DEATH - Wasn't punching");
			deadp = true;
			resetp = true;

			// And the death animation isn't already playing
			if(!anim.IsPlaying("Death")){
				audio.Play();
				exclamationPoint.renderer.enabled = true;

				// Play the death animation
				anim.Play("Death");
				// Send a message to stop Teli's movement
				SendMessageUpwards("death", true);
				SendMessageUpwards("fallingDeath", false);
			}

			// Call reset function after 2 seconds
			Invoke("Reset", 2);
		}
	}
}