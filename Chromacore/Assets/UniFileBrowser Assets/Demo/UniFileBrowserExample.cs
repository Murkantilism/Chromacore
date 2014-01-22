// Example of open/save usage with UniFileBrowser
// This script is free to use in any manner

using UnityEngine;
using System;
using System.Collections;

public class UniFileBrowserExample : MonoBehaviour {
	
	string message = "";
	float alpha = 1.0f;
	char pathChar = '/';
	
	/*
	bool pcP = false;
	bool iphoneP = false;
	bool androidP = false;
	
	// The WWW data
	private WWW wwwData;
	// The instance of this class used to download
	private static UniFileBrowserExample downloadManager = null;
	*/
	
	void Start () {
		/*
		// Initialization of download manager
		if(UniFileBrowserExample.downloadManager == null){
			UniFileBrowserExample.downloadManager = FindObjectOfType(typeof(UniFileBrowserExample)) as UniFileBrowserExample;
		}*/
		
		
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			pathChar = '\\';
		}
		
		/*
		// Determine Platform at compile time
	  	#if UNITY_STANDALONE
	   	 	Debug.Log("Standalone PC (Win/Mac/Lin)");
	    	pcP = true;
	  	#endif
	  
	  	#if UNITY_IPHONE
	    	Debug.Log("iPhone");
	    	iphoneP = true;
	  	#endif
	  
	  	#if UNITY_ANDROID
	  		Debug.Log("Android")
	  		androidP = true;
	 	#endif
	 	*/
	}
	
	void OnGUI () {
		// Only load the File Browser in the Song Browser scene
		if (Application.loadedLevelName != "SongBrowser"){
			return;
		}else{
			// Only load the File Browser in the Song Browser scene
			if (GUI.Button (new Rect(Screen.width/2, Screen.height/2, 95, 35), "Browse")) {
				if (UniFileBrowser.use.allowMultiSelect) {
					UniFileBrowser.use.OpenFileWindow (OpenFiles);
				}
				else {
					UniFileBrowser.use.OpenFileWindow (OpenFile);
				}
			}
			
			if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 95, 35), "Back")){
					Application.LoadLevel("MainMenu");
			}
			
			/*
			if (GUI.Button (new Rect(100, 125, 95, 35), "Save")) {
				UniFileBrowser.use.SaveFileWindow (SaveFile);
			}
			if (GUI.Button (new Rect(100, 200, 95, 35), "Open Folder")) {
				UniFileBrowser.use.OpenFolderWindow (true, OpenFolder);
			}
			*/
			
			var col = GUI.color;
			col.a = alpha;
			GUI.color = col;
			GUI.Label (new Rect(100, 275, 500, 1000), message);
			col.a = 1.0f;
			GUI.color = col;
		}
	}
	
	void OpenFile (string pathToFile) {
		var fileIndex = pathToFile.LastIndexOf (pathChar);
		message = "You selected file: " + pathToFile.Substring (fileIndex+1, pathToFile.Length-fileIndex-1);
		
		// Pass file path to Loading Screen, then load loading screen
		LoadingScreen loadScreen = GetComponent<LoadingScreen>();
		loadScreen.RecieveFilePath(pathToFile.ToString());

		if (loadScreen.shouldLoadP == true){
			Application.LoadLevel("LoadingScreen");
		}
		
		/*
		// Invoke the Download function to begin OGG download, wait, and play functions
		if(pcP){
			try{
				UniFileBrowserExample.Download("file: //C:/Burn.ogg", myDownloadCallback);
			}catch(Exception e){
				Debug.Log(e.ToString());
			}
		}*/
		
		Fade();
	}
	
	void OpenFiles (string[] pathsToFiles) {
		message = "You selected these files:\n";
		for (var i = 0; i < pathsToFiles.Length; i++) {
			var fileIndex = pathsToFiles[i].LastIndexOf (pathChar);
			message += pathsToFiles[i].Substring (fileIndex+1, pathsToFiles[i].Length-fileIndex-1) + "\n";
		}
		Fade();
	}

	/*
	void SaveFile (string pathToFile) {
		var fileIndex = pathToFile.LastIndexOf (pathChar);
		message = "You're saving file: " + pathToFile.Substring (fileIndex+1, pathToFile.Length-fileIndex-1);
		Fade();
	}
	
	void OpenFolder (string pathToFolder) {
		message = "You selected folder: " + pathToFolder;
		Fade();
	}
	*/
	
	void Fade () {
		StopCoroutine ("FadeAlpha");	// Interrupt FadeAlpha if it's already running, so only one instance at a time can run
		StartCoroutine ("FadeAlpha");
	}
	
	IEnumerator FadeAlpha () {
		alpha = 1.0f;
		yield return new WaitForSeconds (5.0f);
		for (alpha = 1.0f; alpha > 0.0f; alpha -= Time.deltaTime/4) {
			 yield return null;
		}
		message = "";
	}
	
	/*
	// Start download of the OGG, catch any errors along the way
	public static void myDownloadCallback(byte[]data, string sError){
		try{
			if(sError != null){
				Debug.Log(sError);
			}else{
				downloadManager.PlayOGG();
			}
		}catch(Exception e){
			Debug.Log(e.ToString());
		}
	}
	
	// A delete, not to be confused with myDownloadCallback above
	public delegate void DownloadCallback(byte[] data, string sError);
	
	// A coroutine used to wait for the download to finish before proceeding
	private IEnumerator WaitForDownload(DownloadCallback fn){
		Debug.Log("Yielding...");
		yield return wwwData;
		Debug.Log("Yielded...DONE");
		fn(wwwData.bytes, wwwData.error);
	}
	
	// Start download of OGG via WWW and the given file path
	private void StartDownload(string filePath, DownloadCallback fn){
		try{
			wwwData = new WWW(filePath);
			Debug.Log("Starting download...");
			StartCoroutine("WaitForDownload", fn);
		}catch(Exception e){
			Debug.Log(e.ToString());
		}
	}
	
	public static void Download(string filePath, DownloadCallback fn){
		downloadManager.StartDownload(filePath, fn);
	}
	
	// Grab the downloaded OGG file, put it into an audio clip, and play
	public void PlayOGG(){
		// Put OGG into audio clip
		audio.clip = wwwData.GetAudioClip(true);
		
		// If the audio isn't already playing & the clip is ready, play
		if(!audio.isPlaying && audio.clip.isReadyToPlay){
			audio.Play();
		}
	}
	
	// Once Application is closed, delete download manager
	void OnApplicationQuit(){
		UniFileBrowserExample.downloadManager = null;
	}*/
}