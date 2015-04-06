using UnityEngine;
using System.Collections;

public class SceneManagement : MonoBehaviour {

	public int generationRow;
	public GameObject[] structsPrefabs;
	public GameObject background1;
	public GameObject background2;
	
	GameObject mainCamera;

	Vector2[,] dependencies; // A 2d array listing dependencies between structs - dependencies[x][y] = the difference in position if y comes after x
	Vector2[] strP; // The generated positions
	StructType[] str; // The type of struct generated
	GameObject[] generatedStructs;
	float rightmostPositionX;
	float rightmostBackgroundPositionX;
	int freeMem;

	enum StructType{
		str1 = 1,
		str2 = 2,
		str3 = 3,
		str4 = 4,
		str5 = 5,
		str6 = 6,
		str7 = 7,
		str8 = 8
	};

	int RandomIntLowerThan(int x) {
		int rand = (int)Random.Range (0, x);
		return rand;
	}

	void SetDependencies () {
		Debug.Log ("Setting dependencies..."); for (int i=1; i<=8; i++) for (int j=1; j<=8; j++) dependencies [i, j] = new Vector2 (75, 0);

		// For structs coming after 1
		dependencies [1, 1] = new Vector2 (76.7f, 1.01f);
		dependencies [1, 2] = new Vector2 (55.19f, -1.46f);
		dependencies [1, 3] = new Vector2 (55.42f, 2f);
		dependencies [1, 4] = new Vector2 (72f, 1.27f);
		dependencies [1, 5] = new Vector2 (56.42f, -1f);
		dependencies [1, 6] = new Vector2 (56.85f, -1.51f);
		dependencies [1, 7] = new Vector2 (63.02f, -1.33f);
		dependencies [1, 8] = new Vector2 (90.6f, 1.35f);

		// For structs coming after 2
		dependencies [2, 1] = new Vector2 (54.51f, 1.15f);
		dependencies [2, 2] = new Vector2 (33.98f, -1.04f);
		dependencies [2, 3] = new Vector2 (33.13f, 1.87f);
		dependencies [2, 4] = new Vector2 (51.31f, 3.77f);
		dependencies [2, 5] = new Vector2 (34.26f, -0.04f);
		dependencies [2, 6] = new Vector2 (35.59f, -0.4f);
		dependencies [2, 7] = new Vector2 (41.5f, -0.51f);
		dependencies [2, 8] = new Vector2 (68.83f, 3.39f);

		// For structs coming after 3
		dependencies [3, 1] = new Vector2 (45.24f, 2.82f);
		dependencies [3, 2] = new Vector2 (25.72f, 0f);
		dependencies [3, 3] = new Vector2 (25.99f, 2.55f);
		dependencies [3, 4] = new Vector2 (43.16f, 3.34f);
		dependencies [3, 5] = new Vector2 (25.81f, 0.82f);
		dependencies [3, 6] = new Vector2 (25.42f, 0.53f);
		dependencies [3, 7] = new Vector2 (32.83f, 0f);
		dependencies [3, 8] = new Vector2 (61.04f, 3.22f);

		// For structs coming after 4
		dependencies [4, 1] = new Vector2 (73.31f, 0.16f);
		dependencies [4, 2] = new Vector2 (52.11f, -0.89f);
		dependencies [4, 3] = new Vector2 (54.1f, 0f);
		dependencies [4, 4] = new Vector2 (70.15f, 1.13f);
		dependencies [4, 5] = new Vector2 (52.17f, 0f);
		dependencies [4, 6] = new Vector2 (51.82f, -0.42f);
		dependencies [4, 7] = new Vector2 (58.92f, -0.68f);
		dependencies [4, 8] = new Vector2 (87.02f, 1.78f);

		// For structs coming after 5
		dependencies [5, 1] = new Vector2 (53.6f, 0f);
		dependencies [5, 2] = new Vector2 (31.82f, 0f);
		dependencies [5, 3] = new Vector2 (33.11f, 2.15f);
		dependencies [5, 4] = new Vector2 (49.31f, 2.74f);
		dependencies [5, 5] = new Vector2 (32.55f, -0.85f);
		dependencies [5, 6] = new Vector2 (32.3f, 0.6f);
		dependencies [5, 7] = new Vector2 (39.32f, 0.42f);
		dependencies [5, 8] = new Vector2 (67.72f, 2.57f);

		// For structs coming after 6
		dependencies [6, 1] = new Vector2 (57.75f, 2.32f);
		dependencies [6, 2] = new Vector2 (37.23f, 0f);
		dependencies [6, 3] = new Vector2 (38.21f, 3.23f);
		dependencies [6, 4] = new Vector2 (53.7f, 3f);
		dependencies [6, 5] = new Vector2 (36.9f, 2.5f);
		dependencies [6, 6] = new Vector2 (36.22f, 1.87f);
		dependencies [6, 7] = new Vector2 (43.03f, 1.97f);
		dependencies [6, 8] = new Vector2 (70.95f, 4.97f);

		// For structs coming after 7
		dependencies [7, 1] = new Vector2 (63.58f, 1.3f);
		dependencies [7, 2] = new Vector2 (42.38f, 0.98f);
		dependencies [7, 3] = new Vector2 (43.48f, 3.88f);
		dependencies [7, 4] = new Vector2 (59.51f, 4.32f);
		dependencies [7, 5] = new Vector2 (43.1f, 1.71f);
		dependencies [7, 6] = new Vector2 (42.38f, 0.96f);
		dependencies [7, 7] = new Vector2 (50.15f, -0.46f);
		dependencies [7, 8] = new Vector2 (78.3f, 4f);

		// For structs coming after 8
		dependencies [8, 1] = new Vector2 (90.1f, -1.43f);
		dependencies [8, 2] = new Vector2 (70.72f, -3.59f);
		dependencies [8, 3] = new Vector2 (70.26f, -1.02f);
		dependencies [8, 4] = new Vector2 (86.79f, 0f);
		dependencies [8, 5] = new Vector2 (70.04f, -3.05f);
		dependencies [8, 6] = new Vector2 (69.47f, -3.4f);
		dependencies [8, 7] = new Vector2 (77.14f, -3.6f);
		dependencies [8, 8] = new Vector2 (105.17f, 0f);
	}

