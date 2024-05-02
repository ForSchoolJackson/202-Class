using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    Vector3 objectPos = Vector3.zero;

    Vector3 direction = Vector3.up;

    Vector3 velocity = Vector3.zero;

    [SerializeField]
    float speed;

    [SerializeField]
    Camera camera;

    [SerializeField]
    public bool move = false;

    [SerializeField]
    GameObject arm;


    public Vector3 Direction
    {
        set { direction = value.normalized; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //start it at the correct place
        objectPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float camWidth = camera.orthographicSize * camera.aspect;
        float camHeight = camera.orthographicSize;

        //move only if input is being used
        if (move)
        {

            //get arm postion
            Vector3 armPos = arm.transform.position;

            //flip character
            if (direction.x > 0)
            {
                transform.rotation = Quaternion.LookRotation(-Vector3.back, Vector3.up);

                //fix arm
                armPos.z = 10f;
                arm.transform.position= armPos;
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);

                //fix arm
                armPos.z = 3f;
                arm.transform.position = armPos;
            }


             //calculate velocity
             velocity = direction * speed * Time.deltaTime;

            //add velocity to pos
            objectPos += velocity;

            //Get at position
            transform.position = objectPos;

            //if off screen in x
            if (objectPos.x >= camWidth)
            {
                objectPos.x = camWidth;
            }
            else if (objectPos.x <= -camWidth)
            {
                objectPos.x = -camWidth;
            }

            //if off screen in y
            if (objectPos.y >= camHeight)
            {
                objectPos.y = camHeight;
            }
            else if (objectPos.y <= -camHeight)
            {
                objectPos.y = -camHeight;
            }
        }




    }
}
