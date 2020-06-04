//author: Ethan Rafael

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightMovementPhys : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag
    private float turnAroundSpeed; //speed of turning around
    private float rollDistance; //How far should a dodge move the player?
    private float rollDuration; //How many frames to complete the dodge?
    private float rollSlowFrames; //How many frames after the fast animation ends does the player stay in slow state
    private float rollSlowSpeedMult; //Multiplier for how fast the player moves during slow frames of a roll
    private float rollOutSpeedMult; //Speed mult after a roll
    private float rollCooldown; //frames after the roll ends before the player can roll again
    protected Rigidbody2D body;

    private HealthManager healthManager;
    public PlayerCombat PlayerCombat;
    public bool unableToMove = false; //needed for scenes other than MainGameplay

    public LayerMask whatIsGround;
    bool state; //false is grounded
    bool isFastFalling = false;
    private bool rollOnCooldown = false;
    float actingGravity; //the current gravity that is acting on the player. Changes to fallingGravity when y vel is < 0
    PlayerController pc;

    float deadzone; //joystick deadzone
    Vector2 stickInput;
    bool controlFrozen;
    public int rollingFrame = 0;
    Vector2 rollInput = Vector2.zero;

    Vector2 velocityVector;
    Vector2 velocityVectorStorage;
    private bool froze = false;

    float remainingAirJumps;
    bool jumpButtonDown; //basically Input.GetButtonDown("Jump") but for fixed update instead of update

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
        turnAroundSpeed = pc.getStat("turnAroundSpeed");
        rollDistance = pc.getStat("rollDistance");
        rollDuration = pc.getStat("rollDuration");
        rollSlowFrames = pc.getStat("rollSlowFrames");
        rollSlowSpeedMult = pc.getStat("rollSlowSpeedMult");
        rollOutSpeedMult = pc.getStat("rollOutSpeedMult");
        rollCooldown = pc.getStat("rollCooldown");
        deadzone = pc.getStat("movementDeadzone");            

        healthManager = GetComponent<HealthManager>();

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
        if (!PlayerCombat.stop1)
        {
            if (froze)
            {
                velocityVector = new Vector2(1, 1);
                body.velocity = velocityVector;
                froze = false;
            }

            stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gets the stick input and puts it in a vector
            if (stickInput.magnitude < deadzone)
            {
                stickInput = Vector2.zero;
            }

            if (controlFrozen == false)
            {
                velocityVector = Vector2.ClampMagnitude(body.velocity + (stickInput * acceleration), moveSpeed);
                /*if ((velocityVector.x < 0 && stickInput.x < 0) || (velocityVector.x > 0 && stickInput.x > 0))//check if it should use acceleration or deceleration
                {
                    velocityVector = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
                }
                else if ((velocityVector.x > 0 && stickInput.x < 0) || (velocityVector.x < 0 && stickInput.x > 0))
                {
                    velocityVector = new Vector2((body.velocity + (stickInput * turnAroundSpeed)).x, body.velocity.y);
                }
                else
                {
                    velocityVector = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
                }*/
            }


            //clamp the move speed
            if (velocityVector.x > moveSpeed && controlFrozen == false)
            {
                velocityVector.x = moveSpeed;
            }
            else if (velocityVector.x < -moveSpeed && controlFrozen == false)
            {
                velocityVector.x = -moveSpeed;
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


                if (rollingFrame == 0)
                {
                    healthManager.manaCounter = 1;
                    controlFrozen = true;
                    rollInput = stickInput;
                    healthManager.invicibilityCounter = healthManager.invicibilityLength;
                    GetComponent<Movement>().rollTime = true;
                }
                doRoll(Vector2.SignedAngle(Vector2.up, rollInput)); //calls roll with the angle (0 degrees is vertical)
            }

            if (unableToMove)
                velocityVector.x = 0;
            body.velocity = velocityVector;
        }
        else
        {
            velocityVector = new Vector2(0, 0);
            body.velocity = velocityVector;
            froze = true;
        }

    }

    void doRoll(float rollAngle)
    {
        if (rollingFrame < rollDuration) //roll in full speed frames
        {
            velocityVector.x = -(rollDistance / rollDuration) * Mathf.Sin(Mathf.Deg2Rad * rollAngle);
            velocityVector.y = (rollDistance / rollDuration) * Mathf.Cos(Mathf.Deg2Rad * rollAngle);

            rollingFrame++;
        }
        else if (rollingFrame < rollDuration + rollSlowFrames) //roll in slow frames
        {
            velocityVector.x = -rollSlowSpeedMult * ((rollDistance / rollDuration) * Mathf.Sin(Mathf.Deg2Rad * rollAngle));
            velocityVector.y = rollSlowSpeedMult * ((rollDistance / rollDuration) * Mathf.Cos(Mathf.Deg2Rad * rollAngle));
            rollingFrame++;
        }
        else if (rollingFrame == rollDuration + rollSlowFrames) //roll is over
        {
            rollingFrame++;
        }
        else if (rollingFrame < rollDuration + rollSlowFrames + rollCooldown) //roll on cooldown
        {
            rollOnCooldown = true;
            controlFrozen = false;
            rollingFrame++;
        }
        else //roll reset
        {
            rollOnCooldown = false;
            rollingFrame = 0;
        }
    }

    public int getRollingFrame()
    {
        return rollingFrame;
    }
    public Vector2 getStickInput()
    {
        return stickInput;
    }
    public bool isRollOnCooldown()
    {
        return rollOnCooldown;
    }

}
