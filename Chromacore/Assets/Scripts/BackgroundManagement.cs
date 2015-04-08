using UnityEngine;
using System.Collections;

public class BackgroundManagement : MonoBehaviour {

	public GameObject background1;
	public GameObject background2;

	float rightmostBackgroundPositionX;

	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		rightmostBackgroundPositionX = 78.9f;
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.transform.position.x >= rightmostBackgroundPositionX) {
			if (background1.transform.position.x < background2.transform.position.x) {
				// Should move background 1
				background1.transform.position = new Vector2(background2.transform.position.x + 78.9f,
				                                             background1.transform.position.y);
				rightmostBackgroundPositionX = background1.transform.position.x;
			} else {
				// Should move background 2
				background2.transform.position = new Vector2(background1.transform.position.x + 78.9f,
				                                             background2.transform.position.y);
				rightmostBackgroundPositionX = background2.transform.position.x;
			}
		}
	}
}
