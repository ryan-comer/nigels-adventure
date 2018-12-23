using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballKids : MonoBehaviour
{
    public GameObject[] kids;   // The kids throwing snowballs

    [Range(0, 1)]
    public float chancePerTick; // How likely a ball will be thrown
    public float tickFrequency; // How often the tick happens

    private int kidThrowing = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(throwSnowballRoutine());
    }

    // Coroutine to periodically throw snowballs
    private IEnumerator throwSnowballRoutine(){
        while(true){
            yield return new WaitForSeconds(tickFrequency);

            // Check if you can throw
            float val = Random.Range(0.0f, 1.0f);
            if(val < chancePerTick){
                throwSnowball();
            }
        }
    }

    // Throw a snowball
    private void throwSnowball(){
        GameObject kid = kids[kidThrowing];

        Animator anim = kid.GetComponent<Animator>();
        anim.SetTrigger("throw");

        kidThrowing = (kidThrowing + 1) % kids.Length;
    }

}
