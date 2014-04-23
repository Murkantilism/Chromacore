#pragma strict
public class shopMenu extends MonoBehaviour {
	
	var skullKidButtonp = false;
	var scarfButtonp = false;
	var donateButtonp = false;
	
	var mainCamera : GameObject;
	
	var chromacoreStore : GameObject;

	function Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		
		chromacoreStore = GameObject.Find("ChromacoreStore");
	}
	
	function OnMouseEnter(){
		// Change the color of the text
		renderer.material.color = Color.blue;
		//levelSelectKeyboard.SendMessage("mouseOver", true);
	}

	function OnMouseExit(){
		// Change the color of the text back to the original color (white)
		renderer.material.color = Color.white;
		//levelSelectKeyboard.SendMessage("mouseOver", false);
	}
	
	function OnMouseUp(){
		if(skullKidButtonp){
			chromacoreStore.SendMessage("buySkin", "skull_kid_skin");
		}else if(scarfButtonp){
			chromacoreStore.SendMessage("buySkin", "scarf_skin");
		}else if(donateButtonp){
			Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/donate.html"); //TODO: Add paypal url here
		}
	}

	function Update () {
		
	}

	function OnGUI() {
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}
	}
}