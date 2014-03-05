#pragma strict
// mainMenu.js - Last Updated 01/04/2014
// Chromacore - Incendiary Industries - Deniz Ozkaynak
// Contact:   incendiaryindustries@gmail.com   with any questions
public class levelSelect2 extends MonoBehaviour {
	var levelEleven = false;
	var levelTwelve = false;
	var levelThirteen = false;
	var levelFourteen = false;
	var levelFifteen = false;
	var levelSixteen = false;
	var levelSeventeen = false;
	var levelEighteen = false;
	var levelNineteen = false;
	var levelTwenty = false;
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
		if(levelEleven){
			Application.LoadLevel("LevelEleven");
		}else if(levelTwelve){
			Application.LoadLevel("LevelTweleve");
		}else if(levelThirteen){
			Application.LoadLevel("LevelThirteen");
		}else if(levelFourteen){
			Application.LoadLevel("LevelFourteen");
		}else if(levelFifteen){
			Application.LoadLevel("LevelFifteen");
		}else if(levelSixteen){
			Application.LoadLevel("LevelSixteen");
		}else if(levelSeventeen){
			Application.LoadLevel("LevelSeventeen");
		}else if(levelEighteen){
			Application.LoadLevel("LevelEightteen");
		}else if(levelNineteen){
			Application.LoadLevel("LevelNineteen");
		}else if(levelTwenty){
			Application.LoadLevel("LevelTwenty");
		}else if(moreLevels){
			Application.LoadLevel("LevelSelect3");
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