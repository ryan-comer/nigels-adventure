using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{

    public float jumpRate;  // How often to jump
    public Vector2 xRange;  // The range of X that the wolf can jump

    public float jumpHeight;    // How high to jump
    public float jumpDistance;  // How far to jump
    public float xLerpFactor;
    public float gravity;
    public int direction = -1;  // Which way the wolf is jumping

    [SerializeField]
    private float currentTargetX;  // Where the wolf is landing
    private float currentYVelocity;
    private bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        currentTargetX = transform.position.x;
        StartCoroutine(jumpCoroutine());
    }

    void Update(){
        moveTowardsTarget();
        updateY();
    }

    public void Jump(){
        // Get a new X Position
        currentTargetX = getNewTargetX();

        currentYVelocity = jumpHeight;
        grounded = false;
    }

    // Periodically jump to a target
    private IEnumerator jumpCoroutine(){
        while(true){
            yield return new WaitForSeconds(jumpRate);

            startJumpAnimation();
        }
    }

    private void startJumpAnimation(){
        GetComponent<Animator>().SetTrigger("jump");    // Trigger the jump animation
    }

    // Slowly get closer to target
    private void moveTowardsTarget(){
        float newX = transform.position.x;
        newX = Mathf.Lerp(newX, currentTargetX, xLerpFactor*Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    // Fall over time
    private void updateY(){
        // Grounded, no need to update Y
        if(grounded){
            return;
        }

        float newY = transform.position.y;
        newY += currentYVelocity*Time.deltaTime;
        
        if(newY < 0){
            if(!grounded){
                GetComponent<Animator>().SetTrigger("land");
                grounded = true;
            }
            currentYVelocity = 0;
            newY = 0.0f;
        }

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        currentYVelocity += gravity * Time.deltaTime;
    }

    private float getNewTargetX(){
        float newTargetX = transform.position.x;
        newTargetX += jumpDistance * direction;

        // Check the bounds
        // Turn right
        if(newTargetX < xRange.x){
            direction *= -1;
            transform.Rotate(Vector3.up, 180.0f);
            newTargetX = transform.position.x + jumpDistance*direction;
        }
        // Turn left
        else if(newTargetX > xRange.y){
            direction *= -1;
            transform.Rotate(Vector3.up, 180.0f);  
            newTargetX = transform.position.x + jumpDistance*direction;          
        }

        return newTargetX;
    }

}
