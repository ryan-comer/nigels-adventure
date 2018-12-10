using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInPlace : MonoBehaviour {

	public float rotateSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rotate();
	}

	private void rotate(){
		transform.Rotate(Vector3.up, rotateSpeed*Time.deltaTime, Space.World);
	}
	
}
