using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Powerup))]
public class Catnip : MonoBehaviour {

	public float boostSpeed;
	private float oldSpeed;
	private Powerup powerup;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(-45, 0, 45);
		oldSpeed = GameController.instance.startingSpeed;

		// Setup the cancel delegate
		powerup = GetComponent<Powerup>();
		powerup.Cancel = cancel;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.gameObject.GetComponent<Nigel>() != null){
			GameController.instance.targetSpeed = boostSpeed;
			collider.gameObject.GetComponent<Nigel>().isCatnipOn = true;

			transform.position = Vector3.one * 1000;

			// Register the powerup with the powerup controller
			PowerupController.instance.RegisterPowerup(powerup);
		}
	}

	private void cancel(){

		GameController.instance.targetSpeed = oldSpeed;
		GameController.instance.nigel.isCatnipOn = false;
		Destroy(gameObject);
	}

}
