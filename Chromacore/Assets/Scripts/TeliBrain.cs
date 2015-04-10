using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeliBrain : MonoBehaviour {

	Animator teliAnimator;
	GameObject mainCamera;
	GameObject scoringSystem;
	public float xSpeed = 4.5f;

	bool jumped;

	// Animation states - Constants
	const int RunAnimationState = 0;
	const int PunchAnimationState = 1;
	const int JumpAnimationState = 2;
	const int FallAnimationState = 3;

	public float jumpHeight = 0.2f;
	private float deltaVelocity = -0.1f;
	private const float errorVel = 0.05f;
	Rigidbody2D teliBody;

	float levelTime;

	bool teliFalling;
	bool shouldJump;

	float startPosition;

	float oldvel;
	float timeForVel;
	
	bool dead;

	void YouAreDead () {
		if (!dead) {
			dead = true;
			xSpeed = 0f;
			// Putting teli in a "safe" position, but only if he died by not catching up with the camera
			if (gameObject.transform.position.x < mainCamera.transform.position.x - 9.35f)
				gameObject.transform.position = new Vector3(-1000f, -1000f, 1000f);
			scoringSystem.SendMessage ("RegisterScore");

			// Do whatever you want after teli dies
			// ...
		}
	}

	void DisableJumped () {
		jumped = false;
	}

	void Start () {
		dead = false;
		timeForVel = 0;
		levelTime = 0;
		teliFalling = false;

		teliBody = GetComponent<Rigidbody2D> ();

		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		scoringSystem = GameObject.FindGameObjectWithTag ("ScoringSystem");
		teliAnimator = GetComponent<Animator> ();
	}

	void FixedUpdate() {
		// Moving character to right
		teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y);
	}

	void Update() {
		// Managing punching
		if (Input.GetKey (KeyCode.A) && teliAnimator.GetInteger ("state") == RunAnimationState)
			teliAnimator.SetInteger ("state", PunchAnimationState);

		// Managing falling
		if (teliBody.velocity.y < deltaVelocity) {
			// Falling
			teliFalling = true;
			if (teliAnimator.GetInteger("state") != FallAnimationState)
				teliAnimator.SetInteger("state", FallAnimationState);
		} else if (teliAnimator.GetInteger("state") == FallAnimationState && !jumped && (teliBody.velocity.y >= oldvel || Mathf.Abs(teliBody.velocity.y) <= errorVel)) {
			// Make Teli run again
			teliFalling = false;
			teliAnimator.SetInteger("state", RunAnimationState);
		}

		// Managing jumping
		if (!teliFalling && !jumped && Input.GetKeyDown(KeyCode.Space) && teliAnimator.GetInteger ("state") == RunAnimationState) {
			teliAnimator.SetInteger ("state", JumpAnimationState);
			shouldJump = true;
			jumped = true;
			startPosition = gameObject.transform.position.y;
			Invoke("DisableJumped", 0.8f);
		}

		if (shouldJump) {
			teliBody.velocity = new Vector2(teliBody.velocity.x, teliBody.velocity.y + 1);
			if (gameObject.transform.position.y - startPosition > jumpHeight) {
				shouldJump = false;
				startPosition = gameObject.transform.position.y;
				teliAnimator.SetInteger("state", FallAnimationState);
			}
		}

		timeForVel += Time.deltaTime;
		if (timeForVel > 0.07) {
			oldvel = teliBody.velocity.y;
			timeForVel = 0;
		}

		levelTime += Time.deltaTime;
		if (levelTime > 3f) {
			if (xSpeed < 7f) {
				xSpeed += 0.025f;
				teliAnimator.speed += 0.005f;
				mainCamera.SendMessage("UpdateVelocity");
			}

			levelTime = 0;
		}
	}
}
