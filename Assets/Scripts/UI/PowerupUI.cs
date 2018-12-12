using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PowerupUI : MonoBehaviour {

	public Image powerupImage;
	public Slider timeLeftSlider;
	public string powerupID;
	public float maxTime;	// The maximum duration for this 

	// Use this for initialization
	void Start () {
		timeLeftSlider.value = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		updateTimeLeft();
	}

	// Check how much time is left on the powerup
	// Update the UI
	private void updateTimeLeft(){
		float timeLeft = PowerupController.instance.getTimer(powerupID);
		timeLeftSlider.value = Mathf.Clamp(timeLeft / maxTime, 0.0f, 1.0f);
	}

}
