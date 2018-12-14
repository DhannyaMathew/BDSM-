using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {

	public GameObject myElf;
	public bool selected = true; // The elf can only be given places to go if current elf is selected.
	private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			//assigning target to agent component
			if (Input.GetMouseButtonDown (0)) {
				//Create raycast to find new target
				//change world cooridnates of mouse into world screen coordinates using camera
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "Ground") { //if select ground, set new destination
						setNextDestination (hit.point);
					}
				}
			}
		}
	}
		
	void setNextDestination(Vector3 destination)
	{
		myElf.GetComponent<Elf> ().enqueueDestination (destination);
	}

	public void goNextDestination(Vector3 destination)
	{
		agent.SetDestination (destination);
	}
		
	public void thisSelected()
	{
		selected = true;
	}

	public void resetSelection()
	{
		selected = false;
	}
}
