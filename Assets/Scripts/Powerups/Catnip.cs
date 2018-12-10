using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour {

	public float boostSpeed;
	public float duration;

	private float oldSpeed;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(-45, 0, 45);
		oldSpeed = GameController.instance.targetSpeed;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider){
		// Hit Nigel - do the powerup
		if(collider.gameObject.GetComponent<Nigel>() != null){
			GameController.instance.targetSpeed = boostSpeed;
			transform.position = Vector3.one * 1000;

			StartCoroutine(cancel());
		}
	}

	private IEnumerator cancel(){
		yield return new WaitForSeconds(duration);

		GameController.instance.targetSpeed = oldSpeed;
		Destroy(gameObject);
	}

}
