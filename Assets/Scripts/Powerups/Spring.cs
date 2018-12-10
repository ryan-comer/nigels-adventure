using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	public float newJumpHeight;
	public float newGravity;
	public float duration;

	private float oldGravity, oldJumpHeight;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(45, 0, 45);
		oldGravity = GameController.instance.nigel.gravity;
		oldJumpHeight = GameController.instance.nigel.jumpHeight;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.gameObject.GetComponent<Nigel>() != null){
			Nigel nigel = GameController.instance.nigel;
			
			oldJumpHeight = nigel.jumpHeight;
			oldGravity = nigel.gravity;

			nigel.jumpHeight = newJumpHeight;
			nigel.gravity = newGravity;

			transform.position = new Vector3(1000, 1000, 1000);
			StartCoroutine(cancel());
		}
	}

	private IEnumerator cancel(){
		yield return new WaitForSeconds(duration);

		// Cancel the powerup
		Nigel nigel = GameController.instance.nigel;

		nigel.gravity = oldGravity;
		nigel.jumpHeight = oldJumpHeight;
		Destroy(gameObject);
	}

}
