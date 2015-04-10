using UnityEngine;
using System.Collections;

public class BackgroundManagement : MonoBehaviour {

	public GameObject background1;
	public GameObject background2;

	public GameObject struct1;
	public GameObject struct2;
	public GameObject struct3;

	float rightmostPositionX;
	float rightmostBackgroundPositionX;

	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		rightmostBackgroundPositionX = 78.9f;
		rightmostPositionX = struct3.transform.position.x;
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

		// Moving around structures
		if (mainCamera.transform.position.x >= rightmostPositionX) {
			if (rightmostPositionX == struct1.transform.position.x) {
				// Struct 2 follows
				struct2.transform.position = new Vector2(struct1.transform.position.x + 36f,
				                                         struct1.transform.position.y + 2.3f);
				struct2.SendMessage("GenerateBoxes");
				struct2.SendMessage("GenerateMisticBalls");
				rightmostPositionX = struct2.transform.position.x;
			} else if (rightmostPositionX == struct2.transform.position.x) {
				// Struct 3 follows
				struct3.transform.position = new Vector2(struct2.transform.position.x + 66f,
				                                         struct2.transform.position.y + 4.85f);
				struct3.SendMessage("GenerateBoxes");
				struct3.SendMessage("GenerateMisticBalls");
				rightmostPositionX = struct3.transform.position.x;
			} else if (rightmostPositionX == struct3.transform.position.x) {
				// Struct 1 follows
				struct1.transform.position = new Vector2(struct3.transform.position.x + 69.5f,
				                                         struct3.transform.position.y - 3.89f);
				struct1.SendMessage("GenerateBoxes");
				struct1.SendMessage("GenerateMisticBalls");
				rightmostPositionX = struct1.transform.position.x;
			}
		}
	}
}
