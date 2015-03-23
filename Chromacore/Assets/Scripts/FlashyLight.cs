using UnityEngine;
using System.Collections;

public class FlashyLight : MonoBehaviour {

	Light light;
	float tm;
	float tm2;
	int sign;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		tm = 0;
		tm2 = 0;
		sign = 1;
	}
	
	// Update is called once per frame
	void Update () {
		tm += Time.deltaTime;
		tm2 += Time.deltaTime;
	
		if (tm2 >= Random.Range(0.01f, 0.05f)) {
			if (light.intensity < 6 && sign == -1) {
				sign *= -1;
				tm = 0;
			}
			light.intensity += (float)(sign * Random.Range (0.3f, 0.7f));
			tm2 = 0;
		}

		if (tm >= Random.Range(0.1f, 0.3f)) {
			sign *= -1;
			tm = 0;
		}
	}
}
