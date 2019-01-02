using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCharmer : MonoBehaviour
{

    public Vector2 timeOpen;  // How long does the lid stay open (random between 2)
    public Vector2 timeClosed;    // How long the lid is closed (random between 2)

    public Animator snakeAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(snakeCoroutine());
    }
    
    private IEnumerator snakeCoroutine(){
        while(true){
            yield return new WaitForSeconds(Random.Range(timeClosed.x, timeClosed.y));

            snakeAnim.SetTrigger("out");

            yield return new WaitForSeconds(Random.Range(timeOpen.x, timeOpen.y));

            snakeAnim.SetTrigger("in");
        }
    }

}
