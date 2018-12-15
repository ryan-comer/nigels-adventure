using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomXSpawn : MonoBehaviour {

	public Vector2 randomXValues;

	// Use this for initialization
	void Start () {
		setXValue();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void setXValue(){
		float newX = Random.Range(randomXValues.x, randomXValues.y);
		Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);

		transform.position = newPosition;
	}

}
