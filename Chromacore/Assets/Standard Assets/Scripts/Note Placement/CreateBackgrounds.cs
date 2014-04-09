using UnityEngine;
using System.Collections;

// Given a number of backgrounds and a starting point, automatically instantiate backgrounds
[ExecuteInEditMode]
public class CreateBackgrounds : MonoBehaviour {
	// The number of platforms to create
	public int numBackgrounds = 0;
	
	// Start the counter at this number (used if some platforms already exist)
	public int startCount = 0;
	
	public GameObject background_bw;
	public GameObject background_color;
	
	public GameObject parentBackground;
	
	public bool instantiationDoneP = false;
	
	// Start the instantiation at this Vector
	public Vector3 startVector = new Vector3(898.6304f, 10.34767f, -9.374798f);
	
	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		if (instantiationDoneP == false){
			for (int i = 0; i < numBackgrounds; i++){
				GameObject temp = Instantiate(background_bw, new Vector3(startVector.x + (i * 31.8278f), startVector.y, startVector.z), Quaternion.identity) as GameObject;
				temp.name = "below_Background" + (i + startCount);
				temp.transform.parent = parentBackground.transform;
			}
		}
		if (instantiationDoneP == false){
			for (int i = 0; i < numBackgrounds; i++){
				GameObject temp = Instantiate(background_color, new Vector3(startVector.x + (i * 31.8278f), startVector.y, startVector.z), Quaternion.identity) as GameObject;
				temp.name = "below_Background" + (i + startCount) + "_color";
				temp.transform.parent = parentBackground.transform;
			}
			instantiationDoneP = true;
		}

		#endif
	}
	// Update is called once per frame
	void Update () {
	
	}
}
