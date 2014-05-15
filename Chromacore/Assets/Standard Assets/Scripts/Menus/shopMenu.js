#pragma strict
import System;
import System.IO;

public class shopMenu extends MonoBehaviour {
	var mainCamera : GameObject;
	
	var chromacoreStore : GameObject;
	
	// Text buttons
	var skullKidPurchaseButtonp = false;
	var scarfPurchaseButtonp = false;
	var donateButtonp = false;
	
	var skullKidEquipButtonp = false;
	var scarfEquipButtonp = false;
	var teliEquipButtonp = false;
	
	// Boolean flags
	var scarfUnlockedp = false;
	var skullKidUnlockedp = false;
	
	var skullKidEquippedp = false;
	var scarfEquippedp = false;
	var teliEquippedp = false;
	
	// Sprites
	var lockedSprite_SkullKid : GameObject;
	var unlockedSprite_SkullKid : GameObject;
	var equippedSprite_SkullKid : GameObject;
	
	var lockedSprite_Scarf : GameObject;
	var unlockedSprite_Scarf : GameObject;
	var equippedSprite_Scarf : GameObject;
	
	var unlockedSprite_Teli : GameObject;
	var equippedSprite_Teli : GameObject;
	
	// Text Meshes
	var Teli_Equipped_text_GO : GameObject;
	var Teli_Equipped_text : TextMesh;
	
	var Scarf_Equipped_text_GO : GameObject;
	var Scarf_Equipped_text : TextMesh;
	
	var SkullKid_Equipped_text_GO : GameObject;
	var SkullKid_Equipped_text : TextMesh;

	// File names for writing text files
	var skullKidPurchasedTextFileName= "skullKidPurchasedTxt.txt";
	var scarfPurchasedTextFileName = "scarfPurchasedTxt.txt";

	var skullKidEquippedTextFileName = "skullKidEquippedTxt.txt";
	var scarfEquippedTextFileName = "scarfEquippedTxt.txt";
	var teliEquippedTextFileName = "teliEquippedTxt.txt";
	
	// Default text shown only on PC builds
	var pcDefaultText_GO : GameObject;
	var pcDefaultText : TextMesh;
	
	// Text meshes for debugging
	var Debug_Text_GO : GameObject;
	var Debug_Text : TextMesh;
	var Debug_Text2_GO : GameObject;
	var Debug_Text2 : TextMesh;
	var Debug_Text3_GO : GameObject;
	var Debug_Text3 : TextMesh;
	var Debug_Text4_GO : GameObject;
	var Debug_Text4 : TextMesh;
	
	function Start(){
		mainCamera = GameObject.FindWithTag("MainCamera");
		
		chromacoreStore = GameObject.Find("ChromacoreStore");
		
		// Find and assign the text meshes for each skin
		Teli_Equipped_text_GO = GameObject.Find("Teli_Equipped_text");
		Teli_Equipped_text = Teli_Equipped_text_GO.GetComponent(TextMesh);
		
		Scarf_Equipped_text_GO = GameObject.Find("Scarf_Equipped_text");
		Scarf_Equipped_text = Scarf_Equipped_text_GO.GetComponent(TextMesh);
		
		SkullKid_Equipped_text_GO = GameObject.Find("SkullKid_Equipped_text");
		SkullKid_Equipped_text = SkullKid_Equipped_text_GO.GetComponent(TextMesh);
		
		// Find and assign the sprites for unlocked/locked/equipped for each skin
		lockedSprite_SkullKid = GameObject.Find("lockedSprite_SkullKid");
		unlockedSprite_SkullKid = GameObject.Find("unlockedSprite_SkullKid");
		equippedSprite_SkullKid = GameObject.Find("equippedSprite_SkullKid");
		
		lockedSprite_Scarf = GameObject.Find("lockedSprite_Scarf");
		unlockedSprite_Scarf = GameObject.Find("unlockedSprite_Scarf");
		equippedSprite_Scarf = GameObject.Find("equippedSprite_Scarf");
		
		unlockedSprite_Teli = GameObject.Find("unlockedSprite_Teli");
		equippedSprite_Teli = GameObject.Find("equippedSprite_Teli");
		
		// Find and assign the text meshes for debugging
		Debug_Text_GO = GameObject.Find("text_Debug");
		Debug_Text = Debug_Text_GO.GetComponent(TextMesh);
		Debug_Text2_GO = GameObject.Find("text_Debug2");
		Debug_Text2 = Debug_Text2_GO.GetComponent(TextMesh);
		Debug_Text3_GO = GameObject.Find("text_Debug3");
		Debug_Text3 = Debug_Text3_GO.GetComponent(TextMesh);
		Debug_Text4_GO = GameObject.Find("text_Debug4");
		Debug_Text4 = Debug_Text4_GO.GetComponent(TextMesh);
		
		// Show Teli's skin as equipped by default - Hide plain unlocked sprite
		unlockedSprite_Teli.renderer.enabled = false;
		// Show green equipped sprite
		equippedSprite_Teli.renderer.enabled = true;
		
		// If the platform is PC, unlock the skins by default
		#if UNITY_STANDALONE
		skullKidUnlockedp = true;
		scarfUnlockedp = true;
		pcDefaultText_GO = GameObject.Find("pc default text");
		pcDefaultText = pcDefaultText_GO.GetComponent(TextMesh);
		pcDefaultText.renderer.enabled = true;
		#endif
		
		// Check which skins were previously equipped
		CheckTextFiles(skullKidEquippedTextFileName, "skullKidEquippedp");
		CheckTextFiles(scarfEquippedTextFileName, "scarfEquippedp");
		CheckTextFiles(teliEquippedTextFileName, "teliEquippedp");
	}
	
