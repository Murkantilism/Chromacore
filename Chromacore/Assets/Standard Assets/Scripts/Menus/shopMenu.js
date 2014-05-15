#pragma strict
import System;
import System.IO;

public class shopMenu extends MonoBehaviour {
	var mainCamera : GameObject;
	
	var chromacoreStore : GameObject;

	var skullKidButtonp = false;
	var scarfButtonp = false;
	var donateButtonp = false;
	
	var scarfUnlockedp = false;
	var skullKidUnlockedp = false;
	
	var skullKidEquipButtonp = false;
	var scarfEquipButtonp = false;
	var teliEquipButtonp = false;
	
	var scarfEquippedp = false;
	var skullKidEquippedp = false;
	var teliEquippedp = true;

	var lockedSprite_SkullKid : GameObject;
	var unlockedSprite_SkullKid : GameObject;
	var equippedSprite_SkullKid : GameObject;
	
	var lockedSprite_Scarf : GameObject;
	var unlockedSprite_Scarf : GameObject;
	var equippedSprite_Scarf : GameObject;
	
	var unlockedSprite_Teli : GameObject;
	var equippedSprite_Teli : GameObject;
	
	var Teli_Equipped_text_GO : GameObject;
	var Teli_Equipped_text : TextMesh;
	
	var Scarf_Equipped_text_GO : GameObject;
	var Scarf_Equipped_text : TextMesh;
	
	var SkullKid_Equipped_text_GO : GameObject;
	var SkullKid_Equipped_text : TextMesh;
	
	var Debug_Text_GO : GameObject;
	var Debug_Text : TextMesh;
	var Debug_Text2_GO : GameObject;
	var Debug_Text2 : TextMesh;
	var Debug_Text3_GO : GameObject;
	var Debug_Text3 : TextMesh;
	var Debug_Text4_GO : GameObject;
	var Debug_Text4 : TextMesh;
	
	var skullKid_boughtP_runp : boolean;
	var scarf_boughtP_runp : boolean;
	
	var skullKidPurchasedTxt = "skullKidPurchasedTxt.txt";
	var scarfPurchasedTxt = "scarfPurchasedTxt.txt";

	var skullKidEquippedTxt = "skullKidEquippedTxt.txt";
	var scarfEquippedTxt = "scarfEquippedTxt.txt";
	
	var teliEquippedTxt = "teliEquippedTxt.txt";
	
	var pcDefaultText_GO : GameObject;
	var pcDefaultText : TextMesh;

	function Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
		
		chromacoreStore = GameObject.Find("ChromacoreStore");
		
		Teli_Equipped_text_GO = GameObject.Find("Teli_Equipped_text");
		Teli_Equipped_text = Teli_Equipped_text_GO.GetComponent(TextMesh);
		
		Scarf_Equipped_text_GO = GameObject.Find("Scarf_Equipped_text");
		Scarf_Equipped_text = Scarf_Equipped_text_GO.GetComponent(TextMesh);
		//Scarf_Equipped_text.renderer.enabled = false;
		
		SkullKid_Equipped_text_GO = GameObject.Find("SkullKid_Equipped_text");
		SkullKid_Equipped_text = SkullKid_Equipped_text_GO.GetComponent(TextMesh);
		//SkullKid_Equipped_text.renderer.enabled = false;
		
		lockedSprite_SkullKid = GameObject.Find("lockedSprite_SkullKid");
		unlockedSprite_SkullKid = GameObject.Find("unlockedSprite_SkullKid");
		equippedSprite_SkullKid = GameObject.Find("equippedSprite_SkullKid");
		
		lockedSprite_Scarf = GameObject.Find("lockedSprite_Scarf");
		unlockedSprite_Scarf = GameObject.Find("unlockedSprite_Scarf");
		equippedSprite_Scarf = GameObject.Find("equippedSprite_Scarf");
		
		unlockedSprite_Teli = GameObject.Find("unlockedSprite_Teli");
		equippedSprite_Teli = GameObject.Find("equippedSprite_Teli");
		
		Debug_Text_GO = GameObject.Find("text_Debug");
		Debug_Text = Debug_Text_GO.GetComponent(TextMesh);
		Debug_Text2_GO = GameObject.Find("text_Debug2");
		Debug_Text2 = Debug_Text2_GO.GetComponent(TextMesh);
		Debug_Text3_GO = GameObject.Find("text_Debug3");
		Debug_Text3 = Debug_Text3_GO.GetComponent(TextMesh);
		Debug_Text4_GO = GameObject.Find("text_Debug4");
		Debug_Text4 = Debug_Text4_GO.GetComponent(TextMesh);
		