	void GenerateStructure() {
		Debug.Log ("Generating a structure on the position " + freeMem.ToString() + "...");

		// Generating a structure on free mem
		int structureIndex = RandomIntLowerThan (structsPrefabs.Length);
		int last = freeMem - 1;
		if (last == 0)
			last = generationRow;

		if (generatedStructs[freeMem] != null)
			Destroy(generatedStructs[freeMem]);
		generatedStructs[freeMem] = (GameObject)Instantiate (structsPrefabs [structureIndex], 
		                                                     new Vector2 (strP [last].x + dependencies [(int)str[last], structureIndex + 1].x, 
		             													  strP [last].y + dependencies [(int)str[last], structureIndex + 1].y), 
		                                                     Quaternion.identity); 

		str [freeMem] = (StructType)(structureIndex + 1);
		strP [freeMem] = new Vector2(generatedStructs[freeMem].transform.position.x, generatedStructs[freeMem].transform.position.y);

		rightmostPositionX = generatedStructs[freeMem].transform.position.x;

		freeMem++;
		if (freeMem > generationRow)
			freeMem = 1;
	}

	void GenerateInitial () {
		Debug.Log ("Generating initial structures...");
		str [1] = StructType.str2;
		strP [1] = new Vector2 (0, 0);
		rightmostPositionX = 0;

		for (int i=1; i<=2; i++)
			GenerateStructure();
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");

		rightmostBackgroundPositionX = 78.9f;
		generationRow = 3;
		freeMem = 2;

		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

		dependencies = new Vector2[10, 10];
		strP = new Vector2[generationRow + 2];
		str = new StructType[generationRow + 2];
		generatedStructs = new GameObject[generationRow + 2];

		SetDependencies ();
		GenerateInitial ();
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.transform.position.x >= rightmostPositionX) {
			GenerateStructure();
		}
		if (mainCamera.transform.position.x >= rightmostBackgroundPositionX) {
			if (background1.transform.position.x < background2.transform.position.x) {
				// Should move background 1
				background1.transform.position = new Vector2(background2.transform.position.x + 78.9f,
				                                             background1.transform.position.y);
				rightmostBackgroundPositionX = background1.transform.position.x;
			} else {
				// Should move background 2
				background2.transform.position = new Vector2(background1.transform.position.x + 78.9f,
				                                             background2.transform.position.y);
				rightmostBackgroundPositionX = background2.transform.position.x;
			}
		}
	}
}
