using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverOnCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		if(collider.GetComponent<Nigel>() != null){
			GameController.instance.GameOver();
		}
	}

}
