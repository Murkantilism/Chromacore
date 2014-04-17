using UnityEngine;
using System.Collections;

// This script is used to enable the title track to continue playing
// across all 3 menu screens uninterrupted (MainMenu, LevelSelect, Credits)

// NOTE: This script must be attached to anything OTHER THAN the MainCamera
// object in the MainMenu scene. Otherwise music will persist into Levels.
public class menuPersistentMusic : MonoBehaviour {

	public Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindObjectOfType<Camera>() as Camera;

		if (! (Application.loadedLevelName == "MainMenu")){
			Destroy(mainCamera.transform);
		}else{
			DontDestroyOnLoad(mainCamera.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
