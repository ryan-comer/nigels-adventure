using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup : MonoBehaviour {

	public Sprite powerupImage;
	public string powerupID;
	public float duration;	// How long the powerup lasts
	public delegate void CancelDelegate();	// Delegate to cancel the powerup
	public CancelDelegate Cancel;
	
}
