using UnityEngine;
using System.Collections;

public class TestNote : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			Debug.Log("TIME: " + Time.timeSinceLevelLoad);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
