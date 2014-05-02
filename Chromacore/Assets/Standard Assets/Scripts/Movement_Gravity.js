﻿#pragma strict

// Responsible for movement, gravity
class Movement_Gravity extends MonoBehaviour{
	var speed : float = 6.0;
	var jumpSpeed : float = 7.5; // NOTE: These values are superseded by
	var gravity : float = 14.0;  // the values set in the inspector!!!!
	var extraJump : float = 5.0; // Used to give jump extra boost if held
	var moveDirectionCap : float = 9.0;
	
	private var moveDirection : Vector3 = Vector3.zero;

	// Are we dead?
	var deadP : boolean = false;
	
	// Is Teli falling to his death?
	var fallingP : boolean = false;
	
	// Are we punching?
	var punchingP : boolean = false;
	
	var validTouch : boolean = false;

	// Used to recieve message from Teli_Animation.cs
	function death(bool : boolean){
		// Set dead boolean to the boolean value passed to it by 
		// either Reset() or ObstalceDeath() methods
		deadP = bool;
	}
	
	// Used to recieve message from Teli_Animation.cs
	function fallingDeath(bool : boolean){
		fallingP = bool;
	}
	
	// Used to recieve message from Teli_Animation.cs
	function punching(bool : boolean){
		punchingP = bool;
	}

	// Update is called once every frame
	function Update() {
		var controller : CharacterController = GetComponent(CharacterController);
		
		if (controller.isGrounded) {
			
			// We are grounded, so recalculate
			// move direction directly from axes
			moveDirection = Vector3(1, 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			// Jump on input
			if (Input.GetButton("Jump") && punchingP == false && deadP == false) {
				moveDirection.y = jumpSpeed;
			}
			
			// Touch screen support:

			// Detect the number of fingers touching screen
			var fingerCount = 0;

			for (var touch : Touch in Input.touches){
				if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled){
					fingerCount++;
				}
				
				// If the finger touch is in the top 3/4 of the screen, it's valid
				if (touch.position.y > (Screen.height / 4)){
					validTouch = true;
				}else{
					validTouch = false; // Otherwise, it's not
				}// This is to avoid the Punch and Pause GUI buttons from triggering jump
			}
			
			// If one finger is touching, jump
			if (fingerCount == 1 && punchingP == false && validTouch == true && deadP == false){
				moveDirection.y = jumpSpeed;
			}
		}
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;

		// Move the controller if we are falling to our death
		if(deadP == true && fallingP == true){
			Debug.Log("deadP: " + deadP + " || fallingP: " + fallingP); 
			controller.Move(moveDirection * Time.deltaTime);
		}
		
		// If we are "regular" dead, don't move controller
		if(deadP == true && fallingP == false){
			Debug.Log("deadP: " + deadP + " || fallingP: " + fallingP); 
			return;
		}
		
		// And otherwise move the controller
		
		controller.Move(moveDirection * Time.deltaTime);
		
		
		/*
		if (fallingP == true){
			controller.Move(moveDirection * Time.deltaTime);
		}else if (deadP == false && fallingP == false){
			controller.Move(moveDirection * Time.deltaTime);
		}*/
	}
}