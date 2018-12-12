using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour {

	public int currentScore = 0;
	public float currentMultiplier = 0;	// How many points in a row has the player received
	public int maxMultiplierPoints;	// Max points to get that gives you max multiplier
	public float maxMultplier;	// The maximum multiplier you can get
	public AnimationCurve multiplierCurve;	// Curve that defines what multiplier you get with your # consecutive points
	public static PointsController instance;

	private int consecutivePoints;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		ChangeScore(0);
		ChangeMultiplier(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeScore(int newScore){
		currentScore = newScore;

		// Update UI
		UIController.instance.ChangeScoreText(newScore);
	}

	public void ChangeMultiplier(float newMultiplier){
		currentMultiplier = newMultiplier;

		// Update the UI
		UIController.instance.ChangeMultiplierText(newMultiplier);
	}

	// Add points to the player's score
	// Take the multiplier into account
	public void AddPoints(int pointsToAdd){
		consecutivePoints += pointsToAdd;
		currentScore += (int)(pointsToAdd*(int)currentMultiplier);

		// Update the multiplier
		currentMultiplier = multiplierCurve.Evaluate(Mathf.Min((float)consecutivePoints/(float)maxMultiplierPoints, 1.0f)) * maxMultplier;
		currentMultiplier += 1;	// Base value is 1

		// Update the UI
		UIController.instance.ChangeScoreText(currentScore);
		UIController.instance.ChangeMultiplierText(currentMultiplier);
	}

	// The player missed a point
	public void PointMissed(){
		currentMultiplier = 1;	// Reset the multiplier
		consecutivePoints = 0;

		// Update the UI
		UIController.instance.ChangeMultiplierText(currentMultiplier);
	}

}
