using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBoosterPU : MonoBehaviour {
	private DailyClock Clock;
	public AudioClip pop;

	void Start (){
		Clock = GameObject.FindObjectOfType<DailyClock> ();
	}
	void OnDestroy () {
		SoundController.instance.Playone (pop);
	}
	
	// Update is called once per frame
	void OnMouseOver () {
		if (Input.GetMouseButtonDown (0)) {
			Clock.DecTime (-10f);
			Destroy (gameObject);

		}
	}
}
