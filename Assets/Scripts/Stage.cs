using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

	public float zValueToSendStageCreateCommand;
	public float lightIntensity;	// Light intensity for this stage
	public string stageID;

	private bool alreadySpawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		checkStageSpawn();
	}

	// See if another stage segment needs to be created
	private void checkStageSpawn(){
		if(alreadySpawned){
			return;
		}

		if(transform.position.z < zValueToSendStageCreateCommand){
			StageController.instance.SpawnStage(this);
			alreadySpawned = true;
		}
	}

}
