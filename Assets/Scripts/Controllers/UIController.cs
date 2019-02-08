using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	// UI Components
	public Image gameOverText;
    public Image victoryText;
	public Text scoreText;
	public Text multiplierText;
	public Slider distanceSlider;
	public RectTransform powerupsGroup;	// Group for active powerups to go
    public Button startGameButton;  // Button to start the game
    public Image titleImage;    // The title of the game
    public RectTransform gameInfoBackground; // Background for game info
    public RectTransform pauseScreen;   // The pause menu
    public Slider musicVolumeSlider;    // The slider for music volume
    public Slider difficultySlider; // The slider for difficulty

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
        checkInput();
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

    // Start the game
    public void StartGame()
    {
        GameController.instance.GameStart();

        // Hide stuff
        startGameButton.gameObject.SetActive(false);
        titleImage.gameObject.SetActive(false);

        // Show the rest of the UI
        scoreText.gameObject.SetActive(true);
        multiplierText.gameObject.SetActive(true);
        distanceSlider.gameObject.SetActive(true);
        gameInfoBackground.gameObject.SetActive(true);
    }

	public void ShowGameOver(){
		gameOverText.gameObject.SetActive(true);
	}

    public void ShowVictory()
    {
        victoryText.gameObject.SetActive(true);
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

    // Turn the pause screen on/off
    public void TogglePauseScreen()
    {
        pauseScreen.gameObject.SetActive(!pauseScreen.gameObject.activeSelf);

        // Pause the game
        if (pauseScreen.gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    // Handle input
    private void checkInput()
    {
        // Pause screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseScreen();
        }
    }

}
