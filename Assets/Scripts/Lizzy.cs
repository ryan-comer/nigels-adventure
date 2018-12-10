using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizzy : MonoBehaviour {

	public Transform nigel;  // Used to match Nigel's X value

	[Range(0, 1)]
	public float distanceChangeRate = 0.2f;
	[Range(0, 1)]
	public float xChangeRate = 0.1f;

	public float targetDistance;

	// Use this for initialization
	void Start () {
		targetDistance = getDistanceFromNigel(transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Change speed when Nigel's speed changed (relative speed).  Also, maybe find another way to represent speed that takes this into account
		moveTowardsNigelX();
		moveTowardsTargetDistance();
	}
	
	// Change the target distance
	public void ChangeDistance(float amountToChange){
		// Corner case - you passed Nigel
		if(transform.position.z + amountToChange > nigel.transform.position.z){
			targetDistance = 0;
			return;
		}

		Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + amountToChange);
		float newDistance = getDistanceFromNigel(newPosition);

		targetDistance = newDistance;
	}

	// Move to match Nigel's X - interpolate
	private void moveTowardsNigelX(){
		float targetX = nigel.position.x;
		float newX = Mathf.Lerp(transform.position.x, targetX, xChangeRate);

		Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);
		transform.position = newPosition;
	}

	private float getDistanceFromNigel(Vector3 otherPosition){
		Vector3 difference = otherPosition - nigel.position;
		float distance = difference.magnitude;
		distance = Mathf.Abs(distance);

		return distance;
	}

	// Go towards the target distance over time
	// Lerp
	private void moveTowardsTargetDistance(){
		Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, nigel.position.z - targetDistance);
		transform.position = Vector3.Lerp(transform.position, targetPosition, distanceChangeRate);
	}

	void OnTriggerEnter(Collider collider){
		// Check if Nigel
		if(collider.gameObject.GetComponent<Nigel>() != null){
			GameController.instance.GameOver();
		}
	}

}
