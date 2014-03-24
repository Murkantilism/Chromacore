using UnityEngine;
using System.Collections;

public class APKExpansionDownloader : MonoBehaviour {

	bool downloadStarted = false;
	string mainPath;
	string expPath;

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID
		CheckStorage();
		CheckDownload();
		#endif
	}

	// Check if this Android Device has available SD card storage
	void CheckStorage(){
		expPath = GooglePlayDownloader.GetExpansionFilePath () as string;
		if (expPath == null) {
			Debug.LogError("Storage is not available!");
		}
	}

	// Check if the APK Expansion File(s) have already been downloaded.
	// If so, load Main Menu. If not, trigger download
	void CheckDownload(){
		string mainPath = GooglePlayDownloader.GetMainOBBPath (expPath) as string;
		string patchPath = GooglePlayDownloader.GetPatchOBBPath (expPath) as string;

		// APK's present, load Main Menu
		if (mainPath != null) {
			Application.LoadLevel("MainMenu");
		}

		// APK's missing, trigger download
		if (mainPath == null) {
			GooglePlayDownloader.FetchOBB ();
			StartCoroutine (loadLevel ());  
		}
	}

	// Load level checks every half second if download is finished
	protected IEnumerator loadLevel ()
	{
		string mainPath;
		do {
			yield return new WaitForSeconds (0.5f);
			mainPath = GooglePlayDownloader.GetMainOBBPath (expPath) as string;
		} while(mainPath == null);
		
		if (downloadStarted == false) {
			downloadStarted = true;
			
			string uri = "file://" + mainPath;
			WWW www = WWW.LoadFromCacheOrDownload (uri, 0);     
			
			yield return www;
			
			if (www.error == null) {
				Application.LoadLevel ("MainMenu");
			}
		} else {
			Application.LoadLevel ("MainMenu");  
		}
	}
}
