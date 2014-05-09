using UnityEngine;
using System.Collections;

public class ObstacleCollision : MonoBehaviour {
	private tk2dSpriteAnimator anim; // Animator for this obstacle

	// Get Teli Animation GO
	public GameObject teliAnimGO;

	// Get the Teli animation object
	public tk2dSpriteAnimator Teli;

	public GameObject powTextGO; // pow gameObject

	tk2dSprite powText; // pow text sprite

	int powCnt; //pow counter used for scaling

	// Use this for initialization
	void Start () {
		teliAnimGO = GameObject.Find("AnimatedSprite");

		Teli = teliAnimGO.GetComponent<tk2dSpriteAnimator>();

		anim = GetComponent<tk2dSpriteAnimator>();

		// Find the pow GO
		powTextGO = GameObject.Find("PunchTextAnimation");
		// Get the text sprite
		powText = powTextGO.GetComponent<tk2dSprite>();
		// Set the initial scale
		powText.scale = new Vector3(0.1f, 0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		// Increase the scale as long the counter is non-zero & less than 10
		if(powCnt > 0 && powCnt < 10){
			// To make it scale a little slower, only scale if x is even
			if (powCnt % 2 == 0){
				powText.scale += new Vector3(0.1f, 0.1f, 0.1f);
			}
			powCnt++;
		}
		// If the counter is maxed, fade text away
		if(powCnt >= 10){
			FadePowText();
		}
	}

	// Upon picking up this object, trigger events
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player")
		{
			Debug.Log("Obstacle Hit");
			// And they are punching
			
			if(Teli.IsPlaying("Punch"))
			{
				// Break the obstacle
				BreakObstacle();
				// Reset scale pow text
				powText.scale = new Vector3(0.1f, 0.1f, 0.1f);
				// Reset pow counter
				powCnt = 0;
				// Reset pow color
				powText.color = new Color(1, 1, 1, 1);
				// Show pow text
				ShowPowText();
				// Play the breaking sound
				audio.Play();
			}
		}
	}
	
	// Play break animation, then destroy gameObject
	void BreakObstacle()
	{
		// Play the break animation
		anim.Play("Obstacle"); 
		Debug.Log("Obstacle break!");
		// Destroy Obstacle gameObject after 5 seconds
		Invoke("Destroy", 5);
	}

	// Used to move and scale the pow text sprite
	void ShowPowText(){
		// Move the pow text to this location
		powTextGO.transform.position = gameObject.transform.position;
		// Move it up and over 5X, 5Y so it's visisble near obstalce
		powTextGO.transform.position += new Vector3(5, 6, 0);

		// Incrememnt pow counter
		powCnt++;
	}

	// Fade the text sprite away by reducing it's alpha value
	void FadePowText(){
		powText.color -= new Color(0, 0, 0, 0.01f);
	}
	
	// Destroy the obstacle
	void Destroy()
	{
		Destroy(gameObject);
	}
	

}
