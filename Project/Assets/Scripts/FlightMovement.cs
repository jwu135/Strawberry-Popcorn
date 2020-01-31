using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovement : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag

    PlayerController pc;

    float deadzone; //joystick deadzone
    Vector2 stickInput;

    Vector2 velocityVector;

    float remainingJumps;

    int mode; //0 for flight, 1 for platforming

    //start is called upon the creation of any instance this script is attached to.
    void Start()
    {
        pc = GetComponent<PlayerController>();
        moveSpeed = pc.getStat("moveSpeed");
        acceleration = pc.getStat("acceleration");
        deceleration = pc.getStat("deceleration");

        deadzone = 0.0007f;

        velocityVector = new Vector2(0, 0);

        mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Controls the movemement of the player when in flying mode
        if (mode == 0)
        {
            doMovement();
        }

    }

    void doMovement()
    {
        stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (stickInput.magnitude < deadzone)
        {
            stickInput = Vector2.zero;
        }

        velocityVector = Vector2.ClampMagnitude(velocityVector + (stickInput * acceleration), moveSpeed);


        //test for these two things separate
        if (stickInput.magnitude == Vector2.zero.magnitude)//if player is not pressing anything horizontal
        {
            if (velocityVector.x > 0)
            {
                if (velocityVector.x - deceleration < 0)
                {
                    velocityVector.x = 0;
                }
                else
                {
                    velocityVector.x -= deceleration;
                }
            }
            if (velocityVector.x < 0)
            {
                if (velocityVector.x + deceleration > 0)
                {
                    velocityVector.x = 0;
                }
                else
                {
                    velocityVector.x += deceleration;
                }
            }

            if (velocityVector.y > 0)
            {
                if (velocityVector.y - deceleration < 0)
                {
                    velocityVector.y = 0;
                }
                else
                {
                    velocityVector.y -= deceleration;
                }
            }
            if (velocityVector.y < 0)
            {
                if (velocityVector.y + deceleration > 0)
                {
                    velocityVector.y = 0;
                }
                else
                {
                    velocityVector.y += deceleration;
                }
            }
        }

        Vector3 velocityVector3 = velocityVector;

        transform.position = transform.position + velocityVector3 * Time.deltaTime; //CURRENTLY TIED TO FRAMERATE CHANGE IN A BIT
    }
}
