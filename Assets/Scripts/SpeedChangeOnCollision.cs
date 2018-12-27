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
		if(collider.gameObject.GetComponent<Nigel>() != null){
			// Check for catnip
			if(collider.gameObject.GetComponent<Nigel>().isCatnipOn){
				return;
			}

			GameController.instance.AddSpeed(speedToAdd);
			GameController.instance.AddLizzyDistance(lizzyDistanceChange);
		}
	}

}
