#pragma strict

var deniz = false;
var jeff = false;
var korbin = false;
var jen = false;
var ronny = false;
var andre = false;
var bj = false;
var cheyenne = false;
var casper = false;
var joshua = false;
var fmod = false;
var kenny = false;
var soomla = false;
var mainCamera : GameObject;

var defaultURL = "https://www.facebook.com/incendiaryindustries";

function Start () {
	mainCamera = GameObject.FindWithTag("MainCamera");
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
	if (deniz){
		Application.OpenURL("http://www.ccs.neu.edu/home/ozkaynak/");
	}else if(jeff){
		Application.OpenURL(defaultURL);
	}else if(korbin){
		Application.OpenURL(defaultURL);
	}else if(jen){
		Application.OpenURL(defaultURL);
	}else if(ronny){
		Application.OpenURL(defaultURL);
	}else if(andre){
		Application.OpenURL(defaultURL);
	}else if(bj){
		Application.OpenURL(defaultURL);
	}else if(cheyenne){
		Application.OpenURL("https://www.behance.net/gallery/Portfolio/15502353");
	}else if(casper){
		Application.OpenURL("http://www.northeastern.edu/camd/artdesign/people/casper-harteveld/");
	}else if(joshua){
		Application.OpenURL("http://www.linkedin.com/pub/joshua-gross/1/a42/443");
	}else if(fmod){
		Application.OpenURL("http://www.fmod.org/");
	}else if(kenny){
		Application.OpenURL("http://kenney.itch.io/kenney-donation");
	}else if(soomla){
		Application.OpenURL("http://project.soom.la/");
	}
}

function OnGUI() {
	GUI.backgroundColor = Color.magenta;
	
	var buttonStyle = new GUIStyle("button");
	buttonStyle.fontSize = 25;
	
	if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
		Destroy(mainCamera);
		Application.LoadLevel("MainMenu");
	}
}

function Update(){
	// If the backspace or espace key is hit, go back to main menu
	if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape)){
		Destroy(mainCamera);
		Application.LoadLevel("MainMenu");
	}
}