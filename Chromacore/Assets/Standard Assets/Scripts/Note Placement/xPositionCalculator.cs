using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class xPositionCalculator : MonoBehaviour {
	// Purpose: To calculate the exact X postion of Notes based on timestamps
	// @author: Deniz Ozkaynak

	public bool triggerXPOSCalc = false;

	// Teli's starting X position
	float teliStartXPOS = -62.20014f;
	
	// Default Test note X position (used to calculate Teli's velocity)
	//float testNoteXPOS = 19.72372f;

	// Level 4 test note x pos
	//float testNoteXPOS = 18.46568f;

	// Level 5 test note x pos
	//float testNoteXPOS = 62.75334f;

	// Level 6 test note x pos
	//float testNoteXPOS = -21.05437f;

	// Level 8 test note x pos
	float testNoteXPOS = -10.48861f;
	
	// Time to get to test note in seconds (value calculated from
	// Unity Debug.Log(Time.timeSinceLevelLoad) on collion with test note)
	// DEFAULT time to test note
	//float timeToTestNote = 17.24f;

	// Level 2 time to test note:
	//float timeToTestNote = 16.28f;

	// Level 4 time to test note:
	//float timeToTestNote = 14.66f;

	// Level 5 time to test note:
	//float timeToTestNote = 23.9f;

	// Level 6 time to test note:
	//float timeToTestNote = 8.3f;

	// Level 8 time to test note
	float timeToTestNote = 10.9f;
	
	public List<string> myTimestamps;
	
	public List<string> myXPositions;
	
	public AutomaticNotePlacer autoNotePlacer;
	
	// Calculate Teli's velocity
	// Formula: Distance / Timef
	//        : (Test Note X Pos - Teli's Starting X Pos) / Time to Note
	float calcVelocity(){
		if (Application.loadedLevelName == "LevelFour"){
			testNoteXPOS = 18.46568f;
			timeToTestNote = 14.66f;
			return ((testNoteXPOS - teliStartXPOS) / timeToTestNote);
		}else if (Application.loadedLevelName == "LevelFive"){
			testNoteXPOS = 62.75334f;
			timeToTestNote = 23.90f;
			return ((testNoteXPOS - teliStartXPOS) / timeToTestNote);
		}else if (Application.loadedLevelName == "LevelSix"){
			testNoteXPOS = -21.05437f;
			timeToTestNote = 8.3f;
			return ((testNoteXPOS - teliStartXPOS) / timeToTestNote);
		}else{
			return ((testNoteXPOS - teliStartXPOS) / timeToTestNote);
		}
	}
	
	// Open the given text file
	public void ReadFile(string filename){
	      var sr = File.OpenText(filename);
	      myTimestamps = sr.ReadToEnd().Split('\n').ToList();
	      sr.Close();
	}
	
	// Open and parse the timestamps text file, calculate the X positon where the 
	// note should go, and output the result to a new text file.
	// Formula: (Vecocity * Timestamp) + Teli's Starting X Pos
	void calcXPOS(){
		if(triggerXPOSCalc == true){
			// The x-position for this timestamp
			float xPOS;
			// float version of timestamp
			float f_timestamp;
			
			// Read the timestamp file based on the current level
			// To identify the current level, we subtract 1 from the Application level count
			if (Application.isEditor){
				//EDITME
				ReadFile("..\\Chromacore\\Assets\\Standard Assets\\Scripts\\Note Placement\\level8_timestamps.txt");
			}//else{
			//	ReadFile("..\\Chromacore\\Assets\\Standard Assets\\Scripts\\Note Placement\\level" + (Application.loadedLevel - 1).ToString() + "_timestamps.txt");
			//}

			// For each timestamp, calcualte the X positions and write 
			// the result to the output text file.
			foreach (string timestamp in myTimestamps){
				// Convert timestamp to a float
				f_timestamp = float.Parse(timestamp);
				// Calculate X position
				xPOS = (calcVelocity() * f_timestamp + teliStartXPOS);
				// Add each x-position to List of X-positions
				myXPositions.Add(xPOS.ToString());
			}
			// Send a message to automatic note placer that x-positions are ready
			autoNotePlacer.SendMessage("CalculationDone");

			triggerXPOSCalc = false;
		}
	}
	
	// Use this for initialization
	void Start () {
		calcXPOS();
	}
}
