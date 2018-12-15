using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour
{

	public Transform target;
	public float ZOffset;
	//recall sprites pivot is at the bottom
	public NavMeshAgent myAgent;
	public KeyCode pull, turn;
	private Rigidbody2D Player;
	private bool left, right;
	public AudioClip PickUpBox, TurnBox;
	private bool AlreadyPlayed;
	private Animator playerAnim;
	private bool moving, pushing;
	private Queue<Vector3> destinationQueue;
	//Queue with all the destinations
	private Vector3 currentDestination;
	private bool starting, working;
	//Used in the case of the first movement
	//make a bool for working

		
	void Start ()
	{
		working = false;
		playerAnim = GetComponent<Animator> ();
		//_audio = GetComponent<AudioSource> ();
		Player = GetComponent<Rigidbody2D> ();
		destinationQueue = new Queue<Vector3> ();
		print (destinationQueue.Count);
		starting = true;
	}


	void Update ()
	{
		//Use Navmesh Velocity since Sprite Velocity is not changed
		float Horizontal = myAgent.velocity.x;
		Vector3 move = new Vector3 (Horizontal, 0, myAgent.velocity.z);




		if (move.magnitude > 0.15 || move.magnitude < -0.15) {
			moving = true;

		} else {
			moving = false;
		}
		
		playerAnim.SetBool ("Walking", moving);
		if (Horizontal < 0) {
			TurnLeft ();
		} else if (Horizontal > 0) {
			TurnRight ();
		}

		if (destinationQueue.Count != 0) {

			if (starting) {
				dequeueDestination ();
				starting = false;
			}

			if (Vector3.Distance (transform.position, currentDestination) <= 1.6f && destinationQueue.Count != 0 && !working) {
				dequeueDestination ();
			}
		}

	}



	void TurnLeft ()
	{
		if (left)
			return;
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		left = true;
		right = false;
	}

	void TurnRight ()
	{
		if (right)
			return;
		transform.localScale = new Vector3 (Mathf.Abs (transform.localScale.x), transform.localScale.y, transform.localScale.z);
		left = false;
		right = true;
	}

	//to move this object after the cube controller has moved, it is placed in late update
	void LateUpdate ()
	{
		transform.localPosition = new Vector3 (target.localPosition.x, transform.localPosition.y, target.localPosition.z + ZOffset);


	}

	public void enqueueDestination (Vector3 destination) //adds a destination to the queue
	{
		destinationQueue.Enqueue (destination);
	}

	public void dequeueDestination () //removes a destination from the queue
	{
		currentDestination = destinationQueue.Dequeue ();
		myAgent.GetComponent<Agent> ().goNextDestination (currentDestination);
	}


	//Make a delay thing ye boi


}
