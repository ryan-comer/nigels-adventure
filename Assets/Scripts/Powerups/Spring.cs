using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Bug where the powerup runs out right after a jump
// The player can non-stop jump for some reason

[RequireComponent(typeof(Powerup))]
public class Spring : MonoBehaviour {

	private Powerup powerup;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(45, 0, 45);

		// Set the cancel delegate
		powerup = GetComponent<Powerup>();
		powerup.Cancel = cancel;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.gameObject.GetComponent<Nigel>() != null){
			Nigel nigel = GameController.instance.nigel;
			
			nigel.doubleJump = true;

			transform.position = new Vector3(1000, 1000, 1000);

			PowerupController.instance.RegisterPowerup(powerup);
		}
	}

	private void cancel(){
		// Cancel the powerup
		Nigel nigel = GameController.instance.nigel;
		
		nigel.doubleJump = false;

		Destroy(gameObject);
	}

}
