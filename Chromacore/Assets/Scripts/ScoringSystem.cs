using UnityEngine;
using System.Collections;

public class ScoringSystem : MonoBehaviour {

	// Properties
	GameObject scoreBoard;

	GUIText scoreLabel;

	private int score;
	private const string highScoreKey = "HighScore";

	// Methods
	void ShowOnScreen() {
		string labelToSet = "Score: " + score.ToString() + "\n\n" + "High Score: " + PlayerPrefs.GetInt (highScoreKey).ToString ();
		scoreBoard.SendMessage ("InitGUI", labelToSet);
	}

	public void RegisterScore () {
		int currentHighScore = PlayerPrefs.GetInt (highScoreKey);

		// Updating the high score
		if (score > currentHighScore) {
			PlayerPrefs.SetInt (highScoreKey, score);
			PlayerPrefs.Save();
		}
	
		// Do what you want with the updated high score and the current score
		// ...

		Invoke ("ShowOnScreen", 1f);
	}
	
	public void ResetScore() {
		score = 0;
	}

	void Start () {
		score = 0;

		scoreBoard = GameObject.FindGameObjectWithTag ("ScoreBoard");

		scoreLabel = GetComponent<GUIText>();
		scoreLabel.pixelOffset = new Vector2(-(Screen.width/2) + 25, (Screen.height/2) - 50);
	}
	
	// Update is called once per frame
	void UpdateScore() {
		score++;
		scoreLabel.text = "Score: " + score.ToString();
	}
}
