using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToyScript : MonoBehaviour {

	Queue<string> stationQueue;

	bool sowing;
	bool repairs;
	bool boxing;
	bool wrapping;
	bool sleigh;
	bool recharge;

	public bool onHead, pickedUp;
	public Sprite[] ToyState = new Sprite[5]; //ensure last state is null!!*************
	int toyStateCounter = 0;
	SpriteRenderer toyImage;
	public Transform toyHeadPos; //make child of Elf holding it
	public Vector3 toyWalkPos;
	public Elf currElf;

	GameObject sowingObject;
	GameObject repairsObject;
	GameObject boxingObject;
	GameObject wrappingObject;
	GameObject sleighObject;
	GameObject rechargeObject;

	Image rechargeImage;
	Image sowingImage;
	Image repairsImage;
	Image boxingImage;
	Image wrappingImage;
	Image sleighImage;

	// Use this for initialization
	void Awake () {
		toyImage = this.GetComponent<SpriteRenderer> ();
		toyWalkPos = this.GetComponentInChildren<Transform> ().position;
		toyImage.sprite = ToyState [toyStateCounter];

		sowingObject = GameObject.Find ("SowingImage");
		repairsObject = GameObject.Find ("RepairsImage");
		rechargeObject = GameObject.Find ("RechargeImage");
		boxingObject = GameObject.Find ("BoxingImage");
		wrappingObject = GameObject.Find ("WrappingImage");
		sleighObject = GameObject.Find ("SleighImage");

		sowingImage = sowingObject.GetComponent<Image> ();
		repairsImage = repairsObject.GetComponent<Image> ();
		rechargeImage = rechargeObject.GetComponent<Image> ();
		boxingImage = boxingObject.GetComponent<Image> ();
		wrappingImage = wrappingObject.GetComponent<Image> ();
		sleighImage = sleighObject.GetComponent<Image> ();

		stationQueue = new Queue<string> ();

		if (this.gameObject.tag == "TeddyBear" || this.gameObject.tag == "Snek" || this.gameObject.tag == "Lion" || this.gameObject.tag == "Socks") {
			stationQueue.Enqueue ("Sowing");
			sowing = true;
			for (int i = 0; i < ToyState.Length-1; i++) {
				ToyState [i] = ToyState [i + 1]; 
			}
			ToyState [4] = null;
		}

		if (this.gameObject.tag == "RubixCube" || this.gameObject.tag == "Car" || this.gameObject.tag == "Whisk" || this.gameObject.tag == "Drum") {
			stationQueue.Enqueue ("Repairs");
			repairs = true;
		}

		if (this.gameObject.tag == "RubixCube" || this.gameObject.tag == "Drum") {
			for (int i = 0; i < ToyState.Length-1; i++) {
				ToyState [i] = ToyState [i + 1]; 
			}
			ToyState [4] = null;
		}

		if (this.gameObject.tag == "Car" || this.gameObject.tag == "Whisk") {
			stationQueue.Enqueue ("Recharge");
			recharge = true;
		}

		stationQueue.Enqueue ("Boxing");
		boxing = true;
		stationQueue.Enqueue ("Wrapping");
		wrapping = true;
		stationQueue.Enqueue ("Sleigh");
		sleigh = true;

		if (this.gameObject.tag == "Coal") {
			stationQueue.Clear ();
			stationQueue.Enqueue ("Sleigh");
		}

	}

	public void NextState(){
		toyStateCounter++;
		toyImage.sprite = ToyState [toyStateCounter];
	}
	// Update is called once per frame
	void Update () {
		if (currElf != null) {
			if (currElf.getCurrentDestination() == toyWalkPos && !pickedUp) {//Vector3.Distance (currElf.GetComponent<Transform>().position, workPos) < 1.9f && 
				onHead = true;
				pickedUp = true;
				Destroy(this.transform.GetChild(0).gameObject);

			}
		}

		if (onHead && pickedUp && currElf != null) {
			transform.position = toyHeadPos.position;
			toyImage.enabled = true;
		} 

		if (!onHead && pickedUp && currElf != null) {
			toyImage.enabled = false;
		}

		/*if (stationQueue.Contains ("Sowing")) {
			sowing = true;
		} else {
			sowing = false;
		}

		if (stationQueue.Contains ("Repairs")) {
			repairs = true;
		} else {
			repairs = false;
		}

		if (stationQueue.Contains ("Boxing")) {
			boxing = true;
		} else {
			boxing = false;
		}

		if (stationQueue.Contains ("Wrapping")) {
			wrapping = true;
		} else {
			wrapping = false;
		}

		if (stationQueue.Contains ("Sleigh")) {
			sleigh = true;
		} else {
			sleigh = false;
		}*/

		//displayQueue (true); //THIS BOI IS USED FOR TESTING, REMOVE LATER

	}

	public void dequeueStation() // used to remove a station from the queue of the station, to be called from the station script
	{
		stationQueue.Dequeue ();
	}
	public string toyStation() //Shows Item at the front of the Queue
	{
		return stationQueue.Peek ();
	}

	public void displayQueue(bool active) 
	{
		int count = 0;
		int xPos = 0;
		int yPos = 145;
		if (active) {
			if (sowing) {
				sowingImage.enabled = true;

				xPos = -320 + 55 * count; //-325 35

				sowingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the sowing image
				count++;
			} else {
				sowingImage.enabled = false;
			}

			if (repairs) {
				repairsImage.enabled = true;

				xPos = -320 + 55 * count;

				repairsImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the repairs image
				count++;
			} else {
				repairsImage.enabled = false;
			}

			if (recharge) {
				rechargeImage.enabled = true;

				xPos = -320 + 55 * count;

				rechargeImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the repairs image
				count++;
			} else {
				rechargeImage.enabled = false;
			}

			if (boxing) {
				boxingImage.enabled = true;

				xPos = -320 + 55 * count;

				boxingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the boxing image
				count++;
			} else {
				boxingImage.enabled = false;
			}

			if (wrapping) {
				wrappingImage.enabled = true;

				xPos = -320 + 55 * count;

				wrappingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the wrapping image
				count++;
			} else {
				wrappingImage.enabled = false;
			}

			if (sleigh) {
				sleighImage.enabled = true;

				xPos = -320 + 55 * count;

				sleighImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the sleigh image
				count++;
			} else {
				sleighImage.enabled = false;
			}
		}
	}

}