	// Check the equip text files to see which skin was previously being used
	function CheckTextFiles(textFilePath : String, skinBool : String){
		// If the file exists
		if(File.Exists(textFilePath)){
			// Create an instance of StreamReader to read text file
			var sr = File.OpenText(Application.persistentDataPath + "\\" + textFilePath);
			var line = sr.ReadToEnd();

			Debug.Log(line);
		}else{
			Debug.Log("Could not Open the file: " + textFilePath + " for reading.");
        	return;
		}

		// If the text file reads true, set boolean flag to true for the corresponding skin
		if(line == "true"){
			if(skinBool == "skullKidEquippedp"){
				skullKidEquippedp = true;
			}
			if(skinBool == "scarfEquippedp"){
				scarfEquippedp = true;
			}
			if(skinBool == "teliEquippedp"){
				teliEquippedp = true;
			}
		// If the text file reads false, set the boolean flag to false for the corresponding skin
		}else{
			if(skinBool == "skullKidEquippedp"){
				skullKidEquippedp = false;
			}
			if(skinBool == "scarfEquippedp"){
				scarfEquippedp = false;
			}
			if(skinBool == "teliEquippedp"){
				teliEquippedp = false;
			}
		}
	}
	
	// If the skull kid skin has been bought (recieves message from ChromacoreEventHandler.cs) 
	function skullKid_skinBought(boughtP : boolean){
		if(boughtP == true){
			// Unlock the skull kid skin
			skullKidUnlockedp = true;
			// Show the "unlocked" sprite
			unlockedSprite_SkullKid.renderer.enabled = true;
			// Hide the "locked" sprite
			lockedSprite_SkullKid.renderer.enabled = false;
			
			// Set the text for this skin to be eligible to equip
			SkullKid_Equipped_text.text = "Equip";
			// Show the "Equipped" text
			SkullKid_Equipped_text.renderer.enabled = true;
			
			// Write to text file that skin has been unlocked
			WriteToTextFile(skullKidPurchasedTextFileName, true);
			
			Debug_Text.text = skullKidUnlockedp + " | " + Time.timeSinceLevelLoad;
		}
	}
	
	// If the scarf skin has been bought (recieves message from ChromacoreEventHandler.cs)
	function scarf_skinBought(boughtP : boolean){
		if(boughtP == true){
			// Unlock the scarf skin
			scarfUnlockedp = true;
			// Show the "unlocked" sprite
			unlockedSprite_Scarf.renderer.enabled = true;
			// Hide the "locked" sprite
			lockedSprite_Scarf.renderer.enabled = false;
			
			// Set the text for this skin to be eligible to equip
			Scarf_Equipped_text.text = "Equip";
			// Show the "Equipped" text
			Scarf_Equipped_text.renderer.enabled = true;
			
			// Write to text file that skin has been unlocked
			WriteToTextFile(scarfPurchasedTextFileName, true);
		}	
	}
	
	// Change the color of the text on scrollover
	function OnMouseEnter(){
		renderer.material.color = Color.blue;
	}
	
	// Change the color of the text back to the original color (white)
	function OnMouseExit(){
		renderer.material.color = Color.white;
	}
	
