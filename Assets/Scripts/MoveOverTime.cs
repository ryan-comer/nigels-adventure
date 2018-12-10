using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour {

	[HideInInspector]
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		// Set the starting speed
		moveSpeed = GameController.instance.CurrentSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		move();
	}

	// Move the obstacle down the path
	private void move(){
		float newZ = transform.position.z + moveSpeed*Time.deltaTime*-1;
		transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
	}
	
}
