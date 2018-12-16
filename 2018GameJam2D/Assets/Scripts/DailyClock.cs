using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyClock : MonoBehaviour {

	public string[] toyTags = new string[8]{"TeddyBear","Snek","Lion","Socks","RubixCube","Car","Whisk","Drum"};
	public int[] toyCount = new int[8]{0,0,0,0,0,0,0,0};

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
	}

	void Update () {
		if (!endLevel) {
			DecTime (0.005f);
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
