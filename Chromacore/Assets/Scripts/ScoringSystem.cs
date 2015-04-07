using UnityEngine;
using System.Collections;

public class ScoringSystem : MonoBehaviour {

	// Properties
	GUIText scoreLabel;

	private int score;
	private const string highScoreKey = "HighScore";

	// Methods
	public void RegisterScore () {
		int currentHighScore = PlayerPrefs.GetInt (highScoreKey);

		// Updating the high score
		if (score > currentHighScore) {
			PlayerPrefs.SetInt (highScoreKey, score);
			PlayerPrefs.Save();
		}
	
		// Do what you want with the updated high score and the current score
		// ...
		scoreLabel.text = "High Score: " + PlayerPrefs.GetInt (highScoreKey);
	}
	
	public void ResetScore() {
		score = 0;
	}

	void Start () {
		score = 0;

		scoreLabel = GetComponent<GUIText>();
		scoreLabel.pixelOffset = new Vector2(-(Screen.width/2) + 100, (Screen.height/2) - 50);
	}
	
	// Update is called once per frame
	void UpdateScore() {
		score++;
		scoreLabel.text = "Score: " + score.ToString();
	}
}
