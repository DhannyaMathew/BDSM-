using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyClock : MonoBehaviour {

	public string[] toyTags = new string[9]{"TeddyBear","Snek","Lion","Socks","RubixCube","Car","Whisk","Drum","Coal"};
	public int[] toyCount = new int[9]{0,0,0,0,0,0,0,0,0};
	public int[] toyCountGoal = new int[9]{0,0,0,0,0,0,0,0,0};
	public Text[] toyText = new Text[9];

	public GameObject Next;
	public GameObject Resume;
	public AudioClip Ticking, Ring;

	int GoalVal;

	public float MaxTime;
	private float CurrTime;
	public Slider TimeSlider;
	public Image FillImage;
	public Color FullTimeColour = Color.green;
	public Color LowTimeColour = Color.red;
	public GameObject TimeBooster;
	//private float randomBoostVal;
	public Transform BottomLine;
	private bool endLevel;

	public static DailyClock instance = null;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != null) {
			Destroy (gameObject);
		}
	}

	void Start () {
		SoundController.instance.PlayTick (Ticking);
		CurrTime = MaxTime;

		endLevel = false;

		for (int i = 0; i < toyCountGoal.Length; i++) {
			GoalVal = Random.Range (1 * SceneManager.GetActiveScene ().buildIndex - 1, Mathf.FloorToInt(2.5f * SceneManager.GetActiveScene ().buildIndex));
			toyCountGoal [i] = GoalVal;
		}
		Invoke ("InvokeSpawn",Random.Range(0,CurrTime/50f));

	}

	void SetTime(){
		TimeSlider.maxValue = MaxTime;
		TimeSlider.value = CurrTime;	
		FillImage.color = Color.Lerp (LowTimeColour, FullTimeColour, CurrTime / MaxTime);
	}

	public void ResetTimer(){
		CurrTime = MaxTime;
		SetTime ();
	}

	public void DecTime(float value){

		CurrTime -= value;
		SetTime ();
		if (CurrTime <= 0) {
			EndLevel ();
		}

	}

	void EndLevel(){
		//end screen with pass of fail = add a panel to block player from being able to keep playing

		SoundController.instance.Ticking.Stop();
		SoundController.instance.Ticking.loop = false;
		SoundController.instance.Ticking.clip = Ring;
		SoundController.instance.Ticking.Play();

		endLevel = true;
		GameManager.instance.endLevel = true;

		for (int i = 0; i < toyCount.Length; i++) {
			if (toyCount [i] < toyCountGoal[i]) {
				GameManager.instance.loseLevel ();
				Next.SetActive (false);
				Resume.SetActive (false);
				break;
			}
			GameManager.instance.winLevel ();
			Next.SetActive (true);
			Resume.SetActive (false);
		}


	}


	void Update () {
		if (!endLevel && !GameManager.instance.GamePaused) {
			DecTime ( 0.2f);
		}

		if (GameManager.instance.PauseScreen.activeInHierarchy) {
			for (int i = 0; i < toyText.Length; i++) {
				toyText[i].text = toyCount[i]+"/"+toyCountGoal[i];
			}
		}

		/*if (!isSpawning) {
			
			if (randomBoostVal > 0f) {
				
				Invoke ("SpawnBoost", randomBoostVal);
				isSpawning = true;
			}
		}*/
	}
	void InvokeSpawn(){
		//Debug.Log ("henlo");
		if (!GameManager.instance.GamePaused || !endLevel){
		Instantiate (TimeBooster, new Vector3(Random.Range(BottomLine.position.x-(BottomLine.localScale.x/2f),BottomLine.position.x+(BottomLine.localScale.x/2f)),BottomLine.position.y,BottomLine.position.z), Quaternion.identity);
		//isSpawning = false;
			Invoke ("InvokeSpawn",Random.Range(0,CurrTime/50f));
		}
	}
}
