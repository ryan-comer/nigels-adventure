using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "nigel")
        {
            GameController.instance.GameWin();
        }
    }
}
