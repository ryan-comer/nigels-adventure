using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{

    public Vector3 targetPosition;
    public Vector3 targetRotation;

    public float moveSpeed;

    // Move to the targets
    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    public IEnumerator MoveCoroutine()
    {
        while (true)
        {
            // Position
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Rotation
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, targetRotation, moveSpeed * Time.deltaTime));

            // Check threshold
            if(Vector3.Distance(transform.position, targetPosition) < 0.05f && Vector3.Distance(transform.rotation.eulerAngles, targetRotation) < 0.05f)
            {
                break;
            }

            yield return new WaitForSeconds(0);
        }
    }

}
