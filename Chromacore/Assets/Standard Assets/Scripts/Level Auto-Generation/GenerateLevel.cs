using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {
	LoadingScreen loadScreen;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Invoked when LoadingScreen.cs is done
	public void ReadyToGenerate(bool readyP){
		if (readyP){
			// Invoke x-position calculator script

			// Invoke automatic note placement script

			// Invoke automatic level platform placement script (which
			// will be based on the position of notes)


			// Once level generation is done, call LoadingScreen.cs
			loadScreen = gameObject.GetComponent("LoadingScreen") as LoadingScreen;
			loadScreen.LevelGenerationComplete(true);
		}

	}
}
