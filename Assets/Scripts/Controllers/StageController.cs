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
    public Transform houseSpawnPosition;    // Where the house is spawned

	public static StageController instance;	// Singleton

    private List<List<GameObject>> stagePool; // <name, objects>
    private const int STARTING_NUM_STAGES = 8; // Number of links to load on start
    [SerializeField]
    private int currentStagePoolIndex = 0;  // Where in the pool we're at

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
		GameController.instance.SetTargetSpeed(stages[currentStageIndex].targetSpeed);

        // Starting song
        AudioController.instance.PlayBackgroundMusic(stages[currentStageIndex].audioClip);

        // Initialize stage pool
        initStagePool();
	}

    // Called when the game starts
    public void GameStart()
    {
        StartCoroutine(switchStages());
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

        // Get from the pool
        GameObject stage = getStageFromPool();
        stage.transform.position = connectionPoint;
        stage.transform.localRotation = Quaternion.identity;

		//Instantiate(stages[currentStageIndex], connectionPoint, Quaternion.identity);
	}

    // Go to the next stage type
    private IEnumerator switchStages()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchingTime);

            // Finished last stage
            if (currentStageIndex + 1 == stages.Length)
            {
                // Spawn the house
                var obj = Instantiate(GameController.instance.house, houseSpawnPosition.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                obj.transform.position = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                break;
            }

            currentStageIndex += 1;

            // Update stuff for this stage
            changeSkybox(stages[currentStageIndex].skybox);
            AudioController.instance.PlayBackgroundMusic(stages[currentStageIndex].audioClip);
            StartCoroutine(changeLightIntensity(stages[currentStageIndex].lightIntensity));
            StartCoroutine(changeLightDirection(stages[currentStageIndex].sunRotation));
            GameController.instance.SetTargetSpeed(stages[currentStageIndex].targetSpeed);
        }
    }

    private IEnumerator changeLightIntensity(float newIntensity)
    {
        float buffer = 0.005f;
        while (mainLight.intensity > newIntensity + buffer || mainLight.intensity < newIntensity - buffer)
        {
            mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, lightChangingSpeed);

            yield return new WaitForSeconds(0);
        }
    }

    private IEnumerator changeLightDirection(Vector3 newRotation)
    {
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

        while ((Mathf.Abs(oldX - newX) > buffer) || (Mathf.Abs(oldY - newY) > buffer) || (Mathf.Abs(oldZ - newZ) > buffer))
        {
            oldX = mainLight.transform.rotation.eulerAngles.x;
            oldY = mainLight.transform.rotation.eulerAngles.y;
            oldZ = mainLight.transform.rotation.eulerAngles.z;

            Vector3 oldVector = new Vector3(oldX, oldY, oldZ);

            Vector3 lerpedVector = Vector3.Lerp(oldVector, newVector, lightDirectionChangeSpeed * Time.deltaTime);

            mainLight.transform.localEulerAngles = lerpedVector;

            yield return new WaitForSeconds(0);
        }

    }

    // Change the skybox for the level
    private void changeSkybox(Material newSkybox){
		RenderSettings.skybox = newSkybox;
	}

    // Initialization for pooling
    private void initStagePool()
    {
        stagePool = new List<List<GameObject>>();
        for(int i = 0; i < stages.Length; i++)
        {
            stagePool.Add(new List<GameObject>());

            for(int j = 0; j < STARTING_NUM_STAGES; j++)
            {
                var obj = Instantiate(stages[i].gameObject);
                obj.SetActive(false);
                obj.GetComponent<Stage>().isPoolActive = true;
                stagePool[i].Add(obj);
            }
        }
    }

    // Grab a stage from the pool
    private GameObject getStageFromPool()
    {
        // List is empty
        if(stagePool[currentStageIndex].Count == 0)
        {
            Stage newStage = Instantiate(stages[currentStageIndex]).GetComponent<Stage>();
            newStage.isPoolActive = false;
            stagePool[currentStageIndex].Insert(currentStagePoolIndex, newStage.gameObject);

            currentStagePoolIndex += 1;

            return newStage.gameObject;
        }

        // End of list
        if(currentStagePoolIndex >= stagePool[currentStageIndex].Count)
        {
            currentStagePoolIndex = 0;  // Start over
        }

        Stage stage = stagePool[currentStageIndex][currentStagePoolIndex].GetComponent<Stage>();

        // Is stage in pool
        if (!stage.isPoolActive)
        {
            // Not in pool - add another link
            Stage newStage = Instantiate(stages[currentStageIndex]).GetComponent<Stage>();
            newStage.isPoolActive = false;
            stagePool[currentStageIndex].Insert(currentStagePoolIndex, newStage.gameObject);

            currentStagePoolIndex += 1;

            return newStage.gameObject;
        }

        // In pool, remove and return
        stage.gameObject.SetActive(true);
        stage.isPoolActive = false;
        stage.alreadySpawned = false;

        currentStagePoolIndex += 1;

        return stage.gameObject;

    }

}
