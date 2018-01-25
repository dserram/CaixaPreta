using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedometerScript : MonoBehaviour {


	static float minAngle = 219f;
	static float maxAngle = -46f;
	static SpeedometerScript speedometerScript;
	// Use this for initialization
	void Start () {
		speedometerScript = this;
	}
	
	// Update is called once per frame
	public static void ShowSpeed (float speed, float min, float max) {

	
		float angle = Mathf.Lerp (minAngle, maxAngle, Mathf.InverseLerp (min, max, speed));
		speedometerScript.transform.eulerAngles = new Vector3 (0, 0, angle);
	}
}
