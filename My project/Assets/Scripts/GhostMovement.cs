using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GhostMovement : MonoBehaviour
{
    Vector3 direction = Vector3.left;
    Vector3 velocity = Vector3.zero;
    Vector3 verticalVelocity = Vector3.up;

   [SerializeField]
    float speed = 5;

    float timeElapsed = 0f;
    float duration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get currect position
        Vector3 currentPos = transform.position;

        //get horizontal velocity
        Vector3 horizontalVelocity = direction * speed * Time.deltaTime;

       // movement();

        //calculate velocity
        velocity = horizontalVelocity;

        //add velocity to pos
        currentPos += velocity;

        //Get at position
        transform.position = currentPos;
       

    }

    //move ghosts up and down (NOT WORKING)
    void movement()
    {
        // Update time elapsed
        timeElapsed += Time.deltaTime;

        if (timeElapsed <= duration)
        {
            verticalVelocity = Vector3.up;
        }
        else
        {
            verticalVelocity = Vector3.down;
        }
    }

}
