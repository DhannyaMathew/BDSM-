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

	int GoalVal;

	public float MaxTime;
	private float CurrTime;
	public Slider TimeSlider;
	public Image FillImage;
	public Color FullTimeColour = Color.green;
	public Color LowTimeColour = Color.red;
	public AudioClip TimeDone;
	public GameObject TimeBooster;
	private float randomBoostVal;
	public Transform BottomLine;
	private bool isSpawning, endLevel;

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
		CurrTime = MaxTime;
		isSpawning = false;
		endLevel = false;

		for (int i = 0; i < toyCountGoal.Length; i++) {
			GoalVal = Random.Range (1 * SceneManager.GetActiveScene ().buildIndex - 1, Mathf.CeilToInt(2.5f * SceneManager.GetActiveScene ().buildIndex));
			toyCountGoal [i] = GoalVal;
		}

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

		CurrTime -= 0.2f;
		SetTime ();
		if (CurrTime <= 0) {
			EndLevel ();
		}

	}

	void EndLevel(){
		//end screen with pass of fail = add a panel to block player from being able to keep playing
		endLevel = true;
		GameManager.instance.endLevel = true;

		for (int i = 0; i < toyCount.Length; i++) {
			if (toyCount [i] < toyCountGoal[i]) {
				GameManager.instance.loseLevel ();
				goto end;
			}
			GameManager.instance.winLevel ();
			Next.SetActive (true);
			Resume.SetActive (false);
		}

		end: 
		Next.SetActive (false);
		Resume.SetActive (false);
	}

	void Update () {
		if (!endLevel) {
			DecTime (0.005f);
		}

		if (GameManager.instance.PauseScreen.activeInHierarchy) {
			for (int i = 0; i < toyText.Length; i++) {
				toyText[i].text = toyCount[i]+"/"+toyCountGoal[i];
			}
		}

		if (!isSpawning) {
			randomBoostVal = Random.Range (CurrTime,CurrTime-30f);
			if (randomBoostVal > 0f) {
				Invoke ("SpawnBoost", randomBoostVal);
				isSpawning = true;
			}
		}
	}
	void SpawnBoost(){
		Instantiate (TimeBooster, new Vector3(Random.Range(BottomLine.position.x-(BottomLine.localScale.x/2f),BottomLine.position.x+(BottomLine.localScale.x/2f)),BottomLine.position.y,BottomLine.position.z), Quaternion.identity, BottomLine);
		isSpawning = false;
	}
}
