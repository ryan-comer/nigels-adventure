using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour {

	public GameObject[] trash;	// Array of gameobjects that act as trash that falls out

	public Vector2 fallRange;	// What positions should the trashcan be elligible to fall

	private float fallLocation;	// This is where the trashcan will fall

	// Use this for initialization
	void Start () {
		disableColliders();
		getFallLocation();
	}
	
	// Update is called once per frame
	void Update () {
		checkForFall();	
	}

	// Disable all of the colliders for the trash objects
	private void disableColliders(){
		foreach(var t in trash){
			t.GetComponent<Collider>().enabled = false;
		}
	}

	// Randomly determine the fall location
	private void getFallLocation(){
		fallLocation = Random.Range(fallRange.x, fallRange.y);
	}

	// Should the trashcan fall
	private void checkForFall(){
		if(transform.position.z < fallLocation){
			// Start the animation
			GetComponent<Animator>().SetTrigger("fall");
		}
	}

	// Enable all of the colliders for the trash
	public void EnableTrashColliders(){
		foreach(var t in trash){
			t.GetComponent<Collider>().enabled = true;
		}
	}

}
