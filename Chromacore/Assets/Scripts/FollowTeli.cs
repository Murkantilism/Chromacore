using UnityEngine;
using System.Collections;

public class FollowTeli : MonoBehaviour {

	GameObject teli;
	Rigidbody2D cameraBody;
	Vector3 pos;
	bool shouldFollow;
	public GameObject background1;
	public GameObject background2;

	void StopFollowing() {
		shouldFollow = false;
	}

	void StartFollowing() {
		cameraBody = GetComponent<Rigidbody2D> ();

		this.transform.position = new Vector3 (teli.transform.position.x, teli.transform.position.y + 3.2f, -12.5f);
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
		if (shouldFollow) {
			this.transform.position = new Vector3 (this.transform.position.x, teli.transform.position.y + 3.2f, -12.5f);
			background1.transform.position = new Vector3(background1.transform.position.x,
			                                             teli.transform.position.y + 5.5f,
			                                             background1.transform.position.z);
			background2.transform.position = new Vector3(background2.transform.position.x,
			                                             teli.transform.position.y + 5.5f,
			                                             background2.transform.position.z);
		} else {
			if (cameraBody.velocity.x >= 0.02f)
				cameraBody.velocity = new Vector2(cameraBody.velocity.x - 0.02f, cameraBody.velocity.y);
			else
				cameraBody.velocity = new Vector2(0f, cameraBody.velocity.y);
		}
	}
}
