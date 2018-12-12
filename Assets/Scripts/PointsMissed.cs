using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMissed : MonoBehaviour {

	public string pointsTag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		// Check if points
		if(collider.gameObject.tag == pointsTag){
			PointsController.instance.PointMissed();
		}
	}

}
