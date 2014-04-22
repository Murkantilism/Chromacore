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
	var mainCamera : GameObject;
	
	var levelSelectKeyboard : GameObject;
	
	function Start(){
		mainCamera = GameObject.FindWithTag("MainCamera");
		
		levelSelectKeyboard = GameObject.Find("levelSelectionKeyboardSupport"); // Second level selection screen
	}
	
	function OnMouseEnter(){
		// Change the color of the text
		renderer.material.color = Color.blue;
		levelSelectKeyboard.SendMessage("mouseOver", true);
	}

	function OnMouseExit(){
		// Change the color of the text back to the original color (white)
		renderer.material.color = Color.white;
		levelSelectKeyboard.SendMessage("mouseOver", false);
	}

	function OnMouseUp(){
		// Load the level selected
		if(levelEleven){
			Destroy(mainCamera);
			Application.LoadLevel("Level11");
		}else if(levelTwelve){
			Destroy(mainCamera);
			Application.LoadLevel("Level12");
		}else if(levelThirteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level13");
		}else if(levelFourteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level14");
		}else if(levelFifteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level15");
		}else if(levelSixteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level16");
		}else if(levelSeventeen){
			Destroy(mainCamera);
			Application.LoadLevel("Level17");
		}else if(levelEighteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level18");
		}else if(levelNineteen){
			Destroy(mainCamera);
			Application.LoadLevel("Level19");
		}else if(levelTwenty){
			Destroy(mainCamera);
			Application.LoadLevel("Level20");
		}else if(moreLevels){
			Application.LoadLevel("LevelSelect3");
		}
	}

	function OnGUI() {
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Application.LoadLevel("LevelSelect");
		}
	}
}