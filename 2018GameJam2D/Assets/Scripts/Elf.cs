﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Elf : MonoBehaviour {

		public Transform target;
		public float ZOffset; //recall sprites pivot is at the bottom
		public NavMeshAgent myAgent;
		public KeyCode pull,turn;
		private Rigidbody2D Player;
		private bool left, right;
		public AudioClip PickUpBox, TurnBox;
		private bool AlreadyPlayed;
		private Animator playerAnim;
		private bool moving, pushing;
		private Queue<Vector3> destinationQueue; //Queue with all the destinations 
		private Vector3 currentDestination;
		private bool starting; //Used in the case of the first movement

		
		void Start(){
			playerAnim = GetComponent<Animator> ();
			//_audio = GetComponent<AudioSource> ();
			Player = GetComponent<Rigidbody2D> ();
			destinationQueue = new Queue<Vector3> ();
			print (destinationQueue.Count);
			starting = true;
		}


		void Update (){
		//Use Navmesh Velocity since Sprite Velocity is not changed
		float Horizontal = myAgent.velocity.x;
		Vector3 move = new Vector3(Horizontal,0,myAgent.velocity.z);




		if (move.magnitude > 0.15 || move.magnitude < -0.15 ) {
			moving = true;

		} else {
			moving = false;
		}


		/*if (Vector3.Distance (transform.position, currentDestination) > 2) {
			moving = true;
			print (Vector3.Distance (transform.position, currentDestination));
		}
		else{
			moving = false;
		}*/
			
				/*if ((Horizontal < 0.15f && Horizontal > -0.15f) || pushing) {
					moving = false;
				} else if (!pushing && (Horizontal < -0.15f || Horizontal > 0.15f)){
					moving = true;
				}*/
		
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

			if (Vector3.Distance (transform.position, currentDestination) <= 1.6 && destinationQueue.Count != 0) {
				dequeueDestination ();
			}
		}

			}



		void TurnLeft(){
			if (left)
				return;
			transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			left = true;
			right = false;
		}
		void TurnRight(){
			if (right)
				return;
			transform.localScale = new Vector3 (Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
			left = false;
			right = true;
		}

		//to move this object after the cube controller has moved, it is placed in late update
		void LateUpdate ()
		{
		transform.localPosition = new Vector3 (target.localPosition.x, transform.localPosition.y, target.localPosition.z + ZOffset);


		}

	public void enqueueDestination(Vector3 destination) //adds a destination to the queue
	{
		destinationQueue.Enqueue (destination);
	}

	public void dequeueDestination() //removes a destination from the queue
	{
		currentDestination = destinationQueue.Dequeue ();
		myAgent.GetComponent<Agent> ().goNextDestination (currentDestination);
	}
		



	}
