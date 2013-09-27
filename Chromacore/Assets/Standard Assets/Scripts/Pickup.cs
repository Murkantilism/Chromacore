using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	
	public AudioClip collectSound;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player")
		{
			gameObject.SendMessage("Pickup", collectSound);
			Destroy(gameObject);
		}
	}
		
}
