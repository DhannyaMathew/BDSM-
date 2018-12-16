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

	GameObject sowingObject;
	GameObject repairsObject;
	GameObject boxingObject;
	GameObject wrappingObject;
	GameObject sleighObject;

	Image sowingImage;
	Image repairsImage;
	Image boxingImage;
	Image wrappingImage;
	Image sleighImage;

	// Use this for initialization
	void Start () {
		sowingObject = GameObject.Find ("SowingImage");
		repairsObject = GameObject.Find ("RepairsImage");
		boxingObject = GameObject.Find ("BoxingImage");
		wrappingObject = GameObject.Find ("WrappingImage");
		sleighObject = GameObject.Find ("SleighImage");

		sowingImage = sowingObject.GetComponent<Image> ();
		repairsImage = repairsObject.GetComponent<Image> ();
		boxingImage = boxingObject.GetComponent<Image> ();
		wrappingImage = wrappingObject.GetComponent<Image> ();
		sleighImage = sleighObject.GetComponent<Image> ();

		stationQueue = new Queue<string> ();

		if (this.gameObject.tag == "TeddyBear" || this.gameObject.tag == "Snek" || this.gameObject.tag == "Lion" || this.gameObject.tag == "Socks") {
			stationQueue.Enqueue ("Sowing");
		}

		if (this.gameObject.tag == "RubixCube" || this.gameObject.tag == "Car" || this.gameObject.tag == "Whisk" || this.gameObject.tag == "Drum") {
			stationQueue.Enqueue ("Repairs");
		}

		stationQueue.Enqueue ("Boxing");

		stationQueue.Enqueue ("Wrapping");

		stationQueue.Enqueue ("Sleigh");

		if (this.gameObject.tag == "Coal") {
			stationQueue.Clear ();
			stationQueue.Enqueue ("Sleigh");
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (stationQueue.Contains ("Sowing")) {
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
		}

		displayQueue (true); //THIS BOI IS USED FOR TESTING, REMOVE LATER

	}

	public void dequeueStation() // used to remove a station from the queue of the station, to be called from the station script
	{
		stationQueue.Dequeue ();
	}

	public void displayQueue(bool active) 
	{
		int count = 0;
		int xPos = 0;
		int yPos = 150;
		if (active) {
			if (sowing) {
				sowingImage.enabled = true;

				xPos = -325 + 35 * count;

				sowingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the sowing image
				count++;
			} else {
				sowingImage.enabled = false;
			}

			if (repairs) {
				repairsImage.enabled = true;

				xPos = -325 + 35 * count;

				repairsImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the repairs image
				count++;
			} else {
				repairsImage.enabled = false;
			}

			if (boxing) {
				boxingImage.enabled = true;

				xPos = -325 + 35 * count;

				boxingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the boxing image
				count++;
			} else {
				boxingImage.enabled = false;
			}

			if (wrapping) {
				wrappingImage.enabled = true;

				xPos = -325 + 35 * count;

				wrappingImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the wrapping image
				count++;
			} else {
				wrappingImage.enabled = false;
			}

			if (sleigh) {
				sleighImage.enabled = true;

				xPos = -325 + 35 * count;

				sleighImage.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (xPos, yPos, 0);
				//Display the sleigh image
				count++;
			} else {
				sleighImage.enabled = false;
			}
		}
	}

}
