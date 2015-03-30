using UnityEngine;
using System.Collections;

public class FollowTeli : MonoBehaviour {

	GameObject teli;

	// Use this for initialization
	void Start () {
		teli = GameObject.FindGameObjectWithTag ("Teli");
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (teli.transform.position.x, teli.transform.position.y + 3.15f, -12.5f);
	}
}
