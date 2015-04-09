using UnityEngine;
using System.Collections;

public class PutTextInCenter : MonoBehaviour {

	public float deltaX = 0f;
	public float deltaY = 0f;

	// Use this for initialization
	void Start () {
		GetComponent<GUIText>().pixelOffset = new Vector2(Screen.width / 2 + deltaX, Screen.height / 2 + deltaY);
	}
}
