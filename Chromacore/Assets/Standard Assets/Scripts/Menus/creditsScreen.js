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

var defaultURL = "https://www.facebook.com/incendiaryindustries";

function Start () {

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
		Application.OpenURL(defaultURL);
	}else if(casper){
		Application.OpenURL(defaultURL);
	}else if(joshua){
		Application.OpenURL(defaultURL);
	}
}

function OnGUI() {
	var buttonStyle = new GUIStyle("button");
	buttonStyle.fontSize = 25;
	
	if (GUI.Button(new Rect(Screen.width/2 + Screen.width/4, Screen.height/2 + Screen.height/4, 200, 100), "Back", buttonStyle)){
		Application.LoadLevel("MainMenu");
	}
}