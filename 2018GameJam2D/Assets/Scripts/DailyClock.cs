using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyClock : MonoBehaviour {
	public float MaxTime;
	private float CurrTime;
	public Slider TimeSlider;
	public Image FillImage;
	public Color FullTimeColour = Color.green;
	public Color LowTimeColour = Color.red;
	public AudioClip TimeDone;

	// Use this for initialization
	void Start () {
		CurrTime = MaxTime;
	}

	void SetTime(){
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
			ResetTimer();
			//change player
		}

	}

	void EndLevel(){

	}

	void Update () {
		DecTime (0.2f);
	}
}
