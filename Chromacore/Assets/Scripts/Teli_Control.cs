using UnityEngine;
using System.Collections;

public class Teli_Control : MonoBehaviour {

	Rigidbody2D teliBody;
	float oldPosition;
	bool jumping;
	bool onGround;
	float jumpTime;

	public float xSpeed = 3f;
	public float jumpDuration = 0.1f;
	public float jumpSpeed = 1.0f;

	// Use this for initialization
	void Start () {
		teliBody = GetComponent<Rigidbody2D> ();
		oldPosition = teliBody.position.y;
	}

	void Jump() {
		if (onGround) {
			jumping = true;
			jumpTime = 0;
		}
	}

	void FixedUpdate () {
		jumpTime += Time.fixedDeltaTime;

		if (jumpTime >= jumpDuration)
			jumping = false;

		if (oldPosition > teliBody.position.y) {
			gameObject.SendMessage ("BeginFalling");
			onGround = false;
		} else if (oldPosition < teliBody.position.y) {
			onGround = false;
			gameObject.SendMessage ("StopFalling");
		} else {
			gameObject.SendMessage ("StopFalling");
			onGround = true;
		}

		oldPosition = teliBody.position.y;
		if (!jumping)
			teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y);
		else
			teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y + jumpSpeed);
	}
}
