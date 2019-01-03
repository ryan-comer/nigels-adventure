using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Powerup))]
public class Catnip : MonoBehaviour {

	public float boostHorizontalSpeed;
	private float oldHorizontalSpeed;
	private Powerup powerup;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(-45, 0, 45);
		oldHorizontalSpeed = GameController.instance.nigel.startingHorizontalSpeed;

		// Setup the cancel delegate
		powerup = GetComponent<Powerup>();
		powerup.Cancel = cancel;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.tag == "nigel"){
			Nigel nigel = GameController.instance.nigel;
			nigel.isCatnipOn = true;
			nigel.horizontalSpeed = boostHorizontalSpeed;

			transform.position = Vector3.one * 1000;

			// Register the powerup with the powerup controller
			PowerupController.instance.RegisterPowerup(powerup);
		}
	}

	private void cancel(){
		GameController.instance.nigel.horizontalSpeed = oldHorizontalSpeed;
		GameController.instance.nigel.isCatnipOn = false;
		Destroy(gameObject);
	}

}
