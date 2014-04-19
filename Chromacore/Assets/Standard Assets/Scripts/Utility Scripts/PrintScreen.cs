using UnityEngine;
using System.Collections;

public class PrintScreen : MonoBehaviour {

	int screenShotIndex = 0;

	public GameObject ScreenshotTaker;

	// Use this for initialization
	void Start () {
		ScreenshotTaker = GameObject.Find("ScreenshotTaker");
		DontDestroyOnLoad(ScreenshotTaker.transform);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Print) || Input.GetKeyDown(KeyCode.Pause) || Input.GetKeyDown(KeyCode.P)){
			screenShotIndex++;
			Application.CaptureScreenshot(Application.dataPath + "Screenshot" + screenShotIndex + ".png");
		}
	}
}
