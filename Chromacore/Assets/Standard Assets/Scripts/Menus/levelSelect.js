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
	var levelDEMO = false;
	var moreLevels = false;
	var mainCamera : GameObject;
	
	var levelSelectKeyboard : GameObject;
	
	function Start(){
		mainCamera = GameObject.FindWithTag("MainCamera");

		levelSelectKeyboard = GameObject.Find("levelSelectKeyboardSupport"); // First level selection screen
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
		if(levelOne){
			Destroy(mainCamera);
			Application.LoadLevel("Level1");
		}else if(levelTwo){
			Destroy(mainCamera);
			Application.LoadLevel("Level2");
		}else if(levelThree){
			Destroy(mainCamera);
			Application.LoadLevel("Level3");
		}else if(levelFour){
			Destroy(mainCamera);
			Application.LoadLevel("Level4");
		}else if(levelFive){
			Destroy(mainCamera);
			Application.LoadLevel("Level5");
		}else if(levelSix){
			Destroy(mainCamera);
			Application.LoadLevel("Level6");
		}else if(levelSeven){
			Destroy(mainCamera);
			Application.LoadLevel("Level7");
		}else if(levelEight){
			Destroy(mainCamera);
			Application.LoadLevel("Level8");
		}else if(levelNine){
			Destroy(mainCamera);
			Application.LoadLevel("Level9");
		}else if(levelTen){
			Destroy(mainCamera);
			Application.LoadLevel("Level10");
		}else if(moreLevels){
			DontDestroyOnLoad(mainCamera.transform);
			Application.LoadLevel("LevelSelect2");
		}else if(levelDEMO){
			Destroy(mainCamera);
			Application.LoadLevel("Level15");
		}
	}

	function OnGUI() {
		GUI.backgroundColor = Color.magenta;
		
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}
	}
}