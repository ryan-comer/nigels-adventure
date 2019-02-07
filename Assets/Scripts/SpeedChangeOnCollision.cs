using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChangeOnCollision : MonoBehaviour {

	public float speedToAdd;
	public float lizzyDistanceChange;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit(Collider collider){
		if(collider.gameObject.tag == "nigel"){
			// Check for catnip
			if(GameController.instance.nigel.isCatnipOn){
				return;
			}

			GameController.instance.AddSpeed(speedToAdd * (GameController.instance.difficultyFactor + 1.0f));
			GameController.instance.AddLizzyDistance(lizzyDistanceChange * ((GameController.instance.difficultyFactor+1)/2));
		}
	}

}
