using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsOnCollision : MonoBehaviour {

	public int pointsToAdd = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "nigel"){
			PointsController.instance.AddPoints(pointsToAdd);
		}
	}

}
