using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisOnCollision : MonoBehaviour {

	public string[] tagsToCollideWith;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		foreach(string tag in tagsToCollideWith){
			if(tag == collider.gameObject.tag){
				Destroy(gameObject);
			}
		}
	}

}
