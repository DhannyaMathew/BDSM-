using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{

	public GameObject myElf;
	private GameObject Toy;
	public bool selected = true;
	// The elf can only be given places to go if current elf is selected.
	private NavMeshAgent agent;
	private WorkStation stationSelected;
	private ToyScript toySelected;
	//private ConveyorBelt addNewToy;
	public AudioClip wrongStation;
	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
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

					if ((hit.collider.tag == "TeddyBear" || hit.collider.tag == "Snek" || hit.collider.tag == "Lion" || hit.collider.tag == "Socks" || hit.collider.tag
						== "RubixCube" || hit.collider.tag == "Car" || hit.collider.tag == "Whisk" || hit.collider.tag == "Drum" || hit.collider.tag == "Coal") && Toy == null) {
						Toy = hit.collider.gameObject;
						Toy.GetComponent<BoxCollider> ().enabled = false;
						Destroy(Toy.GetComponent<Transform>().GetChild(0).gameObject);
						Toy.transform.parent = null;
						//Toy.GetComponentInParent<Transform> ().DetachChildren(); //NEED TO TEST THIS BAD BOI FOR CONVEYOR BELT SCRIPT

						toySelected = Toy.GetComponent<ToyScript> ();
						setNextDestination (toySelected.toyWalkPos);
						toySelected.toyHeadPos = myElf.GetComponent<Elf>().toySpriteHolder;
						toySelected.currElf = myElf.GetComponent<Elf> ();

					}

					if (hit.collider.tag == "Boxing" || hit.collider.tag == "Wrapping" || hit.collider.tag == "Recharge" || hit.collider.tag == "Repairs" 
						|| hit.collider.tag == "Sowing" || hit.collider.tag == "Sleigh" ||hit.collider.tag == "Trash") { 
						
						
						stationSelected = hit.collider.gameObject.GetComponent<WorkStation> ();
						setNextDestination (stationSelected.workPos);

						if (Toy != null && stationSelected.currElf == null) {
							if (toySelected.toyStation () == stationSelected.gameObject.tag) {
								//if (stationSelected.currElf == null) {
								stationSelected.currElf = myElf.GetComponent<Elf> ();
								//}
								stationSelected.Toy = toySelected; 

							}
						} else {
							SoundController.instance.Playone (wrongStation);
						}

					}
				}
			}
			/*if (myElf.GetComponent<Elf>().working) {
				Debug.Log (Vector3.Distance (myElf.transform.position, stationSelected.workPos));
			}*/

			if (Toy != null) {
				Toy.GetComponent<ToyScript> ().displayQueue (selected);
			}

		}
	}

	public void RemoveToy(){
		Toy = null;

	}

	void setNextDestination (Vector3 destination)
	{
		myElf.GetComponent<Elf> ().enqueueDestination (destination);
	}

	public void goNextDestination (Vector3 destination)
	{
		agent.SetDestination (destination);
	}



	public void thisSelected ()
	{
		selected = true;
	}

	public void resetSelection ()
	{
		selected = false;
	}
}
