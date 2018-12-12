using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	// UI Components
	public Text gameOverText;
	public Text scoreText;
	public Text multiplierText;
	public Slider distanceSlider;
	public RectTransform powerupsGroup;	// Group for active powerups to go

	public RectTransform powerupUI_p;

	public static UIController instance;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		gameOverText.gameObject.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScoreText(int newScore){
		scoreText.text = "Score: " + newScore.ToString();
	}

	public void ChangeMultiplierText(float newMultiplier){
		multiplierText.text = "Multiplier: " + (int)newMultiplier + "X";
	}

	public void ChangeLizzyDistanceSlider(float newValue){
		distanceSlider.value = newValue;
	}

	public void ShowGameOver(){
		gameOverText.gameObject.SetActive(true);
	}

	// Create a powerup icon and timer
	public void CreatePowerupUI(Powerup powerup){
		RectTransform newPowerupUI = Instantiate(powerupUI_p, powerupsGroup);
		
		PowerupUI ui = newPowerupUI.GetComponent<PowerupUI>();
		ui.powerupID = powerup.powerupID;
		ui.powerupImage.sprite = powerup.powerupImage;
		ui.maxTime = powerup.duration;
	}

	// Destroy the powerup UI element
	public void RemovePowerupUI(string powerupID){
		// Find the UI element
		foreach(RectTransform rt in powerupsGroup){
			PowerupUI powerupUI = rt.GetComponent<PowerupUI>();

			// Found, now destroy
			if(powerupUI.powerupID == powerupID){
				Destroy(rt.gameObject);
			}
		}
	}

}
