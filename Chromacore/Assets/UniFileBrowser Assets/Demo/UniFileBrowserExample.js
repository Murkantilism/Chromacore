// Example of open/save usage with UniFileBrowser
// This script is free to use in any manner

private var message = "";
private var alpha = 1.0;
private var pathChar = "/"[0];

private var myFilePath;

private var pcP = false;
private var iphoneP = false;
private var androidP = false;

// Determine Platform at compile time
/*
function Awake(){
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
}*/

function Start () {
	if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
		pathChar = "\\"[0];
	}
}

function OnGUI () {
	// Only load the File Browser in the Song Browser scene
	if (Application.loadedLevelName != "SongBrowser"){
		return;
	}else{

		if (GUI.Button (Rect(Screen.width/2, Screen.height/2, 95, 35), "Browse")) {
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
		if (GUI.Button (Rect(100, 125, 95, 35), "Save")) {
			UniFileBrowser.use.SaveFileWindow (SaveFile);
		}
		if (GUI.Button (Rect(100, 200, 95, 35), "Open Folder")) {
			UniFileBrowser.use.OpenFolderWindow (true, OpenFolder);
		}
		*/
		
		GUI.color.a = alpha;
		GUI.Label (Rect(100, 275, 500, 1000), message);
		GUI.color.a = 1.0;
	}
}

function OpenFile (pathToFile : String) {
	message = "You selected file: " + pathToFile.Substring (pathToFile.LastIndexOf (pathChar) + 1);
	// Save file path
	myFilePath = pathToFile;
	myFilePath = myFilePath.ToString();
	//myFilePath = pathToFile.Substring (pathToFile.LastIndexOf (pathChar) + 1);
	openMP3();
	Fade();
}


function OpenFiles (pathsToFiles : String[]) {
	message = "You selected these files:\n";
	for (var i = 0; i < pathsToFiles.Length; i++) {
		message += pathsToFiles[i].Substring (pathsToFiles[i].LastIndexOf (pathChar) + 1) + "\n";
	}
	Fade();
}

/*
function SaveFile (pathToFile : String) {
	message = "You're saving file: " + pathToFile.Substring (pathToFile.LastIndexOf (pathChar) + 1);
	Fade();
}

function OpenFolder (pathToFolder : String) {
	message = "You selected folder: " + pathToFolder;
	Fade();
}
*/

function Fade () {
	StopCoroutine ("FadeAlpha");	// Interrupt FadeAlpha if it's already running, so only one instance at a time can run
	StartCoroutine ("FadeAlpha");
}

function FadeAlpha () {
	alpha = 1.0;
	yield WaitForSeconds (5.0);
	for (alpha = 1.0; alpha > 0.0; alpha -= Time.deltaTime/4) {
		 yield;
	}
	message = "";
}

function openMP3 () {
	Debug.Log("file://"+myFilePath);
	
	// If were running on standalone PC, use OGG to get audio clip
	if (pcP){
		// GetAudioClip implementation (newer):
		
		// Start downloading
		//var wwwPC = new WWW ("file://" + myFilePath);
		var wwwPC = new WWW ("file://C:/Burn.ogg");
		
		// Wait for download to finish
    	while( !wwwPC.isDone )
       		yield wwwPC;
		
		Debug.Log(wwwPC.isDone);
		Debug.Log(audio.clip.name);
		Debug.Log(audio.clip.length);
		
		// Grab the audio clip
		if (wwwPC.isDone){
			audio.clip = wwwPC.GetAudioClip(true, true);
		
			var startTimer = Time.deltaTime; // Start a load timer
			
			// Play clip
			while(!audio.isPlaying && audio.clip.isReadyToPlay){
				audio.Play();
			}
		
			// Output timer
			if (audio.isPlaying){
				stopTimer = Time.deltaTime;
				var loadTimer = stopTimer - startTimer;
				Debug.Log(loadTimer);
			}
		}
	
		/* OGG Streaming solution (depricated):
		
		// Start downloading
		var downloadPC = new WWW (myFilePath);
		
		// Wait for download to finish
		while (!downloadPC.isDone){
			yield return null;
		}
		
		// Create ogg vorbis file
		var clipPC : AudioClip = downloadPC.oggVorbis;
		
		// Play clip
		if (clipPC != null){
			audio.clip = clipPC;
			audio.Play();
		}
		*/
		
	}
	
	// If were are running on iPhone, 
	if (iphoneP){
		// Start downloading at the given path
		var wwwiOS = new WWW (myFilePath);
		// Wait for download to finish
		yield wwwiOS;
		
		audio.clip = wwwiOS.audioClip;
		
		while(!audio.isPlaying && audio.clip.isReadyToPlay){
			audio.Play();
		}
	}
	
	// If were are running on Android, use WWW to get audio clip
	if (androidP){
		// Start downloading at the given path
		var wwwDroid = new WWW (myFilePath);
		// Wait for download to finish
		yield wwwDroid;
		
		audio.clip = wwwDroid.audioClip;
		
		while(!audio.isPlaying && audio.clip.isReadyToPlay){
			audio.Play();
		}
	}
}