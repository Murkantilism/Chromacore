using UnityEngine;
using System.Collections;

public class BoxesGenerator : MonoBehaviour {

	public GameObject box;
	public Vector2[] points;

	int RandomIntLowerThan(int x) {
		int rand = (int)Random.Range (0, x);
		return rand;
	}

	void GenerateBoxes() {
		int randomNumber = RandomIntLowerThan (21);
		if (randomNumber % 5 != 0) {
			// Should generate a box on this structure
			int index = RandomIntLowerThan(points.Length);
			(Instantiate (box, new Vector3(gameObject.transform.position.x + points[index].x, gameObject.transform.position.y + points[index].y, 0f), Quaternion.identity) as GameObject).transform.parent = gameObject.transform;
		}
	}

	// Use this for initialization
	void Start () {
		GenerateBoxes ();
	}
}
