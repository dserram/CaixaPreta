using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReplicateImuData : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private Rigidbody rigidBody;
	private List<float> dataList;
	private static int count = 0;
	private int maxReadings;
	float nextActionTime = 0f;
	private float g = 9.81f;

	private int numData = 7; //número de dados em cada linha do arquivo de texto( 4 quaternions e 3 accel)

	private static List<float> posX, posY, posZ, rotX, rotY, rotZ,rotW;

	private static bool isPaused = false;

	private static bool hasFilledLists = false; //o programa só vai habilitar os controles de reprodução depois de ter rodado a primeira vez
												//e populado as listas de posição e rotação

	[SerializeField]
	private GameObject HUDaccelX, HUDaccelY, HUDaccelZ;

	[SerializeField]
	private GameObject labelLoading, loadingPlane;


	[SerializeField]
	private Slider timeSlider;

	[SerializeField]
	private Text labelTimer;

	[SerializeField]
	private Button btnPausePlay, btnFreeCam;

	[SerializeField]
	private Dropdown dropdownSpeed;

	[SerializeField]
	private TrailRenderer leftSkid, rightSkid;

	[SerializeField]
	private Toggle toggleShowSkid;

	[SerializeField]
	private GameObject speedometer;

	[SerializeField]
	private TextAsset dataFile;

	// Use this for initialization
	void Start () {
		
		posX = new List<float> ();
		posY = new List<float> ();
		posZ = new List<float> ();

		rotX = new List<float> ();
		rotY = new List<float> ();
		rotZ = new List<float> ();
		rotW = new List<float> ();

		rigidBody = GetComponent<Rigidbody> ();

		dataList = ReadString ();
		maxReadings = dataList.Count;
		timeSlider.maxValue = maxReadings/numData;
		btnPausePlay.onClick.AddListener (PausePlayOnClick);
		//timeSlider.OnPointerDown(0);
		Time.timeScale = 10;

		SetUIEnabled (false);

		AddEventTriggers (timeSlider);
		toggleShowSkid.onValueChanged.AddListener (showSkidMarks);


	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		rotateByQuaternion ();
		saveAbsolutePosition ();


	}


	static List<float> ReadString()
	{
		//string path = "Assets/Data/dados4.txt";
		string path = "dados4.txt";
		List<float> quatList = new List<float> ();




		string line;

		using (StreamReader reader = new StreamReader(path))
		{
		
			do {
				string[] quatTemp;
				line = reader.ReadLine ();
				try{
				quatTemp = line.Split(' ');
				}catch{
					break;
				}

				foreach( string quat in quatTemp){
					quatList.Add(float.Parse(quat));

				}
	
			} while(line != null);
		}



		return quatList;
	
	}

	private void rotateByQuaternion(){
		
		if (count >= dataList.Count) {
		
			rigidBody.transform.position = new Vector3 (posX [0], posY [0], posZ [0]);
			rigidBody.transform.rotation = new Quaternion(rotX [0], rotY [0], rotZ [0], rotW [0]);
			count = 0;
			Pause ();
			leftSkid.Clear ();
			rightSkid.Clear ();

			if (!hasFilledLists) {

				hasFilledLists = true;
				SetUIEnabled (true);
				Time.timeScale = 1;

			
			}
			else
			return;
		}
		if (isPaused)
			return;
			
		if (!hasFilledLists) {
			Quaternion q = new Quaternion ();

			q.Set (dataList [count], dataList [count + 1], dataList [count + 2], dataList [count + 3]);
			rigidBody.transform.rotation = q;
			//rigidBody.transform.Rotate (new Vector3 (0, 180, 180)); //<< correção do carro de cabeça pra baixo

			rigidBody.AddForce (new Vector3 (dataList [count + 4] * g, dataList [count + 5] * g, dataList [count + 6] * g), ForceMode.Acceleration);
			//print (q.eulerAngles.ToString());
		} else {
			
			Time.timeScale = getPlaybackSpeed ();
			Quaternion q = new Quaternion ();
			q.Set (rotX [count / numData], rotY [count / numData], rotZ [count / numData], rotW [count / numData]);
			rigidBody.transform.position = new Vector3 (posX [count/numData], posY [count/numData], posZ [count/numData]);
			rigidBody.transform.rotation = q;

		}





			ResizeAccelArrows.ResizeAccelHUD (dataList [count + 4] * g, dataList [count + 5] * g, dataList [count + 6] * g, HUDaccelX, HUDaccelY, HUDaccelZ);
			

			//print (count);
			count += numData; //primeiros 4 dados -> quaternions, dados 5, 6 e 7 acelerometro
			timeSlider.value = count/numData;
			updateLabel (count/numData);
		

	
	}


	IEnumerator rotateIMU(){
		rotateByQuaternion ();
		yield return new WaitForSeconds (1f);
	}


	public void OnPointerDown(PointerEventData data){

		isPaused = true;

	}

	public void OnPointerUp(PointerEventData data){

		isPaused = false;
	}

	public void updateLabel(int count){

		labelTimer.text = count + "/" + maxReadings/numData;
	}



	public void Pause(){

		if (!hasFilledLists)
			return;
		
		isPaused = true;
		rigidBody.isKinematic = true;
		btnPausePlay.GetComponentInChildren<Text>().text = "Play";
	}

	public void Play(){

		if (!hasFilledLists)
			return;
		
		isPaused = false;
		rigidBody.isKinematic = false;
		btnPausePlay.GetComponentInChildren<Text>().text = "Pause";
	}

	private void PausePlayOnClick(){

		if (!hasFilledLists)
			return;

		if (!isPaused)
			Pause ();
		else
			Play ();

	}

	private void saveAbsolutePosition(){

		if (isPaused )
			return;

		if (hasFilledLists)
			return;
		
		posX.Add (rigidBody.transform.position.x);
		posY.Add (rigidBody.transform.position.y);
		posZ.Add (rigidBody.transform.position.z);

		rotX.Add (rigidBody.transform.rotation.x);
//		print (rigidBody.transform.rotation.x);
		rotY.Add (rigidBody.transform.rotation.y);
		rotZ.Add (rigidBody.transform.rotation.z);
		rotW.Add (rigidBody.transform.rotation.w);
	}

	private float getPlaybackSpeed(){

		float speed;

		switch (dropdownSpeed.value) {
		case 0:
			speed = 1f;
			break;
		case 1:
			speed = 2f;
			break;
		case 2:
			speed = 5f;
			break;
		case 3:
			speed = 0.5f;
			break;
		default:
			speed = 1f;
			break;

		}

		return speed;

	}

	public void OnPointerDownDelegate(PointerEventData data)
	{
		if (hasFilledLists)
			Pause ();
	}

	public void DragDelegate(PointerEventData data)
	{
		if (!hasFilledLists)
			return;
		
		int pos = (int)timeSlider.value;
		Quaternion q = new Quaternion ();
		q.Set (rotX [pos], rotY [pos], rotZ [pos], rotW [pos]);
		rigidBody.transform.position = new Vector3 (posX [pos], posY [pos], posZ [pos]);
		rigidBody.transform.rotation = q;
		updateLabel (pos);
		count = pos * numData;

	}

	private void AddEventTriggers(Slider timeSlider){

		EventTrigger trigger = timeSlider.GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });

		EventTrigger.Entry drag = new EventTrigger.Entry();
		drag.eventID = EventTriggerType.Drag;
		drag.callback.AddListener((data) => { DragDelegate((PointerEventData)data); });

		trigger.triggers.Add(entry);
		trigger.triggers.Add(drag);
	}

	public void SetUIEnabled(bool enabled){

		/*dropdownSpeed.enabled = enabled;
		timeSlider.enabled = enabled;
		btnPausePlay.enabled = enabled;
		toggleShowSkid.enabled = enabled;
		btnFreeCam.enabled = enabled;*/

		timeSlider.enabled = enabled;

		dropdownSpeed.gameObject.SetActive (enabled);
		btnPausePlay.gameObject.SetActive (enabled);
		toggleShowSkid.gameObject.SetActive (enabled);
		btnFreeCam.gameObject.SetActive (enabled);
		speedometer.SetActive (enabled);
		HUDaccelX.SetActive (enabled);
		HUDaccelY.SetActive (enabled);
		HUDaccelZ.SetActive (enabled);

		if (enabled) {
			
			labelLoading.SetActive(false);
			loadingPlane.SetActive(false);
		}

	}

	public void showSkidMarks(bool show){

		leftSkid.gameObject.SetActive (show);
		rightSkid.gameObject.SetActive (show);
	}
}



