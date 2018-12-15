using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour {

	public GameObject myElf;
	private GameObject Toy;
	public bool selected = true; // The elf can only be given places to go if current elf is selected.
	private NavMeshAgent agent;
	private WorkStation stationSelected;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (selected) {
			//assigning target to agent component
			if (Input.GetMouseButtonDown (0) && !GameManager.instance.endLevel) {
				//Create raycast to find new target
				//change world cooridnates of mouse into world screen coordinates using camera
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					if (hit.collider.tag == "Ground") { //if select ground, set new destination
						setNextDestination (hit.point);
					}

					if (hit.collider.tag == "Toy" && Toy == null) {
						Toy = hit.collider.gameObject;//*****
					}

					if (hit.collider.tag == "WorkStation") { //NEED TO TEST WHAT CURRENT ELF IS HOLDING AND WHAT IT NEEDS TO BE DONE IN ARRAY
						//IF NEEDS THIS STATION, SET OFF TRUE HERE AND ON STATION WHEN REACH DESTINATION = STATION CHECKS IF TRUE HERE WHEN ARRIVED AT THE VECTOR POSITION AND AVTIVATES 
						//CLOCK AND SPENDS TIME, sets WORKING AS TRUE
						stationSelected = hit.collider.gameObject.GetComponent<WorkStation> ();

						//check if toy = gameobjecti snull and check its array for task, if next = current station
						//myElf.GetComponent<Elf>().working = true;
						//stationSelected.isWorking = true;
						//stationSelected.currAgent = myElf.GetComponent<Elf>().working;
						//stationSelected.Toy = Toy; 
						setNextDestination (stationSelected.workPos);
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
