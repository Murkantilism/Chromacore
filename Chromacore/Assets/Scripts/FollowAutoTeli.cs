using UnityEngine;
using System.Collections;

public class FollowAutoTeli : MonoBehaviour {
	
	GameObject teli;
	Rigidbody2D cameraBody;
	Vector3 pos;
	bool shouldFollow;
	public GameObject background1;
	public GameObject background2;
	
	float unit;
	
	void UpdateVelocity() {
		cameraBody.velocity = new Vector2 (cameraBody.velocity.x + 0.025f, 0);
	}
	
	void StopFollowing() {
		shouldFollow = false;
	}
	
	void StartFollowing() {
		unit = 0f;
		
		cameraBody = GetComponent<Rigidbody2D> ();
		
		gameObject.transform.position = new Vector3 (teli.transform.position.x, teli.transform.position.y + 3.2f, -12.5f);
		cameraBody.velocity = new Vector2 (cameraBody.velocity.x + 4.5f, 0);
		
		shouldFollow = true;
	}
	
	// Use this for initialization
	void Start () {
		teli = GameObject.FindGameObjectWithTag ("AutoTeli");
		
		StartFollowing ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldFollow) {
			if (teli.transform.position.x < gameObject.transform.position.x - 9.35f) {
				StopFollowing();
			} else {
				gameObject.transform.position = new Vector3 (gameObject.transform.position.x, teli.transform.position.y + 3.2f, -12.5f);
				background1.transform.position = new Vector3(background1.transform.position.x,
				                                             teli.transform.position.y + 5.5f,
				                                             background1.transform.position.z);
				background2.transform.position = new Vector3(background2.transform.position.x,
				                                             teli.transform.position.y + 5.5f,
				                                             background2.transform.position.z);
			}
		} else {
			if (unit == 0)
				unit = cameraBody.velocity.x / 128f;
			if (cameraBody.velocity.x >= unit)
				cameraBody.velocity = new Vector2(cameraBody.velocity.x - unit, cameraBody.velocity.y);
			else
				cameraBody.velocity = new Vector2(0f, cameraBody.velocity.y);
		}
	}
}