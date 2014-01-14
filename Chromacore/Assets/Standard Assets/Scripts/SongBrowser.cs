using UnityEngine;
using System.Collections;

public class SongBrowser : MonoBehaviour {
	
	public GUIText textInstructions;
	
	// Use this for initialization
	void Start () {
		textInstructions.text = "Here in this game mode players can provide us with \na song & we'll auto-generate a new level just for you!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
	}
}
