using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour {

	public float moveSpeed;
	public float rotateSpeed;
	public Vector2 changeDirectionX;

	private float moveDirection = 1;	// 1 or -1 depending on direction

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkDirection();
		rotate();
		move();
	}

	// See if you need to change directions
	private void checkDirection(){
		if(transform.position.x > changeDirectionX.y || transform.position.x < changeDirectionX.x){
			moveDirection *= -1;
		}
	}

	// Rotate the snowball
	private void rotate(){
		transform.RotateAround(transform.position, Vector3.forward, rotateSpeed*Time.deltaTime*moveDirection);
	}

	// Move the snowball
	private void move(){
		float newX = transform.position.x + moveSpeed*Time.deltaTime*moveDirection;
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

}
