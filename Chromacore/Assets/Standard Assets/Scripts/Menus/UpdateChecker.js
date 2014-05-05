#pragma strict
public class UpdateChecker extends MonoBehaviour {
	var url = "http://www.ccs.neu.edu/home/ozkaynak/Chromacore/ChromacoreVersion.html";
	var www : WWW;

	var versionText : TextMesh;

	var updateAvaiableText : TextMesh;

	var versionNumber : String;

	var ChromacoreUpdateURL : String;

	var updateButtonp : boolean;

	function Start(){
		//versionText = GameObject.Find("Version Text") as TextMesh;
		
		//updateAvaiableText = GameObject.Find("Update Text") as TextMesh;
		
		// Don't show update message until we know one is available
		updateAvaiableText.renderer.enabled = false;
		
		www = new WWW(url);
		
	 #if UNITY_ANDROID
		ChromacoreUpdateURL = "https://play.google.com/store/apps/details?id=com.Deniz_Ozkaynak.Chromacore&ah=Dl143G_1ozvYTWrN8VT_t0SKydE";
	 #endif
		
		
	 #if UNITY_IPHONE
		ChromacoreUpdateURL = "";
	 #endif
		
	 #if UNITY_STANDALONE
		ChromacoreUpdateURL = "http://www.desura.com/games/chromacore";
	 #endif
		
		
		GetVersion();
	}

	// Grab the version number from a webpage URL
	function GetVersion(){
		// Wait for the request to complete
		yield www;

		// Check for errors
		if(www.error == null){
			// Request is complete
			Debug.Log("WWW data : " + www.text);
			versionNumber = www.text.ToString();
		}else{
			Debug.Log("WWW ERROR : " + www.error);
		}
		
		CheckVersion();
	}

	// Check the version # against the in-game version #
	// Display update avaialable text accordingly
	function CheckVersion(){
		if(!(versionText.text == versionNumber)){
			updateAvaiableText.renderer.enabled = true;
		}
		
		if(versionText.text == versionNumber){
			updateAvaiableText.renderer.enabled = false;
		}
	}

	function OnMouseEnter(){
		// Change the color of the text
		renderer.material.color = Color.blue;
	}

	function OnMouseExit(){
		// Change the color of the text back to the original color (white)
		renderer.material.color = Color.white;
	}

	function OnMouseUp(){
		if(updateButtonp && updateAvaiableText.renderer.enabled == true){
			Application.OpenURL(ChromacoreUpdateURL);
		}
	}
}