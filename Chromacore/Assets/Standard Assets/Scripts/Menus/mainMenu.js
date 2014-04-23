#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

var quitButtonp = false;
var creditsButtonp = false;
var autoGeneratorp = false;
var playButtonp = false;
var shopButtonp = false;

public var playButton : TextMesh;
public var creditsButton : TextMesh;
public var quitButton : TextMesh;
public var shopButton : TextMesh;

var mainMenuKeyboard : GameObject;

function Start(){
	mainMenuKeyboard = GameObject.Find("mainMeunKeyboardSupport");
}

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
	mainMenuKeyboard.SendMessage("mouseOver", true);
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
	mainMenuKeyboard.SendMessage("mouseOver", false);
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
	}else if(shopButtonp){
		Application.LoadLevel("Shop");
	}
}

// Keyboard support for main menu
function Update(){
}