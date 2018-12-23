using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{

    public GameObject snowball_p;   // Prefab for teh snowball that will be thrown
    public Transform spawnLocation; // The location for snowballs to spawn

    public int direction;   // The direction the kid is facing

    public void CreateSnowball(){
        var snowball = Instantiate(snowball_p, spawnLocation.position, Quaternion.identity);
        snowball.GetComponent<KidSnowball>().direction = direction;
    }

}
