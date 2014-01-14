#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

var quitButtonp = false;
var optionsButtonp = false;
var autoGeneratorp = false;
var playButtonp = false;

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
	}else if(optionsButtonp){
		//If the Options button is clicked, open the options menu
		//Application.LoadLevel("OptionsScreen");
	}else if(autoGeneratorp){
		// If the Level Auto-Generator is picked, load the file brwoser
		Application.LoadLevel("SongBrowser");
	}else if(playButtonp){
		//If the play button is clicked, and there is a saved game, load that
		Application.LoadLevel("LevelSelect");
	}
}