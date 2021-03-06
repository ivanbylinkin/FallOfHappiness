﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	// Floating point variable to store the player's movement speed.
	public float speed;
	// Store a reference to the Rigidbody2D component required to use 2D Physics.
	private Rigidbody2D rb2d;
	// Used to store a reference to the Player's animator component.
	private Animator animator;
	// Used to keep track of whether user is collided with something that he/she can enteract with
	bool canOpen = false;
	private Animator interactable;

	// Use this for initialization
	void Start()
	{
		// Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();
		// Get a component reference to the Player's animator component
		animator = GetComponent<Animator>();
	}

	// FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		// Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		// Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		string dir = AnimateDirection (moveHorizontal, moveVertical);

		if (dir != "") {
			animator.SetTrigger (dir);
		}

		MovePlayer (moveHorizontal, moveVertical);
	}

	void Update(){
		// check if user is trying to open an object
		if (canOpen && Input.GetKeyDown("space")){
			interactable.SetTrigger ("open");
		}
	}

	// If user collides with something, it could be a door
	// If it is a door, user can go through it
	void OnCollisionEnter2D (Collision2D col){
		if (col.gameObject.name.IndexOf ("Door") > -1) {
			canOpen = true;
			interactable = col.gameObject.GetComponent<Animator> ();
		} else { 
			canOpen = false;
		}
	}

	string AnimateDirection(float moveHorizontal, float moveVertical){
		// figure out which way the player should be "walking"
		if (moveHorizontal > 0) {
			return "playerRight";
		} else if (moveHorizontal < 0) {
			return "playerLeft";
		} else if (moveVertical > 0) {
			return "playerBackward";
		} else if (moveVertical < 0) {
			return "playerForward";
		}
		return "";
	}

	void MovePlayer(float moveHorizontal, float moveVertical){
		if (moveVertical != 0 || moveHorizontal != 0) {
			// Use the two store floats to create a new Vector2 variable movement.
			Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

			// Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
			rb2d.velocity = (movement * speed);
		} else {
			rb2d.velocity = Vector2.zero;
		}
	}
}
