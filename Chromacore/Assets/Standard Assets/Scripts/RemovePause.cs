using UnityEngine;
using System.Collections;

// Attached in the MainMenu, used to prevent any Pause persistence issues
public class RemovePause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
