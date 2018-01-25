using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour {

	private Rigidbody rigidBody;

	[SerializeField]
	private WheelCollider wheelLeft, wheelRight;

	[SerializeField]
	private float jumpSpeed = 35000f;

	[SerializeField]
	private float moveForce = 45000f;

	[SerializeField]
	private Camera mainCam;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.centerOfMass = new Vector3(0, -1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		moveCar ();

	}

	void moveCar(){


		if (!mainCam.GetComponent<CameraFollow> ().enabled)
			return; //desabilita a movimentação se o simulador estiver em modo free camera
		
		if(Input.GetKey("w")){
			float translation = Input.GetAxis ("Vertical") * moveForce;
			translation *= Time.deltaTime;
			rigidBody.AddForce (this.transform.forward * translation * 40);

		}

		if(Input.GetKey("s")){
			float translation = Input.GetAxis ("Vertical") * moveForce;
			translation *= Time.deltaTime;
			rigidBody.AddForce (this.transform.forward * translation * 40);

		}

		if(Input.GetKey("d")){
			float rotation = Input.GetAxis ("Horizontal") * moveForce;
			rotation *= Time.deltaTime;
			rigidBody.AddTorque (this.transform.up *  rotation *43);


		}

		if(Input.GetKey("a")){
			float rotation = Input.GetAxis ("Horizontal") * moveForce;
			rotation *= Time.deltaTime;
			rigidBody.AddTorque (this.transform.up *  rotation * (43));


		}

		SpeedometerScript.ShowSpeed (rigidBody.velocity.magnitude, 0, 120);

		if (Input.GetKeyDown ("space")){
			Vector3 temp = rigidBody.velocity;
			temp.y += Random.Range(0, jumpSpeed);
			temp.x = Random.Range (0, 5f);


			rigidBody.velocity = temp;
			rigidBody.transform.Rotate (Random.Range (0, 15f), Random.Range (0, 15f), Random.Range (0, 15f));
			rigidBody.AddForce (Random.Range (0, 1500f), Random.Range (0, 105f), Random.Range (0, 15f));


		}

	}


}
