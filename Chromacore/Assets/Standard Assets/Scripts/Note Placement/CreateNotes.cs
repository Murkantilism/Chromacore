using UnityEngine;
using System.Collections;

// Given a number of Notes, automatically instantiate Notes
[ExecuteInEditMode]
public class CreateNotes : MonoBehaviour {
	public int numNotes = 0;

	public GameObject note;

	public GameObject parentNote;

	public bool instantiationDoneP = false;

	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		if (instantiationDoneP == false){
			for (int i = 0; i < numNotes; i++){
				GameObject temp = Instantiate(note, new Vector3(0, 5, -10), Quaternion.identity) as GameObject;
				temp.name = "Note" + i;
				temp.transform.parent = parentNote.transform;
			}
			instantiationDoneP = true;
		}
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
