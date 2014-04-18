#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

var quitButtonp = false;
var creditsButtonp = false;
var autoGeneratorp = false;
var playButtonp = false;

var menuIndex : int = 0;

public var playButton : TextMesh;
public var creditsButton : TextMesh;
public var quitButton : TextMesh;

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
}

function OnMouseUp(){
	if(quitButtonp){
		//If the quit button is clicked, quit the game
		Application.Quit();
	}else if(creditsButtonp){
		//If the Credits button is clicked, open the Credits screen
		Application.LoadLevel("CreditsScreen");
	}else if(autoGeneratorp){
		// If the Level Auto-Generator is picked, load the file brwoser
		Application.LoadLevel("SongBrowser");
	}else if(playButtonp){
		//If the play button is clicked, and there is a saved game, load that
		Application.LoadLevel("LevelSelect");
	}
}

// Keyboard support for main menu
function Update(){

	if(Input.GetKeyDown(KeyCode.UpArrow)){
		if(menuIndex >= 1){
			menuIndex--;
		}
	}
	
	if(Input.GetKeyDown(KeyCode.DownArrow)){
		if(menuIndex <= 1){
			menuIndex++;
		}
	}
	
	// Change the color of the renderer based on index
	if(menuIndex == 0){
		playButton.renderer.material.color = Color.blue;
		playButtonp = true;
	}else{
		playButton.renderer.material.color = Color.white;
		playButtonp = false;
	}
	
	if(menuIndex == 1){
		creditsButton.renderer.material.color = Color.blue;
		creditsButtonp = true;
	}else{
		creditsButton.renderer.material.color = Color.white;
		creditsButtonp = false;
	}
	
	if(menuIndex == 2){
		quitButton.renderer.material.color = Color.blue;
		quitButtonp = true;
	}else{
		quitButton.renderer.material.color = Color.white;
		quitButtonp = false;
	}
	
	// If the user hits Enter, Spacebar, or Keypad Enter, load the currently selected menu component
	if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
		if(quitButtonp){
		//If the quit button is clicked, quit the game
		Application.Quit();
		}else if(creditsButtonp){
			//If the Credits button is clicked, open the Credits screen
			Application.LoadLevel("CreditsScreen");
		}else if(autoGeneratorp){
			// If the Level Auto-Generator is picked, load the file brwoser
			Application.LoadLevel("SongBrowser");
		}else if(playButtonp){
			//If the play button is clicked, and there is a saved game, load that
			Application.LoadLevel("LevelSelect");
		}
	}
	
}