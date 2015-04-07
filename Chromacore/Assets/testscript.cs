using UnityEngine;
using System.Collections;

public class testscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.forward = Vector3.Normalize(new Vector3(1f, -1f, 0f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
