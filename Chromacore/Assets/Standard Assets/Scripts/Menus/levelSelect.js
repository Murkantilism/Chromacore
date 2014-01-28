#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions

var levelOne = false;
var levelTwo = false;
var levelThree = false;

function OnMouseEnter(){
	// Change the color of the text
	renderer.material.color = Color.blue;
}

function OnMouseExit(){
	// Change the color of the text back to the original color (white)
	renderer.material.color = Color.white;
}

function OnMouseUp(){
	// Load the level selected
	if(levelOne){
		Application.LoadLevel("LevelOne");
	}else if(levelTwo){
		Application.LoadLevel("LevelTwo");
	}else if(levelThree){
		Application.LoadLevel("LevelThree");
	}
}

function OnGUI() {
	if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back")){
		Application.LoadLevel("MainMenu");
	}
}