using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
// An automation tool used to assign pick-up mp3s to their respective Notes
public class NoteAssigner : MonoBehaviour {

	public bool triggerNoteAssigner = false;

	// An array of Notes
	public GameObject[] NotesArray;
	
	// A List of Notes
	public List<GameObject> Notes;

	// Do we need to reset the Notes (For editor purposes ONLY!)
	public bool editorResetP = false;

	public AudioClip[] pickupMP3s;

	// Use this for initialization
	void Start () {
		// Grab and sort array of Notes
		NotesArray = GameObject.FindGameObjectsWithTag("Note");
		SortNotes();
		
		// Convert array to List (in order to place Notes easily)
		Notes = NotesArray.OfType<GameObject>().ToList();
		Debug.Log("After List Conversion: " + Notes.Count);

		// Grab a list of pick-up mp3 files
		//EDITME
		pickupMP3s = Resources.LoadAll<AudioClip>("Level20/Level20_pickupTracks");
	}

	// Sort the List of Notes in numerical order
	void SortNotes(){
		if(triggerNoteAssigner == true){
			// If we need to reset first, add "Note " prefix
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
			
			// Actually sort the notes
			Array.Sort(NotesArray, sortList);
			
			// Call PlaceNotes
			AssignNotes();

			triggerNoteAssigner = false;
		}
	}

	int sortList(GameObject a, GameObject b){
		return int.Parse(a.name) - int.Parse(b.name);
	}

	// Assign each mp3 file to a Note
	void AssignNotes(){
		for (int i = 0; i < NotesArray.Length; i++){
			foreach (GameObject note in NotesArray){
				try{
					NotesArray[i].audio.clip = pickupMP3s[i];
					//Debug.Log(pickupMP3s[i]);
					//Debug.Log(note.audio.clip.name);
				}catch(Exception e){
					Debug.Log(e.ToString());
				}
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
