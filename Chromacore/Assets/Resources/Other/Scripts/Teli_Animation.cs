using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Teli_Animation : MonoBehaviour {

	// Animation states
	const int RunAnimationState = 0;
	const int PunchAnimationState = 1;
	const int JumpAnimationState = 2;
	const int FallAnimationState = 3;
	const int DeathAnimationState = 4;
	const int GlowRunAnimationState = 5;

	// The main character's Charcter Controller	
	// CharacterController teliCharacter;
	
	// Value of character's Y velocity to begin falling animation at
	private float fallAnimVel = -0.25f;
	
	Animator animator;
	GameObject scoringSystem;

	// State variable to check if player is running (running by default)
	public bool runningP = true;
	
	// The background music track (used to reset after death)
	public AudioSource backgroundTrack;

	int misticBalls;

	void CollectMisticBall() {
		misticBalls++;
		scoringSystem.SendMessage ("UpdateScore");
		if (animator.GetInteger ("state") != GlowRunAnimationState)
			animator.SetInteger("state", GlowRunAnimationState);
	}

	void Start () {
		misticBalls = 0;

		animator = GetComponent<Animator> ();
	//	teliCharacter = GetComponent<CharacterController> ();
		scoringSystem = GameObject.FindGameObjectWithTag ("ScoringSystem");
	}

	void BeginFalling() {
		if (animator.GetInteger ("state") != FallAnimationState) {
			animator.SetInteger ("state", FallAnimationState);
		}
	}

	void StopFalling() {
		if (animator.GetInteger ("state") == FallAnimationState)
			animator.SetInteger("state", RunAnimationState);
	}
		
	void Update() {
		if (Input.GetKey (KeyCode.Space) && animator.GetInteger ("state") != JumpAnimationState) {
			gameObject.SendMessage("Jump");
			animator.SetInteger ("state", JumpAnimationState);
		}
		if (Input.GetKey (KeyCode.A) && animator.GetInteger ("state") == RunAnimationState)
			animator.SetInteger ("state", PunchAnimationState);
	}
/*
	void Update () {
		// Jump Animation
		if (Input.GetKey(KeyCode.Space)){
			// Only play the clip if it is not already playing.
			// Calling play will restart the clip if it is already playing
			// Jump
			animator.SetTrigger("Jump");
		}
		
		// If the Player presses the punch button
		if (Input.GetKey(KeyCode.A)){
			// And the charcter isn't currently punching
			animator.SetTrigger("Punch");
		}
		
		// If none of the other animations are playing
		/*
		if (!anim.IsPlaying("Run") & !anim.IsPlaying("Jump") & !anim.IsPlaying("Run_Glow") & !anim.IsPlaying("Punch") & !anim.IsPlaying("Death")){
			// Loop running animation
			anim.Play("Run");
		}
		
		// If the character is falling

		if (teliCharacter.velocity.y < fallAnimVel){
			// And the falling animation isn't already playing
			if (animator.GetInteger("state") != FallAnimationState){
				animator.SetInteger("state", FallAnimationState);
			}
		} else if (animator.GetInteger("state") == FallAnimationState)
			animator.SetInteger ("state", RunAnimationState);
		// Wait to invoke death animation function until a few
		// seconds after game has begun.
		//Invoke("DeathAnimation", 3);
	}
*/
	
	// Handles death by Edges (death by obstacles is 
	// handled in ObstacleDeath() function)
	void DeathAnimation(){
		// If Teli's X-position stops increasing or the Y position is below level
		//if (teliCharacter.velocity.x < 1 || teliCharacter.transform.position.y < -5){
			//Debug.Log(teliCharacter.velocity.x);
			Debug.Log("DEATH");
			scoringSystem.SendMessage("SetCharacterDead");
			/*
			// And the death animation isn't already playing
			if(!anim.IsPlaying("Death")){
				// Play the death animation
				anim.Play("Death");
			}*/
			// Call reset function after 2 seconds
			Invoke("Reset", 1);
		//}
	}
	
	// Reset Teli's position, the background track, and respawn Notes
	void Reset(){

	}
	
	// Hanlde Glow Animation
	void GlowAnimation(){/*
		if(!anim.IsPlaying("Run_Glow")){
			anim.Stop();
			anim.Play("Run_Glow");
		}*/
	}
	
	// On death, stop movement and if the PunchAnimation isn't 
	// already playing	when Death broadcast is recieved, play it.
	void ObstacleDeath(){/*
		if(!anim.IsPlaying("Punch")){
			Debug.Log("DEATH - Wasn't punching");
			// And the death animation isn't already playing
			if(!anim.IsPlaying("Death")){
				// Play the death animation
				anim.Play("Death");
				// Send a message to stop Teli's movement
				SendMessageUpwards("death", false);
			}
			// Call reset function after 2 seconds
			Invoke("Reset", 1);
		}*/
	}
}