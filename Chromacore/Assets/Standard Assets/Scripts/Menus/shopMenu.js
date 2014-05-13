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
	
	var skullKidPurchasedTxt = "skullKidPurchasedTxt.txt";
	var scarfPurchasedTxt = "scarfPurchasedTxt.txt";

	var skullKidEquippedTxt = "skullKidEquippedTxt.txt";
	var scarfEquippedTxt = "scarfEquippedTxt.txt";
	
	var teliEquippedTxt = "teliEquippedTxt.txt";

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
	}
	
	function Update(){
		Debug_Text2.text = "Skull kid unlocked: " + skullKidUnlockedp + " | Skull kid equipped : " + skullKidEquippedp;
		Debug_Text3.text = "Scarf unlocked : " + scarfUnlockedp + " | Scarf equipped : " + scarfEquippedp;
		Debug_Text4.text = "Teli equipped : " + teliEquippedp;
		
		// If the skull kid skin is unlocked and equipped and the other skins aren't equipped
		if(skullKidUnlockedp == true && skullKidEquippedp == true && scarfEquippedp == false && teliEquippedp == false){
			// Equip it
			SkullKid_Equipped_text.text = "Equipped";
			
			// Write to text file that this skin has been equipped
			var swSkullKid_equipped = File.CreateText(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
			swSkullKid_equipped.Write("true");
			swSkullKid_equipped.Close();
			
			// Unequip other skins
			scarfEquippedp = false;
			teliEquippedp = false;
			
			// Set the text for other skins to be eligible for equip
			Scarf_Equipped_text.text = "Equip";
			Teli_Equipped_text.text = "Equip";
			
			// Hide plain unlocked sprite
			unlockedSprite_SkullKid.renderer.enabled = false;
			// Show green equipped sprite
			equippedSprite_SkullKid.renderer.enabled = true;
			
			// Write to text files that the other skins have been unequipped
			WriteScarfEquippedFalse();
			WriteTeliEquippedFalse();
		}
		
		// If the scarf skin is unlocked and equipped and the other skins aren't equipped
		if(scarfUnlockedp == true && scarfEquippedp == true && skullKidEquippedp == false && teliEquippedp == false){
			// Equip it
			Scarf_Equipped_text.text = "Equipped";
			
			// Write to text file that this skin has been equipped
			var swScarf_equipped = File.CreateText(Application.persistentDataPath + "\\" + scarfEquippedTxt);
			swScarf_equipped.Write("true");
			swScarf_equipped.Close();
			
			// Unequip other skins
			skullKidEquippedp = false;
			teliEquippedp = false;

			// Set the text for other skins to be eligible for equip
			SkullKid_Equipped_text.text = "Equip";
			Teli_Equipped_text.text = "Equip";
			
			// Hide plain unlocked sprite
			unlockedSprite_Scarf.renderer.enabled = false;
			// Show green equipped sprite
			equippedSprite_Scarf.renderer.enabled = true;
			
			// Write to text files that the other skins have been unequipped
			WriteSkullKidEquippedFalse();
			WriteTeliEquippedFalse();
		}
		
		// If the Teli skin is equipped and the other skins aren't
		if(teliEquipButtonp == true && skullKidEquippedp == false && scarfEquippedp == false){
			// Equip it
			Teli_Equipped_text.text = "Equipped";
			
			// Write to text file that this skin has been equipped
			var swTeli_equipped = File.CreateText(Application.persistentDataPath + "\\" + teliEquippedTxt);
			swTeli_equipped.Write("true");
			swTeli_equipped.Close();
			
			// Unequip other skins
			skullKidEquippedp = false;
			scarfEquippedp = false;
			
			// Set the text for other skins to be eligible for equip
			SkullKid_Equipped_text.text = "Equip";
			Scarf_Equipped_text.text = "Equip";
			
			// Hide plain unlocked sprite
			unlockedSprite_Teli.renderer.enabled = false;
			// Show green equipped sprite
			equippedSprite_Teli.renderer.enabled = true;
			
			// Write to text files that the other skins have been unequipped
			WriteSkullKidEquippedFalse();
			WriteScarfEquippedFalse();
		}
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
		if(skullKidEquipButtonp && skullKidUnlockedp == true){
			// Equip it
			skullKidEquippedp = true;
		
		/*
			// and it's not already equipped
			if(SkullKid_Equipped_text.text == "Equip"){
				
				
				
				SkullKid_Equipped_text.text = "Equipped";
				
				// Write to text file that this skin has been equipped
				var swSkullKid_equipped = File.CreateText(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
				swSkullKid_equipped.Write("true");
				swSkullKid_equipped.Close();
				
				// Unequip other skins
				scarfEquippedp = false;
				teliEquippedp = false;
				
				// Set the text for other skins to be eligible for equip
				Scarf_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Hide plain unlocked sprite
				unlockedSprite_SkullKid.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_SkullKid.renderer.enabled = true;
				
				// Write to text files that the other skins have been unequipped
				WriteScarfEquippedFalse();
				WriteTeliEquippedFalse();
				
			}*/
		// If the scarf equip button is clicked, and it's unlocked
		}else if(scarfEquipButtonp && scarfUnlockedp == true){
			// Equip it
			scarfEquippedp = true;
		
		/*
			// and it's not already equipped
			if(Scarf_Equipped_text.text == "Equip"){
				
				Scarf_Equipped_text.text = "Equipped";
				
				// Write to text file that this skin has been equipped
				var swScarf_equipped = File.CreateText(Application.persistentDataPath + "\\" + scarfEquippedTxt);
				swScarf_equipped.Write("true");
				swScarf_equipped.Close();
				
				// Unequip other skins
				skullKidEquippedp = false;
				teliEquippedp = false;

				// Set the text for other skins to be eligible for equip
				SkullKid_Equipped_text.text = "Equip";
				Teli_Equipped_text.text = "Equip";
				
				// Hide plain unlocked sprite
				unlockedSprite_Scarf.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_Scarf.renderer.enabled = true;
				
				// Write to text files that the other skins have been unequipped
				WriteSkullKidEquippedFalse();
				WriteTeliEquippedFalse();
				
			}*/
		// If the Teli equip button is clicked (no need to check if it's unlocked)
		}else if(teliEquipButtonp){
			// Equip it
			teliEquippedp = true;
			
			/*
			// and it's not already equipped
			if(Teli_Equipped_text.text == "Equip"){

				Teli_Equipped_text.text = "Equipped";
				
				// Write to text file that this skin has been equipped
				var swTeli_equipped = File.CreateText(Application.persistentDataPath + "\\" + teliEquippedTxt);
				swTeli_equipped.Write("true");
				swTeli_equipped.Close();
				
				// Unequip other skins
				skullKidEquippedp = false;
				scarfEquippedp = false;
				
				// Set the text for other skins to be eligible for equip
				SkullKid_Equipped_text.text = "Equip";
				Scarf_Equipped_text.text = "Equip";
				
				// Hide plain unlocked sprite
				unlockedSprite_Teli.renderer.enabled = false;
				// Show green equipped sprite
				equippedSprite_Teli.renderer.enabled = true;
				
				// Write to text files that the other skins have been unequipped
				WriteSkullKidEquippedFalse();
				WriteScarfEquippedFalse();
				
			}*/
		}
	}
	
	// Helper functions for writing to text files based on which skin is equipped
	function WriteTeliEquippedFalse(){
		var swTeli_equipped_f = File.CreateText(Application.persistentDataPath + "\\" + teliEquippedTxt);
		swTeli_equipped_f.Write("false");
		swTeli_equipped_f.Close();
	}
	
	function WriteScarfEquippedFalse(){
		var swScarf_equipped_f = File.CreateText(Application.persistentDataPath + "\\" + scarfEquippedTxt);
		swScarf_equipped_f.Write("false");
		swScarf_equipped_f.Close();
	}
	
	function WriteSkullKidEquippedFalse(){
		var swSkullKid_equipped_f = File.CreateText(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
		swSkullKid_equipped_f.Write("false");
		swSkullKid_equipped_f.Close();
	}
	
	// If the skull kid skin has been bought (recieves message from ChromacoreEventHandler.cs)
	function skullKid_skinBought(boughtP : boolean){
		if(boughtP == true){
			Debug_Text.text = "skull kid skin purchased";
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
			var swSkullKid = File.CreateText(Application.persistentDataPath + "\\" + skullKidPurchasedTxt);
			swSkullKid.Write("true");
			swSkullKid.Close();
			
			/*
			// Write to text file that this skin has been equipped
			var swSkullKid_equipped = File.CreateText(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
			swSkullKid_equipped.Write("true");
			swSkullKid_equipped.Close();
			
			// Unequip other skins
			scarfEquippedp = false;
			teliEquippedp = false;
			// Set the text for other skins to be eligible for equip
			Scarf_Equipped_text.text = "Equip";
			Teli_Equipped_text.text = "Equip";
			// Write to text files that the other skins have been unequipped
			WriteScarfEquippedFalse();
			WriteTeliEquippedFalse();
			*/
		}else{
			// Otherwise, set boolean flag to false, and write to text files 
			// that this skin isn't unlocked or equipped
			skullKidUnlockedp = false;
			var swSkullKid_f = File.CreateText(Application.persistentDataPath + "\\" + skullKidPurchasedTxt);
			swSkullKid_f.Write("false");
			swSkullKid_f.Close();
			
			skullKidEquippedp = false;
			var swSkullKid_equipped_f = File.CreateText(Application.persistentDataPath + "\\" + skullKidEquippedTxt);
			swSkullKid_equipped_f.Write("false");
			swSkullKid_equipped_f.Close();
		}
	}
	
	// If the scarf skin has been bought (recieves message from ChromacoreEventHandler.cs)
	function scarf_skinBought(boughtP : boolean){
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
			var swScarf = File.CreateText(Application.persistentDataPath + "\\" + scarfPurchasedTxt);
			swScarf.Write("true");
			swScarf.Close();
			
			/*
			// Write to text file that this skin has been equipped
			var swScarf_equipped = File.CreateText(Application.persistentDataPath + "\\" + scarfEquippedTxt);
			swScarf_equipped.Write("true");
			swScarf_equipped.Close();
			
			// Unequip other skins
			skullKidEquippedp = false;
			teliEquippedp = false;
			// Set the text for other skins to be eligible for equip
			SkullKid_Equipped_text.text = "Equip";
			Teli_Equipped_text.text = "Equip";
			// Write to text files that the other skins have been unequipped
			WriteSkullKidEquippedFalse();
			WriteTeliEquippedFalse();
			*/
		}else{
			
			// Otherwise, set boolean flag to false, and write to text files 
			// that this skin isn't unlocked or equipped
			scarfUnlockedp = false;
			var swScarf_f = File.CreateText(Application.persistentDataPath + "\\" + scarfPurchasedTxt);
			swScarf_f.Write("false");
			swScarf_f.Close();
			
			scarfEquippedp = false;
			var swScarf_equipped_f = File.CreateText(Application.persistentDataPath + "\\" + scarfEquippedTxt);
			swScarf_equipped_f.Write("false");
			swScarf_equipped_f.Close();
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