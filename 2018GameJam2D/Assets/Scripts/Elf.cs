﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour
{

	public Transform target;
	public Transform toySpriteHolder;
	public float ZOffset;
	//recall sprites pivot is at the bottom
	public NavMeshAgent myAgent;

	private Rigidbody2D Player;
	private bool left, right;

	private bool AlreadyPlayed;
	private Animator playerAnim;
	private bool moving, pushing;
	private Queue<Vector3> destinationQueue;
	//Queue with all the destinations
	private Vector3 currentDestination;
	public bool starting;
	public bool working;
	public bool holding;
	//Used in the case of the first movement
	//make a bool for working
	public AudioClip workingSFX;
		
	void Start ()
	{
		working = false;
		playerAnim = GetComponent<Animator> ();
		//_audio = GetComponent<AudioSource> ();
		Player = GetComponent<Rigidbody2D> ();
		destinationQueue = new Queue<Vector3> ();
		//print (destinationQueue.Count);
		starting = true;
	}


	void Update ()
	{
		//Use Navmesh Velocity since Sprite Velocity is not changed
		float Horizontal = myAgent.velocity.x;
		Vector3 move = new Vector3 (Horizontal, 0, myAgent.velocity.z);




		if ((move.magnitude > 0.15 || move.magnitude < -0.15) && !working) {
			moving = true;
			playerAnim.SetBool ("Walking", moving);
		} else if ((move.magnitude < 0.15 || move.magnitude > -0.15) && !working){
			moving = false;
			playerAnim.SetBool ("Walking", moving);
		}
		playerAnim.SetBool ("Working", working);
		playerAnim.SetBool ("Holding", holding);

		if (Horizontal > 0) {
			TurnLeft ();
		} else if (Horizontal < 0) {
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

	public void StartWorkingSFX(){
		this.GetComponent<AudioSource>().clip = workingSFX;
		this.GetComponent<AudioSource> ().Play ();
	}

	public void StopWorkingSFX(){
		this.GetComponent<AudioSource> ().Stop ();
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
	public int destinationCount(){
		return destinationQueue.Count;
	}
	public float getCurrentDestination(){
		return Vector3.Distance (transform.position, currentDestination);
	}
	//Make a delay thing ye boi


}
