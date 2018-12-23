using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

	// TODO: Add object pooling for the level segments - OPTIMIZATION

	public Stage[] stages;
	public Light mainLight;	 // Main light for the scene
	[Range(0.0f, 1.0f)]
	public float lightChangingSpeed;
	public int switchingTime;	// How long before switching stages (seconds)

	public int currentStageIndex;

	public static StageController instance;	// Singleton

	void Awake(){
		instance = this;	// Assign to singleton
	}

	// Use this for initialization
	void Start () {
		currentStageIndex = 0;

		// Initial light intensity
		StartCoroutine(changeLightIntensity(stages[currentStageIndex].lightIntensity));

		StartCoroutine(switchStages());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Go to the next stage type
	private IEnumerator switchStages(){
		while(true){
			yield return new WaitForSeconds(switchingTime);

			currentStageIndex = (currentStageIndex + 1) % stages.Length;	// Go to the next stage
			StartCoroutine(changeLightIntensity(stages[currentStageIndex].lightIntensity));
		}
	}

	private IEnumerator changeLightIntensity(float newIntensity){
		float buffer = 0.005f;
		while(mainLight.intensity > newIntensity + buffer || mainLight.intensity < newIntensity - buffer){
			mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, lightChangingSpeed);

			yield return new WaitForSeconds(0);
		}
	}

	public void SpawnStage(Stage stageThatSentCommand){
		Vector3 connectionPoint = Vector3.zero;
		bool connectionFound = false;
		// Find the connection point
		foreach(Transform child in stageThatSentCommand.gameObject.transform){
     		if(child.tag == "connection_point"){
				connectionPoint = child.transform.position;
				connectionFound = true;
				break;
			}
 		}

		if(!connectionFound){
			Debug.LogError("Error getting connection point for stage");
			return;
		}

		Instantiate(stages[currentStageIndex], connectionPoint, Quaternion.identity);
	}

}
