using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour {

	public float rotateSpeed;

	public float boostSpeed;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(-45, 0, 45);
	}
	
	// Update is called once per frame
	void Update () {
		rotate();
	}

	private void rotate(){
		transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.gameObject.GetComponent<Nigel>() != null){
			GameController.instance.targetSpeed = boostSpeed;
			Destroy(gameObject);
		}
	}

}
