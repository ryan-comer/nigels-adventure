using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour {

	// TODO: Add object pooling for the level segments - OPTIMIZATION

	public Stage[] stages;
	public Light mainLight;	 // Main light for the scene
	[Range(0.0f, 1.0f)]
	public float lightChangingSpeed;
	public float lightDirectionChangeSpeed;
	public int switchingTime;	// How long before switching stages (seconds)

	public int currentStageIndex;

	public static StageController instance;	// Singleton

	void Awake(){
		instance = this;	// Assign to singleton
	}

	// Use this for initialization
	void Start () {
		currentStageIndex = 0;	// First stage

		// Initial light intensity
		StartCoroutine(changeLightIntensity(stages[currentStageIndex].lightIntensity));

		// Initial light direction
		StartCoroutine(changeLightDirection(stages[currentStageIndex].sunRotation));

		// Initial skybox
		changeSkybox(stages[currentStageIndex].skybox);

		// Starting speed
		GameController.instance.targetSpeed = stages[currentStageIndex].targetSpeed;

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

			// Update stuff for this stage
			changeSkybox(stages[currentStageIndex].skybox);
			StartCoroutine(changeLightIntensity(stages[currentStageIndex].lightIntensity));
			StartCoroutine(changeLightDirection(stages[currentStageIndex].sunRotation));
			GameController.instance.targetSpeed = stages[currentStageIndex].targetSpeed;
		}
	}

	private IEnumerator changeLightIntensity(float newIntensity){
		float buffer = 0.005f;
		while(mainLight.intensity > newIntensity + buffer || mainLight.intensity < newIntensity - buffer){
			mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, lightChangingSpeed);

			yield return new WaitForSeconds(0);
		}
	}

	private IEnumerator changeLightDirection(Vector3 newRotation){
		float buffer = 1;
		
		// Get the old values
		float oldX = mainLight.transform.rotation.eulerAngles.x;
		float oldY = mainLight.transform.rotation.eulerAngles.y;
		float oldZ = mainLight.transform.rotation.eulerAngles.z;

		// Get the new values
		float newX = newRotation.x;
		float newY = newRotation.y;
		float newZ = newRotation.z;
		Vector3 newVector = new Vector3(newX, newY, newZ);

		while((Mathf.Abs(oldX - newX) > buffer) || (Mathf.Abs(oldY - newY) > buffer) || (Mathf.Abs(oldZ - newZ) > buffer)){
			oldX = mainLight.transform.rotation.eulerAngles.x;
			oldY = mainLight.transform.rotation.eulerAngles.y;
			oldZ = mainLight.transform.rotation.eulerAngles.z;

			Vector3 oldVector = new Vector3(oldX, oldY, oldZ);

			Vector3 lerpedVector = Vector3.Lerp(oldVector, newVector, lightDirectionChangeSpeed * Time.deltaTime);
			Vector3 wrappedVector = new Vector3(UnwrapAngle(lerpedVector.x), UnwrapAngle(lerpedVector.y), UnwrapAngle(lerpedVector.z));

			mainLight.transform.localEulerAngles = lerpedVector;

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

	// Change the skybox for the level
	private void changeSkybox(Material newSkybox){
		RenderSettings.skybox = newSkybox;
	}

	private static float WrapAngle(float angle)
	{
		angle%=360;
		if(angle >180)
			return angle - 360;

		return angle;
	}

	private static float UnwrapAngle(float angle)
	{
		if(angle >=0)
			return angle;

		angle = -angle%360;

		return 360-angle;
	}

}
