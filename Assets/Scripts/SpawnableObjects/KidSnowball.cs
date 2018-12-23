using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSnowball : MonoBehaviour
{

    public float lifeTime = 5.0f;   // How long the snowball lasts
    public float moveSpeed = 1.0f;  // Speed of the snowball

    public int direction = 1;   // 1 - forward. -1 - backward

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    // Move the snowball
    private void move(){
        float newX = transform.position.x + (moveSpeed * Time.deltaTime * direction);
        Vector3 newPosition = new Vector3(newX, transform.position.y, transform.position.z);

        transform.position = newPosition;
    }

}
