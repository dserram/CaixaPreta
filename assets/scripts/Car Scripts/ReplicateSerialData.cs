/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading;
using System.IO.Ports;
using System;


public class ReplicateSerialData : MonoBehaviour {

	private Rigidbody rigidBody;

	public float time_between_readings= 0.0000001f;
	float nextActionTime = 0f;
	private float g = 9.81f;

	private SerialPort _serialPort ;

	string message ;


	[SerializeField]
	private GameObject HUDaccelX, HUDaccelY, HUDaccelZ;

	// Use this for initialization
	void Start () {

		rigidBody = GetComponent<Rigidbody> ();
		Thread readThread = new Thread(Read);

		// Create a new SerialPort object with default settings.
		_serialPort = new SerialPort();


		// Set the read/write timeouts
		_serialPort.ReadTimeout = 500;
		_serialPort.WriteTimeout = 500;

		_serialPort.Open();

		readThread.Start();

	}

	// Update is called once per frame
	void Update () {
		

		rotateByQuaternion (ReadString(message));
	

	
	}


	static List<float> ReadString(string message)
	{
		List<float> quatList = new List<float> ();

				string[] quatTemp;
			
				try{
				quatTemp = message.Split(' ');

				}catch{
					break;
				}

				foreach( string quat in quatTemp){
					quatList.Add(float.Parse(quat));
				
				}

		


		return quatList;
	}

	private void rotateByQuaternion(List<float> dataList){

		Quaternion q = new Quaternion();

		q.Set (dataList [0], dataList [1], dataList [2], dataList [3]);
		rigidBody.transform.rotation = q;

		rigidBody.AddForce(new Vector3(dataList[4]*g, dataList[5]*g, dataList[6]*g), ForceMode.Acceleration);
		//print (q.eulerAngles.ToString());

		ResizeAccelArrows.ResizeAccelHUD (dataList [4] * g, dataList [5] * g, dataList [6] * g, HUDaccelX, HUDaccelY, HUDaccelZ);



	}




	public static void Read()
	{
		while (true)
		{
			try
			{
				message = _serialPort.ReadLine();
				print(message);
			}
			catch (TimeoutException) { }
		}
	}
}

*/

