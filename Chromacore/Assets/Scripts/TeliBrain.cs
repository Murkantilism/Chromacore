using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeliBrain : MonoBehaviour {

	public float xSpeed = 5f;

	bool jumped;
	
	// Animation states - Constants
	const int RunAnimationState = 0;
	const int PunchAnimationState = 1;
	const int JumpAnimationState = 2;
	const int FallAnimationState = 3;
	const int DeathAnimationState = 4;

	public float jumpHeight = 0.2f;
	private float deltaVelocity = -0.05f;
	private float precisionError = 0.05f;
	Rigidbody2D teliBody;
	
	Animator animator;

	bool teliFalling;
	bool shouldJump;

	float startPosition;

	float oldvel;
	float timeForVel;

	float time;

	void YouAreDead () {
		Debug.Log ("I am dead");
	}

	void DisableJumped () {
		jumped = false;
	}

	void Start () {
		time = 0;
		timeForVel = 0;
		teliFalling = false;

		animator = GetComponent<Animator> ();
		teliBody = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		// Moving character to right
		teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y);
	}

	void Update() {
		// Managing punching
		if (Input.GetKey (KeyCode.A) && animator.GetInteger ("state") == RunAnimationState)
			animator.SetInteger ("state", PunchAnimationState);

		// Managing falling
		if (teliBody.velocity.y < deltaVelocity) {
			// Falling
			teliFalling = true;
			if (animator.GetInteger("state") != FallAnimationState)
				animator.SetInteger("state", FallAnimationState);
		} else if (animator.GetInteger("state") == FallAnimationState && !jumped && teliBody.velocity.y >= oldvel) {
			// Make Teli run again
			teliFalling = false;
			animator.SetInteger("state", RunAnimationState);
		}

		// Managing jumping
		if (!teliFalling && !jumped && Input.GetKeyDown(KeyCode.Space) && animator.GetInteger ("state") == RunAnimationState) {
			animator.SetInteger ("state", JumpAnimationState);
			shouldJump = true;
			jumped = true;
			startPosition = this.transform.position.y;
			Invoke("DisableJumped", 0.8f);
		}

		if (shouldJump) {
			teliBody.velocity = new Vector2(teliBody.velocity.x, teliBody.velocity.y + 1);
			if (this.transform.position.y - startPosition > jumpHeight) {
				shouldJump = false;
				startPosition = this.transform.position.y;
				animator.SetInteger("state", FallAnimationState);
			}
		}

		timeForVel += Time.deltaTime;
		if (timeForVel > 0.07) {
			oldvel = teliBody.velocity.y;
			timeForVel = 0;
		}
	}
}
