#pragma strict

// Responsible for movement, gravity
class Movement_Gravity extends MonoBehaviour{
	var speed : float = 6.0;
	var jumpSpeed : float = 8.0;
	var gravity : float = 20.0;
	
	private var moveDirection : Vector3 = Vector3.zero;

	// Are we not dead?
	var notdeadp = true;

	// Used to recieve message from Teli_Animation.cs
	function death(bool){
		// Set dead boolean to the boolean value passed to it by 
		// either Reset() or ObstalceDeath() methods
		notdeadp = bool;
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
			if (Input.GetButton ("Jump")) {
				moveDirection.y = jumpSpeed;
			}
			
			// Touch screen support:

			// Detect the number of fingers touching screen
			var fingerCount = 0;

			for (var touch : Touch in Input.touches){
				if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled){
					fingerCount++;
				}
			}
			
			// If one finger is touching, jump
			if (fingerCount == 1){
				moveDirection.y = jumpSpeed;
			}
		}
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller if we are not dead and the game is not paused
		if (notdeadp == true){
			controller.Move(moveDirection * Time.deltaTime);
		}
	}
}