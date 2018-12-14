using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfManagerScript : MonoBehaviour {

	public GameObject[] elfList;
	public GameObject currentElf;
	public GameObject currentElfGrandParent;
	public LayerMask ElfCollider;

	// Use this for initialization
	void Start () {
		elfList = GameObject.FindGameObjectsWithTag ("Elf");

		for (int loop = 0; loop < elfList.Length; loop++) {
			elfList [loop].GetComponent<ElfColliderClick>().agent.resetSelection();
			//elfList [loop].GetComponent<Agent> ().resetSelection();
		}
		currentElf = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit,Mathf.Infinity,ElfCollider)) {
				//Debug.Log (hit.collider.gameObject.name);
				if (hit.collider.tag == "Elf") { //if select ground, set new destination
					currentElf = hit.collider.gameObject;
					//GameObject.Find(hit.collider.gameObject.transform.name);
					//print (currentElf.name);
				}
			}
		}

		if (currentElf != null) {
			for (int loop = 0; loop < elfList.Length; loop++) {
				if (elfList [loop].transform.parent.parent.name == currentElf.transform.parent.parent.name) {
					elfList [loop].GetComponent<ElfColliderClick>().agent.thisSelected();
					//elfList [loop].GetComponent<Agent> ().thisSelected ();
				} else {
					elfList [loop].GetComponent<ElfColliderClick>().agent.resetSelection();
					//elfList [loop].GetComponent<Agent> ().resetSelection ();
				}
			}
		}

	}
}
