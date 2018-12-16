﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

	public Transform[] ToyPos;
	int moveToy = -1;
	public GameObject[] Toys;
	int randomToy;
	public float conveyorSpeed;
	private Material Belt;
	private Vector2 offSet;
	public float Xvel,Yvel;

	// Use this for initialization
	void Start () {
		Belt = GetComponent<Renderer> ().material;
		//ToyFilled = new int[ToyPos.Length];
		for (int i = 0; i < ToyPos.Length; i++) {
			//ToyFilled [i] = i + 1;
			randomToy = Random.Range (0, ToyPos.Length - 1);
			Instantiate (Toys [randomToy], ToyPos [i].position, Quaternion.identity, ToyPos [i]);
		}
		StartCoroutine ("moveBelt");

	}

	IEnumerator moveBelt(){
		while (true) {
			if (moveToy != -1) {
				offSet = new Vector2 (Xvel, Yvel);
				for (int j = 0; j <= moveToy; j++) {
					if (ToyPos [j].childCount != 0) {
						Destroy (ToyPos [0].GetComponentInChildren<GameObject> ());
					}
					for (int k = 0; k < ToyPos.Length-1; k++) {
						ToyPos [k+1].GetComponentInChildren<Transform> ().SetParent (ToyPos [k]);
						//ToyPos [k].DetachChildren();
						ToyPos [k].GetComponentInChildren<Transform> ().position = ToyPos [k].position;
					}
					randomToy = Random.Range (0, ToyPos.Length - 1);
					Instantiate (Toys [randomToy], ToyPos [ToyPos.Length - 1].position, Quaternion.identity, ToyPos [ToyPos.Length - 1]);
					yield return new WaitForSeconds (conveyorSpeed);
				}
				offSet = new Vector2 (0, 0);
				moveToy = -1;
			}
			Belt.mainTextureOffset += offSet * Time.deltaTime;
		}
	}
	// Update is called once per frame
	void Update () {
		if (moveToy == -1) {
			for (int i = 0; i < ToyPos.Length; i++) {
				//ToyFilled [i] = i + 1;
				if (ToyPos [i].childCount == 0) {
					moveToy = i;

				}
			}
		}
	}

}
