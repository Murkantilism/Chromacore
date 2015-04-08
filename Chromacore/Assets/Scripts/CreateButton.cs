using UnityEngine;
using System.Collections;

public class CreateButton : MonoBehaviour {

	public string messageToSend;
	public Vector2 pos;
	public float width;
	public float height;

	public Sprite buttonImage;

	GameObject manager;

	void Start() {
		manager = GameObject.FindGameObjectWithTag ("Manager");
	}

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2 + pos.x, Screen.height/2 + pos.y, width, height), buttonImage.texture, new GUIStyle())) {
			manager.SendMessage(messageToSend);
		}
	}
}
