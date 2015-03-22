using UnityEngine;
using System.Collections;

public class ScoringSystem : MonoBehaviour {
	// Properties
	float timeLapsed;
	GUIText scoreLabel;
	int score;
	bool teliIsDead;

	// Methods
	// Start method
	public void SetCharacterAlive() {
		teliIsDead = false;
	}

	public void SetCharacterDead() {
		teliIsDead = true;
	}

	public void ResetScore() {
		score = 0;
	}

	void Start () {
		timeLapsed = 0;
		score = 0;

		scoreLabel = GetComponent<GUIText>();
		scoreLabel.pixelOffset = new Vector2(-(Screen.width/2) + 100, (Screen.height/2) - 50);
	
		SetCharacterAlive ();
	}
	
	// Update is called once per frame
	void UpdateScore() {
		score++;
		scoreLabel.text = "Score: " + score.ToString();
	}
}
