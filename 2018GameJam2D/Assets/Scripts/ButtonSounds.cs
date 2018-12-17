using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//required when dealing with event data
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, ISelectHandler { 
	//behaviour inherited for on select and on press events and functions

	BaseEventData buttonEvent;
	public AudioClip buttonHover;

	//do this whene UI is selected
	public void OnSelect (BaseEventData eventData) {
		SoundController.instance.RandomPitchandsfx (buttonHover);
	}
}
