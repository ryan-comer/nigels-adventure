using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Camera cam;

	public Nigel nigel;
	public Lizzy lizzy;

	public SpawnableObject[] spawnableObjects;
    public GameObject house;    // The house to put at the end

	public float obstacleSpawnRate = 1;

	public bool spawnObjectsEnabled;	// Turn off for debugging

	// Speed stuff for Nigel
	public float targetSpeed;	// The speed at which to equalize to
	[Range(0, 1)]
	public float speedEqualizeFactor;	// How fast to equalize to the target speed

	public float CurrentSpeed{
		get{
			return m_currentSpeed;
		}
	}

	public Transform spawnLocation;	// Spawn location for items/obstacles

	// Speed changed event
	public delegate void OnSpeedChangeDelegate(float newSpeed);
	public event OnSpeedChangeDelegate onSpeedChanged;

	public static GameController instance;	// Singleton

    public float difficultyFactor = 0.0f;

	private float m_currentSpeed;
	
	private float sliderStartingDistance;	// Distance that Lizzy starts from Nigel (this is slider value 0)
	private SpawnableObject powerupToSpawn;	// Spawn this powerups this tick
	private Dictionary<string, List<SpawnableObject>> spawnableObjectsDict;

	private bool isGameOver = false;

    private float lastSpeedSet; // The last speed that was set
    private float lastObstacleSpawnRate;    // The last value that the obstacle spawn rate was set to


	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		initializeSpawnableObjectsDict();

		initializeSlider();
        lastObstacleSpawnRate = obstacleSpawnRate;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGameOver)
        {
            equalizeSpeed();
            updateSlider();
        }
	}

    // Start the game
    public void GameStart()
    {
        lizzy.GameStart();
        StartCoroutine(spawnObjects());

        // Move the main camera
        cam.GetComponent<MoveToLocation>().Move();

        // Start spawning powerups
        PowerupController.instance.GameStart();

        // Start switching stages
        StageController.instance.GameStart();
    }

    // The game was won!
    public void GameWin()
    {
        GameOver();
    }

	// Called by the powerupController
	// Tells the game controller to spawn the powerup soon
	public void SpawnPowerup(SpawnableObject powerup){
		powerupToSpawn = powerup;
	}

    public void AddSpeed(float speed)
    {
        SetSpeed(m_currentSpeed + speed);
    }

    public void SetSpeed(float newSpeed)
    {
        m_currentSpeed = newSpeed;
        if (m_currentSpeed < 0)
        {
            m_currentSpeed = 0;
        }
    }

    public void SetTargetSpeed(float newTargetSpeed)
    {
        lastSpeedSet = newTargetSpeed;
        newTargetSpeed *= (difficultyFactor + 1);

        if(newTargetSpeed < 0)
        {
            newTargetSpeed = 0;
        }

        targetSpeed = newTargetSpeed;
    }

    public void AddLizzyDistance(float distanceChange)
    {
        lizzy.GetComponent<Lizzy>().ChangeDistance(distanceChange);
    }

    // Function called when it's game over
    public void GameOver()
    {
        UIController.instance.ShowGameOver();

        targetSpeed = 0;
        m_currentSpeed = 0;
        onSpeedChanged(0);

        lizzy.GetComponent<Animator>().SetTrigger("idle");  // Idle animation for lizzy
        nigel.GetComponent<Animator>().SetTrigger("idle");  // Idle animation for nigel
    }

    // Called from UI to change the difficulty
    public void ChangeDifficultyFromUI()
    {
        difficultyFactor = UIController.instance.difficultySlider.value;
        targetSpeed = lastSpeedSet * (difficultyFactor + 1.0f);
        obstacleSpawnRate = lastObstacleSpawnRate - difficultyFactor / 2;
    }

    // Coroutine to periodically create an obstacle
    private IEnumerator spawnObjects(){
		while(true){
			// Gameover - stop spawning
			if(isGameOver){
				break;
			}

			if(powerupToSpawn != null){
				GameObject.Instantiate(powerupToSpawn, getObjectSpawnLocation(powerupToSpawn), Quaternion.identity);
				powerupToSpawn = null;
			}else if(spawnObjectsEnabled){
				// Create the obstacle
				SpawnableObject spawnableObject = getRandomObject();
				var obj = GameObject.Instantiate(spawnableObject, getObjectSpawnLocation(spawnableObject), Quaternion.identity);
				obj.transform.Rotate(Vector3.up, spawnableObject.spawnYRotation);	// Turn the object around
			}

			yield return new WaitForSeconds(obstacleSpawnRate);
		}
	}

	// Get a random obstacle from the known list of obstacles
	private SpawnableObject getRandomObject(){
		// Get the proper list
		SpawnableObject[] possibleObjects = spawnableObjectsDict[StageController.instance.stages[StageController.instance.currentStageIndex].stageID].ToArray();
		
		float currentWeight = 0;
		float[] itemNumbers = new float[possibleObjects.Length];

		// Assign the numbers based on weight
		for(int i = 0; i < possibleObjects.Length; i++){
			currentWeight += possibleObjects[i].spawnChance;
			itemNumbers[i] = currentWeight;
		}

		// Get the random number
		float randomNumber = Random.Range(0, currentWeight);

		// Find out who won
		for(int i = 0; i < itemNumbers.Length; i++){
			if(itemNumbers[i] > randomNumber){
				return possibleObjects[i];
			}
		}

		Debug.LogError("No random object found");
		return null;
	}

	// Get the spawn location for the obstacle
	private Vector3 getObjectSpawnLocation(SpawnableObject spawnableObject){
		return new Vector3(Random.Range(spawnableObject.spawnableLocation.x, spawnableObject.spawnableLocation.y), spawnableObject.spawnHeight, spawnLocation.position.z);
	}

	// Go towards the target speed
	private void equalizeSpeed(){
		m_currentSpeed = Mathf.Lerp(CurrentSpeed, targetSpeed, speedEqualizeFactor);
		onSpeedChanged(m_currentSpeed);
	}

	private void initializeSlider(){        
        // Set the slider distance
		sliderStartingDistance = lizzy.startingDistance;
		UIController.instance.ChangeLizzyDistanceSlider(0);
	}

	// Create the dictionary of spawnable objects
	// This is used to filter objects based on current level
	private void initializeSpawnableObjectsDict(){
		StageController sc = StageController.instance;

		spawnableObjectsDict = new Dictionary<string, List<SpawnableObject>>();
		for(int levelNumber = 0; levelNumber < sc.stages.Length; levelNumber++){
			spawnableObjectsDict.Add(sc.stages[levelNumber].stageID, new List<SpawnableObject>());
		}

		// Go through the spawnable objects and add to the proper dictionary lists
		foreach(SpawnableObject so in spawnableObjects){
			foreach(string levelID in so.activeLevels){
				// Stage isn't loaded, can't add this object
				if(!spawnableObjectsDict.ContainsKey(levelID)){
					continue;
				}
				spawnableObjectsDict[levelID].Add(so);
			}
		}
	}

	// Update the distance shown on the slider
	private void updateSlider(){
		// Calculate the distance between Lizzy and Nigel
		Vector3 diff = lizzy.transform.position - nigel.transform.position;
		float distance = diff.magnitude;
		distance = Mathf.Abs(distance);

		// Update the slider value
		float newValue = Mathf.Clamp(1 - (distance/sliderStartingDistance), 0f, 1.0f);
		UIController.instance.ChangeLizzyDistanceSlider(newValue);
	}

}
