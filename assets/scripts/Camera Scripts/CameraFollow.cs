using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Camera cam;

	[SerializeField]
	private GameObject car;

	private Vector3 camOffset;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		camOffset = transform.position - car.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCam ();
	}

	void MoveCam(){

	
		transform.position = car.transform.position + camOffset;

	}
}
