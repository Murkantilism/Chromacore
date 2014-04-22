#pragma strict

// Keyboard support for navigating level selection menus
public class levelSelect2Keyboard extends MonoBehaviour {
	var menuIndex : int = 0;
	
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
	
	var levelElevenText : TextMesh;
	var levelTwelveText : TextMesh;
	var levelThirteenText : TextMesh;
	var levelFourteenText : TextMesh;
	var levelFifteenText : TextMesh;
	var levelSixteenText : TextMesh;
	var levelSeventeenText : TextMesh;
	var levelEighteenText : TextMesh;
	var levelNineteenText : TextMesh;
	var levelTwentyText : TextMesh;
	var moreLevelsText : TextMesh;
	
	var mainCamera : GameObject;
	
	var mouseOverp : boolean = false;
	
	function Start(){
		mainCamera = GameObject.FindWithTag("MainCamera");
		//mouseOverp = true;
	}
	
	function mouseOver(bool : boolean){
		mouseOverp = bool;
	}
	
	function Update(){
		// If the backspace or espace key is hit, go back to main menu
		if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("LevelSelect");
		}
		
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(menuIndex >= 1){
				menuIndex--;
			}
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow)){
			if(menuIndex <= 9){
				menuIndex++;
			}
		}
		
		// If the mouse isn't already navigating the menu,
		// Change the color of the renderer and boolean value based on index
		if(mouseOverp == false){
			if(menuIndex == 0){
				levelElevenText.renderer.material.color = Color.blue;
				levelEleven = true;
			}else{
				levelElevenText.renderer.material.color = Color.white;
				levelEleven = false;
			}
			if(menuIndex == 1){
				levelTwelveText.renderer.material.color = Color.blue;
				levelTwelve = true;
			}else{
				levelTwelveText.renderer.material.color = Color.white;
				levelTwelve = false;
			}
			if(menuIndex == 2){
				levelThirteenText.renderer.material.color = Color.blue;
				levelThirteen = true;
			}else{
				levelThirteenText.renderer.material.color = Color.white;
				levelThirteen = false;
			}
			if(menuIndex == 3){
				levelFourteenText.renderer.material.color = Color.blue;
				levelFourteen = true;
			}else{
				levelFourteenText.renderer.material.color = Color.white;
				levelFourteen = false;
			}
			if(menuIndex == 4){
				levelFifteenText.renderer.material.color = Color.blue;
				levelFifteen = true;
			}else{
				levelFifteenText.renderer.material.color = Color.white;
				levelFifteen = false;
			}
			if(menuIndex == 5){
				levelSixteenText.renderer.material.color = Color.blue;
				levelSixteen = true;
			}else{
				levelSixteenText.renderer.material.color = Color.white;
				levelSixteen = false;
			}
			if(menuIndex == 6){
				levelSeventeenText.renderer.material.color = Color.blue;
				levelSeventeen = true;
			}else{
				levelSeventeenText.renderer.material.color = Color.white;
				levelSeventeen = false;
			}
			if(menuIndex == 7){
				levelEighteenText.renderer.material.color = Color.blue;
				levelEighteen = true;
			}else{
				levelEighteenText.renderer.material.color = Color.white;
				levelEighteen = false;
			}
			if(menuIndex == 8){
				levelNineteenText.renderer.material.color = Color.blue;
				levelNineteen = true;
			}else{
				levelNineteenText.renderer.material.color = Color.white;
				levelNineteen = false;
			}
			if(menuIndex == 9){
				levelTwentyText.renderer.material.color = Color.blue;
				levelTwenty = true;
			}else{
				levelTwentyText.renderer.material.color = Color.white;
				levelTwenty = false;
			}
			if(menuIndex == 10){
				moreLevelsText.renderer.material.color = Color.blue;
				moreLevels = true;
			}else{
				moreLevelsText.renderer.material.color = Color.white;
				moreLevels = false;
			}
		}
		
		// If the user hits Enter, Spacebar, or Keypad Enter, load the currently selected menu component
		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
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
				DontDestroyOnLoad(mainCamera.transform);
				Application.LoadLevel("LevelSelect3");
			}
		}
	}
}