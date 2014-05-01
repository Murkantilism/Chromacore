#pragma strict
public class shopMenu extends MonoBehaviour {
	
	var skullKidButtonp = false;
	var scarfButtonp = false;
	var donateButtonp = false;
	
	var mainCamera : GameObject;
	
	var chromacoreStore : GameObject;
	
	var unlockedSprite_SkullKid : GameObject;
	
	var unlockedSprite_Scarf : GameObject;
	
	var lockedSprite_SkullKid : GameObject;
	
	var lockedSprite_Scarf : GameObject;

	function Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		
		chromacoreStore = GameObject.Find("ChromacoreStore");
		
		unlockedSprite_SkullKid = GameObject.Find("unlockedSprite_SkullKid");
		
		unlockedSprite_Scarf = GameObject.Find("unlockedSprite_Scarf");
		
		lockedSprite_SkullKid = GameObject.Find("lockedSprite_SkullKid");
		
		lockedSprite_Scarf = GameObject.Find("lockedSprite_Scarf");
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
			Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/donate.html");
		}
	}
	
	function skullKid_skinBought(boughtP : boolean){
		if(boughtP == true){
			unlockedSprite_SkullKid.renderer.enabled = true;
			lockedSprite_SkullKid.renderer.enabled = false;
		}else{
			return;
		}
	}
	
	function scarf_skinBought(boughtP : boolean){
		if(boughtP == true){
			unlockedSprite_Scarf.renderer.enabled = true;
			lockedSprite_Scarf.renderer.enabled = false;
		}else{
			return;
		}
	}

	function OnGUI() {
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Destroy(mainCamera);
			Destroy(chromacoreStore);
			Application.LoadLevel("MainMenu");
		}
	}
}