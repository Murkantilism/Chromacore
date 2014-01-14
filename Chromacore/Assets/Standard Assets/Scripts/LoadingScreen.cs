using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	
	public GUIText loadingText;
	public tk2dSprite animSeqOne;
	public tk2dSprite animSeqTwo;
	public tk2dSprite animSeqThree;
	
	bool loadSeqOne;
	bool loadSeqTwo = false;
	bool loadSeqThree = false;
	
	float fadeAlphaOne;
	float fadeAlphaTwo;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("loadAnimation", 0, 2);
		loadSeqOne = true;
		DoComplex();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Take care of all the heavy computations before the level
	// Save results into data structures with DontDestroyOnLoad
	void DoComplex(){
		
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
}
