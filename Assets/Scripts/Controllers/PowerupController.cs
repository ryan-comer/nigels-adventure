using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour {

	public float powerupSpawnRate;	// The rate at which to spawn powerups
	public SpawnableObject[] powerups;

	// Use this for initialization
	void Start () {
		StartCoroutine(spawnPowerup());
	}
	
	// Update is called once per frame
	void Update () {
		
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
