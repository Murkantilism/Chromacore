using UnityEngine;
using System.Collections;

// Given a number of platforms and a starting point, automatically instantiate platforms
[ExecuteInEditMode]
public class CreatePlatforms : MonoBehaviour {
	// The number of platforms to create
	public int numPlatforms = 0;

	// Start the counter at this number (used if some platforms already exist)
	public int startCount = 0;
	
	public GameObject platform;
	
	public GameObject parentPlatform;
	
	public bool instantiationDoneP = false;

	// Start the instantiation at this Vector
	public Vector3 startVector = new Vector3(1175f, 8.483f, -11f);
	
	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		if (instantiationDoneP == false){
			for (int i = 0; i < numPlatforms; i++){
				GameObject temp = Instantiate(platform, new Vector3(startVector.x + (i * 20), startVector.y, startVector.z), Quaternion.identity) as GameObject;
				temp.name = "Platform" + (i + startCount);
				temp.transform.parent = parentPlatform.transform;
			}
			instantiationDoneP = true;
		}
		#endif
	}
	// Update is called once per frame
	void Update () {
	
	}
}
