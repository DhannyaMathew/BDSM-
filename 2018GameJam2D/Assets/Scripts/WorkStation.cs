using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkStation : MonoBehaviour {
	public float MaxTime;
	private float CurrTime;
	public Slider TimeSlider;
	public Image FillImage;
	public Color FullTimeColour = Color.green;
	public Color LowTimeColour = Color.red;
	public AudioClip TimeDone;
	public Vector3 workPos;
	public bool isWorking;
	// Use this for initialization
	void Start () {
		CurrTime = MaxTime;
		SetTime ();
		workPos = transform.GetComponentInChildren<Transform> ().position;
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
			endTask ();
			ResetTimer();
			//change player
		}

	}

	void endTask(){
		isWorking = false;

		//set array of toy elf is holding to next thing on the array

	}

	void Update () {
		if (isWorking){
		DecTime (0.2f);
		}
	}
}
