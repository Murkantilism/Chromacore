using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
// Purpose: Auomatically place Notes in the correct x positions
public class AutomaticNotePlacer : MonoBehaviour {
	// Purpose: To automatically place Note objects at the 
	// X postions calculated by xPositionCalcular.cs
	// @author: Deniz Ozkaynak

	public bool triggerAutomaticNotePlacer = false;

	public bool sliceNotes = false;

	// Access the X-position calculator script
	public xPositionCalculator xPosCalc;
	
	// A local list of x-positions
	List<string> myXPositions;
	
	// An array of Notes
	public GameObject[] NotesArray;
	
	// A List of Notes
	public List<GameObject> Notes;

	// Do we need to reset the Notes (For editor purposes ONLY!)
	public bool editorResetP = false;

	public bool calcDone = false;

	// Called once x-position calculations are finished
	void CalculationDone(){
		calcDone = true;

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

		// If we are reseting, assume the list of x-positions is good
		if(calcDone == true || editorResetP == true){
			// Grab the List of X-Positions from the Calculator script
			myXPositions = xPosCalc.myXPositions;
		}

		PlaceNotes();
	}

	// Sort the List of Notes in numerical order
	void SortNotes(){
		if(triggerAutomaticNotePlacer == true){
			// If we need to reset first, add "Note" prefix
			if (editorResetP == true){
				foreach (GameObject note in NotesArray){
					try{ 
						note.name = note.name.Insert(0, "Note");
					}catch(Exception e){
						Debug.Log(e.ToString());
					}
				}
				// Once the reset is done, set it to false
				editorResetP = false;
			}

			// If we need to slice the Notes
			//if(sliceNotes == true){
			// For each note in the List of Notes
			foreach (GameObject note in NotesArray){
				// Slice off the first four characters from every name of each Note gameobject
				// Ex: "Note34" becomes "34" - making numerical sorting possible
				try{
					note.name = note.name.Substring(4);
				}catch(Exception e){
					Debug.Log(e.ToString());
				}
			}
			//	sliceNotes = false;
			//}

			// Actually sort the notes
			Array.Sort(NotesArray, sortList);

			triggerAutomaticNotePlacer = false;
		}
	}

	int sortList(GameObject a, GameObject b){
		return int.Parse(a.name) - int.Parse(b.name);
	}

	// Assign each note in this array of Notes a corresponding x-position
	void PlaceNotes(){
		RaycastHit hit;

		// Minimum and maximum threshold for y distance
		// from Note positon to level platform postion.
		float yThreshMin = 0.75f;
		float yThreshMax = 3.0f;

		// Min and max threshold for y distance from Note
		// position to level platform when Notes aren't
		// above any platforms.
		float offPlatform_yThreshMin = 5.0f;
		float offPlatform_yThreshMax = 7.0f;

		// For each Note in Notes
		foreach(GameObject note in Notes){
			// Draw a ray downwards in the y direction
			if(Physics.Raycast(note.transform.position, Vector3.down, out hit, 10)){
				Debug.Log(note.name + " : " + hit.collider.name + " : " + hit.distance);

				// If this Note's y position is below the min treshold
				if (hit.distance < yThreshMin){
					// Increment this Note's y poition to the treshold
					note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y + (yThreshMin - hit.distance), note.transform.position.z);
					Debug.Log("ATTN: Note " + note.name + "'s y position has been INcrememnted by " + note.transform.position.y + (yThreshMin - hit.distance));
				// Elif this Note's y position is above the max threshold AND below
				// the minimum off-platform threshold
				}else if(hit.distance > yThreshMax && hit.distance < offPlatform_yThreshMin){
					// Decrement this Note's y position to the Max treshold
					note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y - (yThreshMax - hit.distance), note.transform.position.z);
					Debug.Log("ATTN: Note " + note.name + "'s y position has been DEcrememnted by " + (note.transform.position.y - (yThreshMax - hit.distance)));
				// Elif this Note's y position above the max off-platform treshold
				}else if(hit.distance > offPlatform_yThreshMax){
					// Flag this Note for manual placement
					Debug.LogError("ATTN: Note " + note.name + " requires manual y-positon placement.");
				}
			}
		}

		// For every Note in Notes convert the xPos in the List from 
		// string to float, then assign the x-postion to each note 
		// in Notes, leaving the y & z positions alone.
		Debug.Log("Placing Notes");
		for(int i = 0; i < Notes.Count; i++){
			Debug.Log(myXPositions[i]);

			try{
				Notes[i].transform.position = new Vector3(float.Parse(myXPositions[i]), Notes[i].transform.position.y, Notes[i].transform.position.z);
			}catch (Exception e){
				Debug.Log(e.ToString());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}