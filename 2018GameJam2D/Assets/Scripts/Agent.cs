using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {

	private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		//assigning target to agent component
		if (Input.GetMouseButtonDown(0)){
			//Create raycast to find new target
			//change world cooridnates of mouse into world screen coordinates using camera
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray,out hit)) {
				if (hit.collider.tag == "Ground") { //if select ground, set new destination
					agent.SetDestination(hit.point);
				}
			}
		}
	}
}
