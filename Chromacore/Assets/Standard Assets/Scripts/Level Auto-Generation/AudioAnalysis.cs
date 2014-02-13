using UnityEngine;
using System.Collections;

public class AudioAnalysis : MonoBehaviour {
	GenerateLevel genLevel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Starts the analysis of the mp3 file (invoked by
	// LoadingScreen.cs). Returns true once the analysis is complete.
	public void StartAnalysis(AudioClip myClip){
		// Do analysis here

		// Once audio analysis is done, call the level generation script
		genLevel = gameObject.GetComponent("GenerateLevel") as GenerateLevel;
		genLevel.ReadyToGenerate(true);
	}
}
