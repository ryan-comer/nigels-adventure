using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	public string[] tagsToDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider collider){
		string tag = collider.gameObject.tag;

		// Check if the tag is in the list of tags to destroy
		foreach(string destroyTag in tagsToDestroy){
			if(destroyTag == tag){
				Destroy(collider.transform.root.gameObject);
			}
		}
	}

}
