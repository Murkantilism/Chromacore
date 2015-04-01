using UnityEngine;
using System.Collections;

public class FollowTeli : MonoBehaviour {

	GameObject teli;
	Rigidbody2D cameraBody;
	Vector3 pos;
	bool shouldFollow;

	void StopFollowing() {
		shouldFollow = false;
	}

	void StartFollowing() {
		cameraBody = GetComponent<Rigidbody2D> ();

		this.transform.position = new Vector3 (teli.transform.position.x, teli.transform.position.y + 3.15f, -12.5f);
		cameraBody.velocity = new Vector2 (cameraBody.velocity.x + 4.5f, 0);

		shouldFollow = true;
	}

	// Use this for initialization
	void Start () {
		teli = GameObject.FindGameObjectWithTag ("Teli");

		StartFollowing ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldFollow)
			this.transform.position = new Vector3 (this.transform.position.x, teli.transform.position.y + 3.15f, -12.5f);
		else {
			if (cameraBody.velocity.x >= 0.02f)
				cameraBody.velocity = new Vector2(cameraBody.velocity.x - 0.02f, cameraBody.velocity.y);
			else
				cameraBody.velocity = new Vector2(0f, cameraBody.velocity.y);
		}
	}
}
