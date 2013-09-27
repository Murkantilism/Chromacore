using UnityEngine;
using System.Collections;

public class Collect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Pickup(AudioClip sound)
	{
		//AudioSource.PlayClipAtPoint(sound, transform.position);
		Debug.Log("Play sound");
	}
}