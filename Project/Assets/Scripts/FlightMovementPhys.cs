//Author: Ethan Rafael

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovementPhys : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag
    private float rollDistance; //How far should a dodge move the player?
    private float rollDuration; //How many frames to complete the dodge?
    private float rollSlowFrames; //How many frames after the fast animation ends does the player stay in slow state
    private float rollSlowSpeedMult; //Multiplier for how fast the player moves during slow frames of a roll
    private float rollCooldown; //frames after the roll ends before the player can roll again

    protected Rigidbody2D body;

    PlayerController pc;

    float deadzone; //joystick deadzone
    Vector2 stickInput;
    bool controlFrozen;
    int rollingFrame;
    Vector2 rollInput = Vector2.zero;

    Vector2 velocityVector;

    float remainingJumps;

    int mode; //0 for flight, 1 for platforming

    //start is called upon the creation of any instance this script is attached to.
    private void Start()
    {
        initMode();
    }
    public void initMode()
    {
        pc = GetComponent<PlayerController>();
        body = GetComponent<Rigidbody2D>();
        moveSpeed = pc.getStat("moveSpeed");
        acceleration = pc.getStat("acceleration");
        deceleration = pc.getStat("deceleration");
        rollDistance = pc.getStat("rollDistance");
        rollDuration = pc.getStat("rollDuration");
        rollSlowFrames = pc.getStat("rollSlowFrames");
        rollSlowSpeedMult = pc.getStat("rollSlowSpeedMult");
        rollCooldown = pc.getStat("rollCooldown");
        deadzone = pc.getStat("movementDeadzone");

        controlFrozen = false;
        rollingFrame = 0;

        velocityVector = new Vector2(0, 0);
    }

    // Fixed update is called 50 times per second, regardless of framerate (this can be changed in the project settings)
    private void FixedUpdate()
    {
        mode = pc.getMode();
        //Controls the movemement of the player when in flying mode
        if (mode == 0)
        {
            doMovement();
        }

    }

    void doMovement()
    {
        stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gets the stick input and puts it in a vector
        if (stickInput.magnitude < deadzone)
        {
            stickInput = Vector2.zero;
        }

        if(controlFrozen == false) //as long as the player isn't rolling
        {
            velocityVector = Vector2.ClampMagnitude(body.velocity + (stickInput * acceleration), moveSpeed);
        }


        //first do velocity based on stick movement
        if (stickInput.magnitude == Vector2.zero.magnitude && controlFrozen == false)//if player is not pressing anything horizontal and the player isn't rolling

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



        if (((Input.GetButton("Roll") == true) || (Input.GetAxis("Roll") < 0)) && (rollingFrame == 0 && stickInput.magnitude > 0) || rollingFrame >= 1)
        {
            if(rollingFrame == 0)
            {
                controlFrozen = true;
                rollInput = stickInput;
            }
            doRoll(Vector2.SignedAngle(Vector2.right, rollInput)); //calls roll with the angle (0 degrees is right)
        }

        body.velocity = velocityVector;
    }
    void doRoll(float rollAngle)
    {
        if(rollingFrame < rollDuration)
        {
            velocityVector.x = (rollDistance / rollDuration) * Mathf.Cos(Mathf.Deg2Rad * rollAngle);
            velocityVector.y = (rollDistance / rollDuration) * Mathf.Sin(Mathf.Deg2Rad * rollAngle);

            rollingFrame++;
        }
        else if( rollingFrame < rollDuration + rollSlowFrames)
        {
            velocityVector.x = rollSlowSpeedMult * ((rollDistance / rollDuration) * Mathf.Cos(Mathf.Deg2Rad * rollAngle));
            velocityVector.y = rollSlowSpeedMult * ((rollDistance / rollDuration) * Mathf.Sin(Mathf.Deg2Rad * rollAngle));
            rollingFrame++;
        }
        else if( rollingFrame < rollDuration + rollSlowFrames + rollCooldown)
        {
            controlFrozen = false;
            rollingFrame++;
        }
        else
        {
            rollingFrame = 0;
        }
    }
}

