using UnityEngine;
using System;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	
	// Loading screen animation variables
	public GUIText loadingText;
	public tk2dSprite animSeqOne;
	public tk2dSprite animSeqTwo;
	public tk2dSprite animSeqThree;
	
	int animOneID;
	int animTwoID;
	int animThreeID;
	
	public tk2dSpriteCollectionData TeliRun;
	
	bool loadSeqOne;
	bool loadSeqTwo = false;
	bool loadSeqThree = false;
	
	float fadeAlphaOne;
	float fadeAlphaTwo;
	
	// Used to invoke startAnimation only once
	bool invokeMeP = true;
	
	// Loading screen music loading variables
	bool pcP = false;
	bool iphoneP = false;
	bool androidP = false;
	
	// The WWW data
	private WWW wwwData;
	// The instance of this class used to download
	private static LoadingScreen downloadManager = null;
	
	// Use this for initialization
	void Start () {
		
		// Initialization of download manager
		if(LoadingScreen.downloadManager == null){
			LoadingScreen.downloadManager = FindObjectOfType(typeof(LoadingScreen)) as LoadingScreen;
		}
		
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
	  		Debug.Log("Android");
	  		androidP = true;
	 	#endif
	}
	
	void Update(){
		// If we are actually in the LoadingScreen scene, play animation
		if(Application.loadedLevelName == "LoadingScreen"){
			if(invokeMeP){
				Invoke("startAnimation", 0);
				invokeMeP = false;
			}
		}
			
	}
	
	/****************************************************************
	 * 		            		Animation Code						*
	 ****************************************************************/
	
	// Kick-off the animation
	void startAnimation(){
		InvokeRepeating("loadAnimation", 0, 2);
		loadSeqOne = true;
		loadingText = GameObject.Find("LoadingText").guiText;
		
		//animOneID = animSeqOne.GetSpriteIdByName("2run_plain__000_002");
		//animTwoID = animSeqTwo.GetSpriteIdByName("2run_plain__000_004");
		//animThreeID = animSeqThree.GetSpriteIdByName("2run_plain__000_006");
		
		//animSeqOne.SetSprite(TeliRun, animOneID);
		//animSeqTwo.SetSprite(TeliRun, animTwoID);
		//animSeqThree.SetSprite(TeliRun, animThreeID);
	}
	
	// Play the loading animation
	void loadAnimation(){
		// Play the first loading sequence
		if(loadSeqOne){
			loadingText.text = "Loading.";
			
			animSeqOne.renderer.enabled = true;
			animSeqTwo.renderer.enabled = false;
			animSeqThree.renderer.enabled = false;
			
			loadSeqOne = false;
			loadSeqTwo = true;
		
		// Switch to second loading sequence
		}else if(loadSeqTwo){
			loadingText.text = "Loading..";

			animSeqOne.renderer.enabled = false;
			animSeqTwo.renderer.enabled = true;
			animSeqThree.renderer.enabled = false;
			
			loadSeqTwo = false;
			loadSeqThree = true;
		
		// Switch to last loading sequence
		}else if(loadSeqThree){
			loadingText.text = "Loading...";
			
			animSeqOne.renderer.enabled = false;
			animSeqTwo.renderer.enabled = false;
			animSeqThree.renderer.enabled = true;
			
			loadSeqThree = false;
			loadSeqOne = true;
		// Lastly, switch back to sequence one
		}
	}
	
	
	/****************************************************************
	 * 				      Music Loading Code for PC					*
	 ****************************************************************/
	
	// Recieve the file path from UniFileBrowserExample.cs
	// and format it to work with WWW download
	public void RecieveFilePath(string myFilePath){
		Debug.Log(myFilePath);
		
		DontDestroyUs();
		
		// Load the level
		Load(myFilePath);
	}
	
	// Preserve the needed game objects
	void DontDestroyUs(){
		GameObject loadingscreen = GameObject.Find("LoadingScreen");
		DontDestroyOnLoad(loadingscreen);
		
		GameObject runSeq1 = GameObject.Find("Run Seq 1");
		DontDestroyOnLoad(runSeq1);
		
		GameObject runSeq2 = GameObject.Find("Run Seq 2");
		DontDestroyOnLoad(runSeq2);
		
		GameObject runSeq3 = GameObject.Find("Run Seq 3");
		DontDestroyOnLoad(runSeq3);
	}
	
	// Take care of all the heavy computations before the level
	// Save results into data structures with DontDestroyOnLoad
	void Load(string myFilePath){
		// Invoke the Download function to begin OGG download, wait, and play functions
		if(pcP){
			try{
				LoadingScreen.Download("file: //C:/Burn.ogg", myDownloadCallback);
			}catch(Exception e){
				Debug.Log(e.ToString());
			}
		}else if(androidP){
			try{
				downloadManager.Download_Android("file: //C:/Burn.ogg");
			}catch(Exception e){
				Debug.Log(e.ToString());
			}
		}
	}
	
	// Start download of the OGG, catch any errors along the way
	public static void myDownloadCallback(byte[]data, string sError){
		try{
			if(sError != null){
				Debug.Log(sError);
			}else{
				downloadManager.StartCoroutine(downloadManager.PlayOGG());
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
	public IEnumerator PlayOGG(){
		// Put OGG into audio clip
		audio.clip = wwwData.GetAudioClip(true);
		
		// If the audio isn't already playing & the clip is ready, load
		// the auto generated CustomLevel scene
		if(!audio.isPlaying && audio.clip.isReadyToPlay){
			// Wait at least six seconds before loading
			yield return new WaitForSeconds(6);
			// Load when ready
			Application.LoadLevel("CustomLevel");
			// Start music
			audio.Play();
		}
	}
	
	// Once Application is closed, delete download manager
	void OnApplicationQuit(){
		LoadingScreen.downloadManager = null;
	}
	
	
	/****************************************************************
	 * 				      Music Loading Code for Android			*
	 ****************************************************************/
	IEnumerator Download_Android(string myFilePath){
		// Start downloading at the given path
		WWW wwwDroid = new WWW (myFilePath);
		
		// Wait for download to finish
		yield return wwwDroid;
		
		audio.clip = wwwDroid.audioClip;
		
		while(!audio.isPlaying && audio.clip.isReadyToPlay){
			audio.Play();
		}
	}
	
	
	
	
	
	
	
	
	
	/****************************************************************
	 * 				      Music Loading Code for iPhone				*
	 ****************************************************************/
	
	
	
	
	
	
	
	
}
