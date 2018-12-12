using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Powerup))]
public class Spring : MonoBehaviour {

	public float newJumpHeight;
	public float newGravity;
	private float oldGravity, oldJumpHeight;

	private Powerup powerup;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(45, 0, 45);
		oldGravity = GameController.instance.nigel.startingGravity;
		oldJumpHeight = GameController.instance.nigel.startingJumpHeight;

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
			
			oldJumpHeight = nigel.startingJumpHeight;
			oldGravity = nigel.startingGravity;

			nigel.jumpHeight = newJumpHeight;
			nigel.gravity = newGravity;

			transform.position = new Vector3(1000, 1000, 1000);

			PowerupController.instance.RegisterPowerup(powerup);
		}
	}

	private void cancel(){
		// Cancel the powerup
		Nigel nigel = GameController.instance.nigel;

		nigel.gravity = oldGravity;
		nigel.jumpHeight = oldJumpHeight;
		Destroy(gameObject);
	}

}
