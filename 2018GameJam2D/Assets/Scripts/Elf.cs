using System.Collections;
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
		
		void Start(){
			playerAnim = GetComponent<Animator> ();
			//_audio = GetComponent<AudioSource> ();
			Player = GetComponent<Rigidbody2D> ();
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
		

	}
