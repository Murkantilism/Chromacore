using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeliAutoBrain : MonoBehaviour {
	
	Animator teliAnimator;
	GameObject mainCamera;
	public float xSpeed = 4.5f;
	
	bool jumped;
	
	// Animation states - Constants
	const int RunAnimationState = 0;
	const int PunchAnimationState = 1;
	const int JumpAnimationState = 2;
	const int FallAnimationState = 3;
	const int DeathAnimationState = 4;
	
	public float jumpHeight = 0.2f;
	private float deltaVelocity = -0.05f;
	Rigidbody2D teliBody;
	
	float levelTime;

	float timeForVel;
	bool shouldJump;
	
	float startPosition;
	
	float oldvel;

	// Auto-control methods
	public void Jump() {
		teliAnimator.SetInteger ("state", JumpAnimationState);
		shouldJump = true;
		jumped = true;
		startPosition = gameObject.transform.position.y;
		Invoke("DisableJumped", 0.8f);
	}
	
	public void Punch() {
		teliAnimator.SetInteger ("state", PunchAnimationState);
	}


	void DisableJumped () {
		jumped = false;
	}
	
	void Start () {
		timeForVel = 0;
		levelTime = 0;
		
		teliBody = GetComponent<Rigidbody2D> ();
		
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		teliAnimator = GetComponent<Animator> ();
	}
	
	void FixedUpdate() {
		// Moving character to right
		teliBody.velocity = new Vector2 (xSpeed, teliBody.velocity.y);
	}
	
	void Update() {
		// Managing falling
		RaycastHit2D hitBox = Physics2D.BoxCast (new Vector2 (this.transform.position.x, this.transform.position.y), new Vector2 (0.25f, 4f), 0, new Vector2 (0.1f, 0f));
		if (hitBox)
			if (hitBox.collider.gameObject.tag == "Box")
				Punch ();

		if (teliBody.velocity.y < deltaVelocity) {
			// Falling
			if (teliAnimator.GetInteger("state") != FallAnimationState)
				teliAnimator.SetInteger("state", FallAnimationState);
		} else if (teliAnimator.GetInteger("state") == FallAnimationState && !jumped && teliBody.velocity.y >= oldvel) {
			// Make Teli run again
			teliAnimator.SetInteger("state", RunAnimationState);
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
