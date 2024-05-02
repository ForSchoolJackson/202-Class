using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ImputController : MonoBehaviour
{
    [SerializeField]
    MovementController movementController;

    [SerializeField]
    Animator animator;

    //direction object facing
    Vector3 direction;
    Vector3 Direction
    {
        get { return direction; }
        //must normalize the direction vector
        set
        {
            direction = value.normalized;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //taken from other scrpit
        movementController.move = true;

        Direction = context.ReadValue<Vector2>();

        movementController.Direction = Direction;

        //update animation
        if(Direction.magnitude > 0 )
        {
            animator.SetBool("moving", true);

        }
        else
        {
            animator.SetBool("moving", false);
        }

       
    }
}