#pragma strict

var mouseOverp : boolean = false;

public var playButton : TextMesh;
public var creditsButton : TextMesh;
public var quitButton : TextMesh;
public var shopButton : TextMesh;

var quitButtonp = false;
var creditsButtonp = false;
var autoGeneratorp = false;
var playButtonp = false;
var shopButtonp = false;

var menuIndex : int = 0;

function mouseOver(bool : boolean){
		mouseOverp = bool;
}

function Update () {

	if(Input.GetKeyDown(KeyCode.UpArrow)){
		if(menuIndex >= 1){
			menuIndex--;
		}
	}
	
	if(Input.GetKeyDown(KeyCode.DownArrow)){
		if(menuIndex <= 2){
			menuIndex++;
		}
	}
	
	// If the mouse isn't already navigating the menu,
	// Change the color of the renderer and boolean value based on index
	if(mouseOverp == false){
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
			shopButton.renderer.material.color = Color.blue;
			shopButtonp = true;
		}else{
			shopButton.renderer.material.color = Color.white;
			shopButtonp = false;
		}
		
		if(menuIndex == 3){
			quitButton.renderer.material.color = Color.blue;
			quitButtonp = true;
		}else{
			quitButton.renderer.material.color = Color.white;
			quitButtonp = false;
		}
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