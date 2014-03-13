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
			Application.LoadLevel("Level11");
		}else if(levelTwelve){
			Application.LoadLevel("Level12");
		}else if(levelThirteen){
			Application.LoadLevel("Level13");
		}else if(levelFourteen){
			Application.LoadLevel("Level14");
		}else if(levelFifteen){
			Application.LoadLevel("Level15");
		}else if(levelSixteen){
			Application.LoadLevel("Level16");
		}else if(levelSeventeen){
			Application.LoadLevel("Level17");
		}else if(levelEighteen){
			Application.LoadLevel("Level18");
		}else if(levelNineteen){
			Application.LoadLevel("Level19");
		}else if(levelTwenty){
			Application.LoadLevel("Level20");
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