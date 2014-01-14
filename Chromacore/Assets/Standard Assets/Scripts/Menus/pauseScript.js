#pragma strict
// pauseScript.js - Handles pausing the game. Last updated 8/5/2012
// A Viking Tale - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

// Is the game is currently paused? (True means paused)
var pausedp : boolean = false;
// Has the user clicked the Quit button?
var quitp : boolean = false;
// Has the user clicked the Options button?
var optionsp : boolean = false;

// Should OnGUI be enabled right now?
var turnOnGUIOn : boolean = false;

// The options button text variable
var optionsBtn : GUIText;
// The oquit button text variable
var quitBtn : GUIText;

// The background music audio clip variable
var backgroundMusic : AudioClip;

// Is the background music currently playing? (Default true)
var musicPlayingp : boolean = true;

function Start () {
	
}

function Update () {
	// If the Pause button has been clicked AND..
	if (Input.GetButtonDown("Pause")){
		// ...the game isn't already paused, then pause it
		if(!pausedp){
			pausedp = true;	
			Time.timeScale = 0;
			optionsBtn.enabled = true;
			quitBtn.enabled = true;
			
			// Turn the music off in the pause screen
			audio.Pause();
			musicPlayingp = false;
		// ...the game has already been paused and the Pause button
		// is clicked again, unpause the game.
		}else if(pausedp){
			pausedp = false;
			Time.timeScale = 1;
			optionsBtn.enabled = false;
			quitBtn.enabled = false;
			turnOnGUIOn = false;
			
			// Turn the music back on
			audio.Play();
			musicPlayingp = true;
		}
	}
}

function OnMouseUp(){
	if(quitp){
		//If the quit button is clicked, quit the game
		Application.Quit();
		
	}else if(optionsp){
		//If the Options button is clicked, open the options menu
		//Application.LoadLevel("OptionsScreen");
		turnOnGUIOn = true;
		OnGUI();
	}
}

function OnGUI(){
	
	if(turnOnGUIOn == true){	
		// Create a purple Options button
		GUI.backgroundColor = Color.magenta;
		
		GUI.Button((new Rect (800, 220, 100, 50)), "Graphics Options");
		GUI.Button((new Rect (800, 150, 100, 50)), "Sound Options");
		GUI.Button((new Rect (800, 100, 100, 50)), "Other Options");				
	}else{
		GUI.enabled = false;
	}	
}