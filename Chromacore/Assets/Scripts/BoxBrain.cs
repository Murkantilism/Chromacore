using UnityEngine;
using System.Collections;

public class BoxBrain : MonoBehaviour {

	// Properties
	public Sprite[] BoxAnimation;
	public Sprite[] TeliPunchAnimation;
	public AudioClip[] BoxSounds;

	SpriteRenderer TeliSpriteRenderer;

	float time;
	int index;
	bool shouldPlayAnimation;

	SpriteRenderer BoxRenderer;


	// Methods
	void DestroyYourself () {
		Destroy (gameObject);
		Destroy (gameObject.transform.parent.gameObject);
	}

	int RandomIntLowerThan(int x) {
		int rand = (int)Random.Range (0, x);
		return rand;
	}

	void OnTriggerStay2D(Collider2D col) {
		for (int i = 0; i < TeliPunchAnimation.Length; i++) {
			if (TeliSpriteRenderer.sprite == TeliPunchAnimation[i]) {
				Destroy(GetComponent<BoxCollider2D>());
				shouldPlayAnimation = true;
				AudioSource BoxAudioSource = GetComponent<AudioSource>();
				BoxAudioSource.clip = BoxSounds[RandomIntLowerThan(BoxSounds.Length)];
				BoxAudioSource.Play();
				Invoke("DestroyYourself", 3f);
			}
		}
	}

	// Use this for initialization
	void Start () {
		TeliSpriteRenderer = GameObject.FindGameObjectWithTag ("Teli").GetComponent<SpriteRenderer> ();
		BoxRenderer = GetComponent<SpriteRenderer> ();

		shouldPlayAnimation = false;
		time = 0f;
		index = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldPlayAnimation) {
			if (index >= BoxAnimation.Length)
				shouldPlayAnimation = false;
			else {
				time += Time.deltaTime;
				if (time >= 2*Time.fixedDeltaTime) {
					time = 0f;
					BoxRenderer.sprite = BoxAnimation[index];
					index++;
				}
			}
		}
	}
}
