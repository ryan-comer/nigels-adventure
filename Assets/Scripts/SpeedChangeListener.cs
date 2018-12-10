using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveOverTime))]
public class SpeedChangeListener : MonoBehaviour {

	private MoveOverTime mot;

	// Use this for initialization
	void Start () {
		mot = GetComponent<MoveOverTime>();

		GameController.instance.onSpeedChanged += onSpeedChanged;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void onSpeedChanged(float newSpeed){
		mot.moveSpeed = newSpeed;
	}
	
}
