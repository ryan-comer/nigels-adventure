using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : MonoBehaviour
{

    public Vector2 bounds;  // How far the camel moves
    public float moveSpeed; // How fast the camel moves

    private int direction = 1;  // Which direction the camel is moving.  1-Right -1-Left

    // Update is called once per frame
    void Update()
    {
        checkChangeDirection();
        move();
    }

    // See if the camel needs to change direction
    private void checkChangeDirection(){
        if(transform.position.x > bounds.y){
			direction = -1;
            transform.eulerAngles = new Vector3(0, -90, 0);
		}else if(transform.position.x < bounds.x){
			direction = 1;
            transform.eulerAngles = new Vector3(0, 90, 0);
		}
    }

    // Move the camel over time
    private void move(){
        float newX = transform.position.x + moveSpeed*Time.deltaTime*direction;
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

}
