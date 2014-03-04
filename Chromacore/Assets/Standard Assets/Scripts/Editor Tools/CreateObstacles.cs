using UnityEngine;
using System.Collections;

// Given a number of obstacles and a starting point, automatically instantiate obstacles
[ExecuteInEditMode]
public class CreateObstacles : MonoBehaviour {
	// The number of platforms to create
	public int numObstacles = 0;
	
	// Start the counter at this number (used if some platforms already exist)
	public int startCount = 0;
	
	public GameObject obstacle;
	
	public GameObject parentObstacle;
	
	public bool instantiationDoneP = false;
	
	// Start the instantiation at this Vector
	public Vector3 startVector = new Vector3(0f, 8.5f, -11f);
	
	// Use this for initialization
	void Start () {
		#if UNITY_EDITOR
		if (instantiationDoneP == false){
			for (int i = 0; i < numObstacles; i++){
				GameObject temp = Instantiate(obstacle, new Vector3(startVector.x + (i * 10), startVector.y, startVector.z), Quaternion.identity) as GameObject;
				temp.name = "Obstacle" + (i + startCount);
				temp.transform.parent = parentObstacle.transform;
			}
			instantiationDoneP = true;
		}
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
