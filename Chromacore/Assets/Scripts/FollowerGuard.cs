using UnityEngine;
using System.Collections;

public class FollowerGuard : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		GameObject.FindGameObjectWithTag ("Teli").SendMessage ("YouAreDead");
		GameObject.FindGameObjectWithTag ("MainCamera").SendMessage ("StopFollowing");
	}
}
