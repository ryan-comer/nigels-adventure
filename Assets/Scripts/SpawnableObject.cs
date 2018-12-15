using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour {

	[Range(0, 1)]
	public float spawnChance;	// How likely is this object to spawn
	public  Vector2 spawnableLocation;	// What are the bounds of spawning for this object
	public float spawnHeight;	// At what height should this be spawned
	public string[] activeLevels;	// IDs of levels for when this object is active

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
