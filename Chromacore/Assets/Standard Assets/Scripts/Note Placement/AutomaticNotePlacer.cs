using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class AutomaticNotePlacer : MonoBehaviour {
	// Purpose: To automatically place Note objects at the 
	// X postions calculated by xPositionCalcular.cs
	// @author: Deniz Ozkaynak
	
	// Access the X-position calculator script
	public xPositionCalculator xPosCalc;
	
	// A local list of x-positions
	List<string> myXPositions;
	
	// An array of Notes
	public GameObject[] NotesArray;
	
	// A List of Notes
	public List<GameObject> Notes;

	// Called once x-position calculations are finished
	void CalculationDone(){
		// Grab the List of X-Positions from the Calculator script
		myXPositions = xPosCalc.myXPositions;
		
		Start();
	}

	// Use this for initialization
	void Start () {
		// Grab and sort array of Notes
		NotesArray = GameObject.FindGameObjectsWithTag("Note");
		SortNotes();
		
		// Convert array to List (in order to place Notes easily)
		Notes = NotesArray.OfType<GameObject>().ToList();
		Debug.Log("After List Conversion: " + Notes.Count);
	}

	// Sort the List of Notes in numerical order
	void SortNotes(){
		// For each note in the List of Notes
		foreach (GameObject note in NotesArray){
			// Slice off the first four characters from every name of each Note gameobject
			// Ex: "Note34" becomes "34" - making numerical sorting possible
			note.name = note.name.Substring(4);
		}

		// Actually sort the notes
		Array.Sort(NotesArray, sortList);

		// Call PlaceNotes
		PlaceNotes();
	}

	int sortList(GameObject a, GameObject b){
		return int.Parse(a.name) - int.Parse(b.name);
	}

	// Assign each note in this array of Notes a corresponding x-position
	void PlaceNotes(){
		// For every Note in Notes
		for(int i = 0; i < Notes.Count; i++){
			// Convert the xPos in the List from string to float,
			// then assign the x-postion to each note in Notes,
			// leaving the y & z positions alone.
			Notes[i].transform.position = new Vector3(float.Parse(myXPositions[i]), Notes[i].transform.position.y, Notes[i].transform.position.z);
			//Debug.Log(Notes[i].transform.position.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}