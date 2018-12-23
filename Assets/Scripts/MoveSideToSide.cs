using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideToSide : MonoBehaviour {

	public float moveSpeed;
	public Vector2 changeDirectionX;

	private float moveDirection = 1;	// 1 or -1 depending on direction

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkDirection();
		move();
	}

	// See if you need to change directions
	private void checkDirection(){
		if(transform.position.x > changeDirectionX.y){
			moveDirection = -1;
		}else if(transform.position.x < changeDirectionX.x){
			moveDirection = 1;
		}
	}

	// Move the snowball
	private void move(){
		float newX = transform.position.x + moveSpeed*Time.deltaTime*moveDirection;
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
	
}