	function OnMouseUp(){
		// If the button to buy Skull Kid is clicked
		if(skullKidPurchaseButtonp){
			// Send a message to ChromacoreStore.cs
			chromacoreStore.SendMessage("buySkin", "skull_kid_skin");
		}else if(scarfPurchaseButtonp){
			chromacoreStore.SendMessage("buySkin", "scarf_skin");
		}else if(donateButtonp){
			Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/donate.html");
		}
		
		// If the equip text button for Skull Kid is clicked
		if(skullKidEquipButtonp){
			// And the skin is eligible to be equipped
			if(SkullKid_Equipped_text.text == "Equip"){
				// Equip the skin
				skullKidEquippedp = true;
				SkullKid_Equipped_text.text = "Equipped";
				
				// Hide plain unlocked sprite
				unlockedSprite_SkullKid.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_SkullKid.renderer.enabled = true;
				
				// Write to text file that this skin has been equipped
				WriteToTextFile(skullKidEquippedTextFileName, true);
				
				// Unequip the other skins
				scarfEquippedp = false;
				teliEquippedp = false;
				
				// Set the text for other skins to "Equip"
				Scarf_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Hide the equipped sprites for other skins
				equippedSprite_Teli.renderer.enabled = false;
				equippedSprite_Scarf.renderer.enabled = false;
				// Show the plain sprite for other skins
				unlockedSprite_Teli.renderer.enabled = true;
				unlockedSprite_Scarf.renderer.enabled = true;
				
				// Write to text file that the other skins have been unequipped
				WriteToTextFile(scarfEquippedTextFileName, false);
				WriteToTextFile(teliEquippedTextFileName, false);
				
				Debug_Text.text = "Skull Kid skin equipped";
				Debug_Text2.text = "Skull kid unlocked: " + skullKidUnlockedp + " | Skull kid equipped : " + skullKidEquippedp;
				Debug_Text3.text = "Scarf unlocked : " + scarfUnlockedp + " | Scarf equipped : " + scarfEquippedp;
				Debug_Text4.text = "Teli equipped : " + teliEquippedp + " | " + Time.timeSinceLevelLoad.ToString();
			}
		}
		
		// If the equip text button for Scarf is clicked
		if(scarfEquipButtonp){
			// And the skin is eligible to be equipped
			if(Scarf_Equipped_text.text == "Equip"){
				// Equip the skin
				scarfEquippedp = true;
				Scarf_Equipped_text.text = "Equipped";
				
				// Hide plain unlocked sprite
				unlockedSprite_Scarf.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_Scarf.renderer.enabled = true;
				
				// Write to text file that this skin has been equipped
				WriteToTextFile(scarfEquippedTextFileName, true);
				
				// Unequip the other skins
				skullKidEquippedp = false;
				teliEquippedp = false;
				
				// Set the text for other skins to "Equip"
				SkullKid_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Hide the equipped sprites for other skins
				equippedSprite_Teli.renderer.enabled = false;
				equippedSprite_SkullKid.renderer.enabled = false;
				// Show the plain sprite for other skins
				unlockedSprite_Teli.renderer.enabled = true;
				unlockedSprite_SkullKid.renderer.enabled = true;
				
				// Write to text file that the other skins have been unequipped
				WriteToTextFile(skullKidEquippedTextFileName, false);
				WriteToTextFile(teliEquippedTextFileName, false);
				
				Debug_Text.text = "Scarf skin equipped";
				Debug_Text2.text = "Skull kid unlocked: " + skullKidUnlockedp + " | Skull kid equipped : " + skullKidEquippedp;
				Debug_Text3.text = "Scarf unlocked : " + scarfUnlockedp + " | Scarf equipped : " + scarfEquippedp;
				Debug_Text4.text = "Teli equipped : " + teliEquippedp + " | " + Time.timeSinceLevelLoad.ToString();
			}
		}
		
		// If the equip text button for Teli is clicked
		if(teliEquipButtonp){
			// And the skin is eligible to be equipped
			if(Teli_Equipped_text.text == "Equip"){
				// Equip the skin
				teliEquippedp = true;
				Teli_Equipped_text.text = "Equipped";
				
				// Hide plain unlocked sprite
				unlockedSprite_Teli.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_Teli.renderer.enabled = true;
				
				// Write to text file that this skin has been equipped
				WriteToTextFile(teliEquippedTextFileName, true);
				
				// Unequip the other skins
				skullKidEquippedp = false;
				scarfEquippedp = false;
				
				// Set the text for other skins to "Equip"
				SkullKid_Equipped_text.text = "Equip";
				Scarf_Equipped_text.text = "Equip";
				
				// Hide the equipped sprites for other skins
				equippedSprite_SkullKid.renderer.enabled = false;
				equippedSprite_Scarf.renderer.enabled = false;
				// Show the plain sprite for other skins
				unlockedSprite_SkullKid.renderer.enabled = true;
				unlockedSprite_Scarf.renderer.enabled = true;
				
				// Write to text file that the other skins have been unequipped
				WriteToTextFile(skullKidEquippedTextFileName, false);
				WriteToTextFile(scarfEquippedTextFileName, false);
				
				Debug_Text.text = "Teli skin equipped";
				Debug_Text2.text = "Skull kid unlocked: " + skullKidUnlockedp + " | Skull kid equipped : " + skullKidEquippedp;
				Debug_Text3.text = "Scarf unlocked : " + scarfUnlockedp + " | Scarf equipped : " + scarfEquippedp;
				Debug_Text4.text = "Teli equipped : " + teliEquippedp + " | " + Time.timeSinceLevelLoad.ToString();
			}
		}
	}
	
	// Helper function for writing to text files
	function WriteToTextFile(textFilePath : String, myBool : boolean){
		var sw = File.CreateText(Application.persistentDataPath + "\\" + textFilePath);
		sw.Write(myBool);
		sw.Close();
	}
}