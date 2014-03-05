#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions
public class levelSelect extends MonoBehaviour {
	var levelOne = false;
	var levelTwo = false;
	var levelThree = false;
	var levelFour = false;
	var levelFive = false;
	var levelSix = false;
	var levelSeven = false;
	var levelEight = false;
	var levelNine = false;
	var levelTen = false;
	var moreLevels = false;

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
		}else if(levelFour){
			Application.LoadLevel("LevelFour");
		}else if(levelFive){
			Application.LoadLevel("LevelFive");
		}else if(levelSix){
			Application.LoadLevel("LevelSix");
		}else if(levelSeven){
			Application.LoadLevel("LevelSeven");
		}else if(levelEight){
			Application.LoadLevel("LevelEight");
		}else if(levelNine){
			Application.LoadLevel("LevelNine");
		}else if(levelTen){
			Application.LoadLevel("LevelTen");
		}else if(moreLevels){
			Application.LoadLevel("LevelSelect2");
		}
	}

	function OnGUI() {
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Application.LoadLevel("MainMenu");
		}
	}
}