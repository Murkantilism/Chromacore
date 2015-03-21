using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	// Notes that have been collected by the player
	private int misticBallsCollected = 0;

	public GameObject scoringSystem;
	
	// Variables used to save score, zero by default
	private int save_notesCollected = 0;
	private int save_notesSeen = 0;
	
	// Use this for initialization
	void Start () {

		GameObject teli = GameObject.FindGameObjectWithTag ("Teli");
	}

	// Increment notes when the player collects them
	void CollecMisticBall()
	{
		misticBallsCollected++;
		//scoringSystem.SendMessage("ScoreCollected", misticBallsCollected.ToString());
	}

	// Update is called once per frame
	void Update () {
		
	}
}
