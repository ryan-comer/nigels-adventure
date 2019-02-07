using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController instance;
    public AudioSource audioSource; //Source for background music
    public AudioListener audioListener; // Main listener

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Play the audio clip as background music
    public void PlayBackgroundMusic(AudioClip audioClip)
    {
        audioSource.clip = audioClip;   // Set the clip
        audioSource.Play(); // Play the clip
    }

    // Change the volume for the game
    public void ChangeVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    // Called from UI elements
    public void ChangeVolumeFromUI()
    {
        ChangeVolume(UIController.instance.musicVolumeSlider.value);
    }

}