		// Hide plain unlocked sprite
		unlockedSprite_Teli.renderer.enabled = false;
		// Show green equipped sprite
		equippedSprite_Teli.renderer.enabled = true;
		
		skullKid_boughtP_runp = false;
		scarf_boughtP_runp = false;
		
		// If the platform is PC, unlock the skins by default
		#if UNITY_STANDALONE
		scarfUnlockedp = true;
		scarfUnlockedp = true;
		pcDefaultText_GO = GameObject.Find("pc default text");
		pcDefaultText = pcDefaultText_GO.GetComponent(TextMesh);
		pcDefaultText.renderer.enabled = true;
		#endif
	}
	
	function Update(){
		Debug_Text2.text = "Skull kid unlocked: " + skullKidUnlockedp + " | Skull kid equipped : " + skullKidEquippedp;
		Debug_Text3.text = "Scarf unlocked : " + scarfUnlockedp + " | Scarf equipped : " + scarfEquippedp;
		Debug_Text4.text = "Teli equipped : " + teliEquippedp + " | " + Time.timeSinceLevelLoad.ToString();
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
		
		// If the skull kid equip button is clicked, and it's unlocked
		if(skullKidEquipButtonp){
			Debug_Text.text = "Skull Kid skin unlocked, can't equip." + skullKidUnlockedp;
			//if(skullKidUnlockedp == true){
			if(SkullKid_Equipped_text.text == "Equip"){
				Debug_Text.text = "Skull Kid skin equip started";
				// Equip it
				skullKidEquippedp = true;
				SkullKid_Equipped_text.text = "Equipped";
				
				// Write to text file that this skin has been equipped
				WriteToTextFile(skullKidEquippedTxt, true);
				
				// Hide plain unlocked sprite
				unlockedSprite_SkullKid.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_SkullKid.renderer.enabled = true;
				
				// Hide the equipped sprites for other skins
				equippedSprite_Teli.renderer.enabled = false;
				equippedSprite_Scarf.renderer.enabled = false;
				// Show the plain sprite for other skins
				unlockedSprite_Teli.renderer.enabled = true;
				unlockedSprite_Scarf.renderer.enabled = true;
				
				// Unequip the other skins
				scarfEquippedp = false;
				teliEquippedp = false;
				
				// Set text for other skins to "equip"
				Scarf_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Write to text file that the other skins have been unequipped
				WriteToTextFile(scarfEquippedTxt, false);
				WriteToTextFile(teliEquippedTxt, false);
				
				Debug_Text.text = "Skull Kid skin equipped";
			}
		}
		// If the scarf equip button is clicked, and it's unlocked
		if(scarfEquipButtonp){
			Debug_Text.text = "Scarf skin unlocked, can't equip." + scarfUnlockedp;
			//if(scarfUnlockedp == true){
			if(Scarf_Equipped_text.text == "Equip"){
				Debug_Text.text = "Scarf equip started";
				// Equip it
				scarfEquippedp = true;
				Scarf_Equipped_text.text = "Equipped";
				
				// Write to text file that this skin has been equipped
				WriteToTextFile(scarfEquippedTxt, true);
				
				// Hide plain unlocked sprite
				unlockedSprite_Scarf.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_Scarf.renderer.enabled = true;
				
				// Hide the equipped sprites for other skins
				equippedSprite_Teli.renderer.enabled = false;
				equippedSprite_SkullKid.renderer.enabled = false;
				// Show the plain sprite for other skins
				unlockedSprite_Teli.renderer.enabled = true;
				unlockedSprite_SkullKid.renderer.enabled = true;
				
				// Unequip the other skins
				skullKidEquippedp = false;
				teliEquippedp = false;
				
				// Set text for other skins to "equip"
				SkullKid_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Write to text file that the other skins have been unequipped
				WriteToTextFile(skullKidEquippedTxt, false);
				WriteToTextFile(teliEquippedTxt, false);
				
				Debug_Text.text = "Scarf skin equipped";
			}
		}
		// If the Teli equip button is clicked (no need to check if it's unlocked)
		if(teliEquipButtonp){
			// Equip it
			teliEquippedp = true;
			Teli_Equipped_text.text = "Equipped";
			
			// Write to text file that this skin has been equipped
			WriteToTextFile(teliEquippedTxt, true);
			
			// Hide plain unlocked sprite
			unlockedSprite_Teli.renderer.enabled = false;
			// Show green equipped sprite
			equippedSprite_Teli.renderer.enabled = true;
			
			// Hide the equipped sprites for other skins
			equippedSprite_SkullKid.renderer.enabled = false;
			equippedSprite_Scarf.renderer.enabled = false;
			// Show the plain sprite for other skins
			unlockedSprite_SkullKid.renderer.enabled = true;
			unlockedSprite_Scarf.renderer.enabled = true;
			
			// Unequip the other skins
			skullKidEquippedp = false;
			scarfEquippedp = false;
			
			// Set text for other skins to "equip"
			SkullKid_Equipped_text.text = "Equip";
			Scarf_Equipped_text.text = "Equip";
			
			// Write to text file that the other skins have been unequipped
			WriteToTextFile(skullKidEquippedTxt, false);
			WriteToTextFile(scarfEquippedTxt, false);
			
			Debug_Text.text = "Teli skin equipped";
		}
	}
	
	// Helper function for writing to text files
	function WriteToTextFile(textFilePath : String, myBool : boolean){
		var sw = File.CreateText(Application.persistentDataPath + "\\" + textFilePath);
		sw.Write(myBool);
		sw.Close();
	}
	
	// If the skull kid skin has been bought (recieves message from ChromacoreEventHandler.cs)
	function skullKid_skinBought(boughtP : boolean){
		if(skullKid_boughtP_runp == false){ // Only run this code once
			if(boughtP == true){
				// Set boolean flag to true
				skullKidUnlockedp = true;
				// Show the "unlocked" sprite
				unlockedSprite_SkullKid.renderer.enabled = true;
				// Hide the "locked" sprite
				lockedSprite_SkullKid.renderer.enabled = false;
				
				// Set the text for this skin to be eligible to equip
				SkullKid_Equipped_text.text = "Equip";
				// Show the "Equipped" text
				SkullKid_Equipped_text.renderer.enabled = true;
				
				// Equip skin on purchase
				//skullKidEquippedp = true;
				
				// Write to text file that skin has been unlocked
				WriteToTextFile(skullKidPurchasedTxt, true);
			}else if (boughtP == false){
				// Otherwise, set boolean flag to false, and write to text files 
				// that this skin isn't unlocked or equipped
				skullKidUnlockedp = false;
				WriteToTextFile(skullKidPurchasedTxt, false);
				
				//skullKidEquippedp = false;
				//WriteToTextFile(skullKidEquippedTxt, false);
			}
			skullKid_boughtP_runp = true; // Only run the above code once
		}
	}
	
	// If the scarf skin has been bought (recieves message from ChromacoreEventHandler.cs)
	function scarf_skinBought(boughtP : boolean){
		if(scarf_boughtP_runp == false){ // Only run this code once
			if(boughtP == true){
				Debug_Text.text = "scarf skin purchased";
				// Set boolean flag to true
				scarfUnlockedp = true;
				// Show the "unlocked" sprite
				unlockedSprite_Scarf.renderer.enabled = true;
				// Hide the "locked" sprite
				lockedSprite_Scarf.renderer.enabled = false;
				
				// Set the text for this skin to be eligible to equip
				Scarf_Equipped_text.text = "Equip";
				// Show the "Equipped" text
				Scarf_Equipped_text.renderer.enabled = true;
				
				// Equip skin on purchase
				//scarfEquippedp = true;
				
				// Write to text file that skin has been unlocked
				WriteToTextFile(scarfPurchasedTxt, true);
			}else if (boughtP == false){
				// Otherwise, set boolean flag to false, and write to text files 
				// that this skin isn't unlocked or equipped
				scarfUnlockedp = false;
				WriteToTextFile(scarfPurchasedTxt, false);
				
				//scarfEquippedp = false;
				//WriteToTextFile(scarfEquippedTxt, true);
			}
			scarf_boughtP_runp = true; // Only run above code once
		}
	}

	function OnGUI() {
		GUI.backgroundColor = Color.magenta;
	
		var buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			Destroy(mainCamera);
			Destroy(chromacoreStore);
			Application.LoadLevel("MainMenu");
		}
	}
}