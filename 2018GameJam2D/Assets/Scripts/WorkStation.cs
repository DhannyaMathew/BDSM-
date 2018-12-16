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
	public Elf currElf;
	public ToyScript Toy;

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
		currElf.working = false;
		currElf = null;
		Toy.dequeueStation ();
		Toy.NextState ();
		Toy.onHead = true;
		currElf.holding = true;
		currElf.dequeueDestination();
		if (currElf.destinationCount() <= 0){
			currElf.myAgent.GetComponent<Agent>().RemoveToy();
			Destroy(Toy.GetComponent<GameObject>());
			Toy.currElf = null; 
			currElf.holding = false;
			for (int i = 0; i < DailyClock.instance.toyTags.Length; i++){ 
				if (this.gameObject.tag == DailyClock.instance.toyTags[i]) {
					DailyClock.instance.toyCount [i]++;
				}
			}
		}
		Toy = null;
	}

	void Update () {
		if (currElf != null) {
			if (currElf.getCurrentDestination() == workPos) {//Vector3.Distance (currElf.GetComponent<Transform>().position, workPos) < 1.9f && 
				isWorking = true;
				Toy.onHead = false;
				currElf.working = true;
				currElf.holding = false;
			}
		}
		if (isWorking){
		DecTime (0.2f);
		}
	}
}
