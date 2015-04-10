using UnityEngine;
using System.Collections;

public class BoardBrain : MonoBehaviour {

	GameObject mainCamera;
	GUIText scoreLabel;
	Renderer boardRenderer;
	GameObject[] boardButtons;

	bool shouldUpdate;

	void InitGUI(string labelToSet) {
		shouldUpdate = true;
		scoreLabel.enabled = true;
		boardRenderer.enabled = true;

		scoreLabel.text = labelToSet;

		foreach (GameObject boardButton in boardButtons)
			boardButton.SetActive(true);
	}

	// Use this for initialization
	void Start () {
		shouldUpdate = false;
		boardRenderer = GetComponent<Renderer> ();
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		GameObject scoreBoardGUI = GameObject.FindGameObjectWithTag ("ScoreBoardGUI");

		scoreLabel = scoreBoardGUI.GetComponent<GUIText>();

		boardButtons = GameObject.FindGameObjectsWithTag ("BoardButton");
		foreach (GameObject boardButton in boardButtons)
			boardButton.SetActive(false);

		scoreLabel.enabled = false;
		boardRenderer.enabled = false;
		scoreLabel.pixelOffset = new Vector2(Screen.width/2, Screen.height/2 + 20);
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldUpdate) {
			gameObject.transform.parent.position = new Vector3 (mainCamera.transform.position.x, mainCamera.transform.position.y, -1);
		}
	}
}
