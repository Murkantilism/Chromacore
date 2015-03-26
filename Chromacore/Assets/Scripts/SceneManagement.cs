using UnityEngine;
using System.Collections;

public class SceneManagement : MonoBehaviour {

	public Transform struct1;
	public Transform struct2;
	public Transform struct3;
	public Transform struct4;
	public Transform struct5;
	public Transform struct6;
	public Transform struct7;

	// Use this for initialization
	void Start () {
		for (int i=1; i<=3; i++) {
			Transform test = (Transform)(Instantiate (struct1, new Vector3 ((float)(75 * (i - 1)), 0f, 0f), Quaternion.identity));
			Destroy (test.gameObject, (float)(10*i));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
