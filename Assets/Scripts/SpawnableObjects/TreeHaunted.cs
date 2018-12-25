using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHaunted : MonoBehaviour
{

    public Vector2 fallRange;   // The range of Z values that the tree can fall

    private float zFall;    // What z value that the tree will fall

    // Start is called before the first frame update
    void Start()
    {
        zFall = Random.Range(fallRange.x, fallRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        checkFall();
    }

    // Check if the tree should fall
    private void checkFall(){
        if(transform.position.z < zFall){
            GetComponent<Animator>().SetTrigger("fall");
        }
    }
}
