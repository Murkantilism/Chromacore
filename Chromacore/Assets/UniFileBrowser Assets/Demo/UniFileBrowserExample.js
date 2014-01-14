// Example of open/save usage with UniFileBrowser
// This script is free to use in any manner

private var message = "";
private var alpha = 1.0;
private var pathChar = "/"[0];

private var myFilePath;

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
	myFilePath = pathToFile.Substring (pathToFile.LastIndexOf (pathChar) + 1);
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
	Debug.Log(myFilePath);
	
	// Start downloading at the given path
	var www = new WWW (myFilePath);
	// Wait for download to finish
	yield www;
	
	audio.clip = www.audioClip;
	
	/*
	while(!audio.isPlaying && audio.clip.isReadyToPlay){
		audio.Play();
	}*/
}

function Update(){
	Debug.Log(audio.clip.isReadyToPlay);
	if (!audio.isPlaying && audio.clip.isReadyToPlay)
	        audio.Play();
}