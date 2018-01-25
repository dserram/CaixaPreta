using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeAccelArrows : MonoBehaviour {

	private float sizeMag = 3f;

	//[SerializeField]
	//private static GameObject AccelX, AccelY, AccelZ;

	[SerializeField]
	private Rigidbody carRigidBody;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public static void ResizeAccelHUD (float x, float y, float z, GameObject AccelX, GameObject AccelY, GameObject AccelZ) {
		

		//AccelX.transform.localScale = new Vector3 (1, Random.Range (0, 1f), 1);
		//AccelY.transform.localScale = new Vector3 (1, Random.Range (0, 1f), 1);
		//AccelZ.transform.localScale = new Vector3 (1, Random.Range (0, 1f), 1);
		//print(carRigidBody.transform.)

		float xNorm = 3 * x / (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z));
		float yNorm = 3 * y / (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z));
		float zNorm = 3 * z / (Mathf.Abs(x) + Mathf.Abs(y) + Mathf.Abs(z));

		AccelX.transform.localScale = new Vector3 (1, xNorm, 1);
		AccelY.transform.localScale = new Vector3 (1, yNorm, 1);
		AccelZ.transform.localScale = new Vector3 (1, zNorm, 1);


	}
}
