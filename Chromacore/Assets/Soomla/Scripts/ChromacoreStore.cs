using UnityEngine;
using System.Collections;
using Soomla;

public class ChromacoreStore : StoreEvents {
	private static ChromacoreStore instance = null;

	private static ChromacoreEventHandler handler;

	GameObject mainCamera;

	GameObject shopMenu;

	void Awake(){
		if(instance == null){ 	//making sure we only initialize one instance.
			instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		} else {					//Destroying unused instances.
			GameObject.Destroy(this);
		}
	}

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");

		handler = new ChromacoreEventHandler();
		
		StoreController.Initialize(new ChromacoreStoreAssets());

		shopMenu = GameObject.Find("Shop Text");
		
		// Initialization of 'ExampleLocalStoreInfo' and some example usages in ExampleEventHandler.onStoreControllerInitialized
	}

	void Update(){
		if(StoreInventory.NonConsumableItemExists("skull_kid_skin")){
			shopMenu.SendMessage("skullKid_skinBought", true);
		}else if(StoreInventory.NonConsumableItemExists("scarf_skin")){
			shopMenu.SendMessage("scarf_skinBought", true);
		}
	}

	void buySkin(string skinID){
		StoreInventory.BuyItem(skinID);
	}

	void OnGUI(){
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 25;
		
		if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
			#if UNITY_ANDROID && !UNITY_EDITOR
			StoreController.StopIabServiceInBg();
			#endif
			GameObject.Destroy(this);
			Destroy(instance);
			Destroy(mainCamera);
			Application.LoadLevel("MainMenu");
		}

		/*if (GUI.Button(new Rect(Screen.width/2 - Screen.width/4, Screen.height/2 + Screen.height/4, 250, 100), "Activate Store", buttonStyle)){
			#if UNITY_ANDROID && !UNITY_EDITOR
			StoreController.StartIabServiceInBg();
			#endif
		}*/
	}
}
