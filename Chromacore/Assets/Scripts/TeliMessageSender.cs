using UnityEngine;
using System.Collections;

public class TeliMessageSender : MonoBehaviour {

	GameObject teli;
	public string messageForTeli;

	// Use this for initialization
	void Start () {
		teli = GameObject.FindGameObjectWithTag ("AutoTeli");
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "AutoTeli") {
			teli.SendMessage(messageForTeli);
		}
	}
}
