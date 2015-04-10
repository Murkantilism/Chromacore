using UnityEngine;
using System.Collections;

public class MoveToRight : MonoBehaviour {

	Rigidbody2D cameraBody;
	float unit;

	// Use this for initialization
	void Start () {
		cameraBody = GetComponent<Rigidbody2D> ();
		cameraBody.velocity = new Vector2 (1.75f, 0f);
	}

	void Update () {
		if (this.transform.position.x >= 15f)
			cameraBody.velocity = new Vector2 (-1.75f, 0f);
		else if (this.transform.position.x <= -7)
			cameraBody.velocity = new Vector2 (1.75f, 0f);
	}
}
