using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeliBrain : MonoBehaviour {
	
	public AudioSource backgroundTrack; // The background music track (used to reset after death)
	public float xSpeed = 5f;

	// Animation states - Constants
	const int RunAnimationState = 0;
	const int PunchAnimationState = 1;
	const int JumpAnimationState = 2;
	const int FallAnimationState = 3;
	const int DeathAnimationState = 4;
	const int GlowRunAnimationState = 5;

	private float deltaVelocity = -0.15f;
	Rigidbody2D teliBody;
	
	Animator animator;
	GameObject scoringSystem;
	
	int misticBalls;

	bool teliFalling;
	bool shouldJump;

	float time;
	
	void CollectMisticBall() {
		misticBalls++;
		scoringSystem.SendMessage ("UpdateScore");
		if (animator.GetInteger ("state") != GlowRunAnimationState)
			animator.SetInteger("state", GlowRunAnimationState);
	}
	
	void Start () {
		misticBalls = 0;
		time = 0;
		teliFalling = false;

		animator = GetComponent<Animator> ();
		teliBody = GetComponent<Rigidbody2D> ();

		scoringSystem = GameObject.FindGameObjectWithTag ("ScoringSystem");
	}

	void FixedUpdate() {
		// Managing falling
		if (teliBody.velocity.y < deltaVelocity) {
			// Falling
			Debug.Log ("Falling");
			teliFalling = true;
			if (animator.GetInteger("state") != FallAnimationState)
				animator.SetInteger("state", FallAnimationState);
		} else if (animator.GetInteger("state") == FallAnimationState) {
			// Make Teli run again
			teliFalling = false;
			animator.SetInteger("state", RunAnimationState);
		}

		// Moving character to right
		teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y);


		// Managing jumping
		if (!teliFalling && Input.GetKeyDown(KeyCode.Space) && animator.GetInteger ("state") == RunAnimationState) {
			Debug.Log("Jump");
			animator.SetInteger ("state", JumpAnimationState);
			shouldJump = true;
		}

		if (shouldJump) {
			teliBody.velocity = new Vector2(teliBody.velocity.x, teliBody.velocity.y + 1);
			time += Time.fixedDeltaTime;
			if (time >= 0.1) {
				time = 0;
				shouldJump = false;
				animator.SetInteger("state", RunAnimationState);
			}
		}
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.A) && animator.GetInteger ("state") == RunAnimationState)
			animator.SetInteger ("state", PunchAnimationState);
	}
}