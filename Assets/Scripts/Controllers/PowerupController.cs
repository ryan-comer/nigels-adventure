using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Manage the powerups here (central place) so refreshing the powerup works
// TODO: Getting a powerup should register a cancel function under a powerup ID
// TODO: Have a timer for each powerup

public class PowerupController : MonoBehaviour {

	public float powerupSpawnRate;	// The rate at which to spawn powerups
	public SpawnableObject[] powerups;

	public static PowerupController instance;

	private Dictionary<string, float> powerupTimers = new Dictionary<string, float>();	// Timers for each powerup
	private Dictionary<string, Powerup.CancelDelegate> powerupCancels = new Dictionary<string, Powerup.CancelDelegate>();	// Delegates for canceling powerups when they run out

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnPowerup());
	}
	
	// Update is called once per frame
	void Update () {
		tickTimers();
	}

	// Register a new powerup with the controller
	// The player just go this powerup
	public void RegisterPowerup(Powerup powerup){
		// Key not in dict - initialize
		if(!powerupTimers.ContainsKey(powerup.powerupID)){
			powerupTimers.Add(powerup.powerupID, powerup.duration);
			powerupCancels.Add(powerup.powerupID, powerup.Cancel);

			// Create the UI
			UIController.instance.CreatePowerupUI(powerup);
		}
		// Key is in dict - reset timer
		else{
			powerupTimers[powerup.powerupID] = powerup.duration;
		}
	}

	public float getTimer(string powerupID){
		return powerupTimers[powerupID];
	}

	// Decrement all of the timers for the powerups
	private void tickTimers(){
		List<string> powerupsToDestroy = new List<string>();
		// Go through each timer
		var keys = new List<string>(powerupTimers.Keys);
		foreach (string powerupID in keys){
			float newTime = powerupTimers[powerupID] - Time.deltaTime;
			newTime = Mathf.Max(0.0f, newTime);	// Don't go below 0
			powerupTimers[powerupID] = newTime;

			// If a timer is 0, run the cancel and remove the UI
			if(newTime == 0.0f){
				powerupsToDestroy.Add(powerupID);
			}
		}

		// Remove finished powerups
		foreach(string powerupID in powerupsToDestroy){
			powerupCancels[powerupID]();	// Execute the cancel delegate
			UIController.instance.RemovePowerupUI(powerupID);

			// Remove the powerup from the dictionaries
			powerupTimers.Remove(powerupID);
			powerupCancels.Remove(powerupID);
		}
	}

	// Periodically tell the gamecontroller to spawn powerups
	private IEnumerator spawnPowerup(){
		yield return new WaitForSeconds(powerupSpawnRate);

		while(true){
			GameController.instance.SpawnPowerup(getRandomPowerup());

			yield return new WaitForSeconds(powerupSpawnRate);
		}
	}

	// Returns a random powerup from the list of powerups
	private SpawnableObject getRandomPowerup(){
		return powerups[Random.Range(0, powerups.Length)];
	}

}
