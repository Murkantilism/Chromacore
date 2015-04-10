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
		if (this.transform.position.x >= 13f) {
			if (unit == 0)
				unit = cameraBody.velocity.x / 128f;
			if (cameraBody.velocity.x >= unit)
				cameraBody.velocity = new Vector2 (cameraBody.velocity.x - unit, cameraBody.velocity.y);
			else
				cameraBody.velocity = new Vector2 (0f, cameraBody.velocity.y);
		}
	}
}
