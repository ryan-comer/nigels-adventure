﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nigel : MonoBehaviour {

	// Bounds for where Nigel can move
	public float leftXBound;
	public float rightXBound;

	public float horizontalSpeed = 5.0f;
	public float startingJumpHeight = 5.0f;
	public float startingGravity = -9.81f;
	[HideInInspector]
	public float jumpHeight, gravity;

	public bool isCatnipOn = false;	// Is the catnip powerup activated

	private bool isJumping = false;
	private float deltaY = 0.0f;	// How much to change Nigel's Y every frame

	private float lastY;	// Used to see if you're falling
	private bool jumped = false;	// Used to ensure you only trigger the falling animation once per jump

	private Animator anim;

	// Use this for initialization
	void Start () {
		jumpHeight = startingJumpHeight;
		gravity = startingGravity;

		anim = GetComponent<Animator>();
		Debug.Assert(anim);
		lastY = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		getInput();
	}

	// Handle input for this frame
	private void getInput(){
		float moveX = Input.GetAxis("Horizontal");

		if(Input.GetKeyDown(KeyCode.Space) && !isJumping){
			jump();
		}
		
		if(!isJumping){
			moveNigel(moveX);
		}

		handlePhysics();
	}

	// Move Nigel based on the X value
	private void moveNigel(float moveX){
		// Check the horizontal bounds
		if(transform.position.x + moveX < leftXBound || transform.position.x + moveX > rightXBound){
			return;
		}

		// Apply the speed
		moveX *= horizontalSpeed * Time.deltaTime;

		// Update the position
		Vector3 newPosition = new Vector3(transform.position.x + moveX, transform.position.y, transform.position.z);
		transform.position = newPosition;
	}

	// Make nigel jump
	private void jump(){
		// Only jump if in the running state
		if(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Run"){
			return;
		}

		deltaY = jumpHeight;
		isJumping = true;
		jumped = true;

		// Trigger the animation
		anim.SetTrigger("jump");
		anim.SetBool("grounded", false);
	}


	private void handlePhysics(){
		// Move nigel
		float newY = transform.position.y + deltaY*Time.deltaTime;

		// Grounded
		if(newY < 0){
			newY = 0;
			isJumping = false;
			deltaY = 0;

			// Trigger the animation
			anim.SetBool("grounded", true);
			anim.SetBool("falling", false);
		}

		// Move Nigel
		transform.position = new Vector3(transform.position.x, newY, transform.position.z);

		// Apply gravity
		deltaY += gravity*Time.deltaTime;

		checkFall();	// Check for animation
	}

	private void checkFall(){
		if(jumped && (transform.position.y < lastY)){
			anim.SetBool("falling", true);
			jumped = false;
		}

		lastY = transform.position.y;
	}

}
