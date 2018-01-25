using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeCameraButtonScript : MonoBehaviour {

	private Camera cam;
	private CameraFollow camFollow;

	private float speed = 40f, rotateSpeed = 45f;

	[SerializeField]
	private Button btn;
	// Use this for initialization
	void Start () {
		btn.onClick.AddListener (TaskOnClick);
		cam = GetComponent<Camera> ();
		camFollow = GetComponent<CameraFollow> ();
	}
	
	// Update is called once per frame
	void TaskOnClick () {
		camFollow.enabled = !camFollow.enabled;

		cam.transform.localEulerAngles = new Vector3 (27.5f, 0, 0);
	}

	void Update(){
		if (!camFollow.enabled) {


			if (Input.GetKey (KeyCode.UpArrow))
				cam.transform.Rotate(new Vector3(-rotateSpeed * Time.deltaTime/Time.timeScale, 0, 0));

			if (Input.GetKey(KeyCode.DownArrow))
				cam.transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime/Time.timeScale, 0, 0));

			if (Input.GetKey(KeyCode.LeftArrow))
				cam.transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime/Time.timeScale, 0));

			if (Input.GetKey(KeyCode.RightArrow))
				cam.transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime/Time.timeScale, 0));



			if (Input.GetKey (KeyCode.W))
				cam.transform.position += new Vector3 (0, 0, speed * Time.deltaTime/Time.timeScale);

			if (Input.GetKey(KeyCode.S))
				cam.transform.position += new Vector3 (0, 0, -speed * Time.deltaTime/Time.timeScale);
			
			if (Input.GetKey(KeyCode.A))
				cam.transform.position += new Vector3 (-speed * Time.deltaTime/Time.timeScale,0, 0);
			
			if (Input.GetKey(KeyCode.D))
				cam.transform.position += new Vector3 ( speed * Time.deltaTime/Time.timeScale,0, 0);

			if (Input.GetKey(KeyCode.LeftControl))
				cam.transform.position += new Vector3 ( 0 ,-speed * Time.deltaTime/Time.timeScale , 0);

			if (Input.GetKey(KeyCode.LeftShift))
				cam.transform.position += new Vector3 ( 0 ,speed * Time.deltaTime , 0);
			

			

		}
	}
}
