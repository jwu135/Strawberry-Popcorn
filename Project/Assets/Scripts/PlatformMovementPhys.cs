//author: Ethan Rafael

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementPhys : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag
    private float numAirJumps; //the amount of times the player can jump, where 1 is a single double jump
    private float jumpVelocity;
    private float gravity;
    private float fallingGravity;
    private float fallSpeedCap;
    private float rollDistance; //How far should a dodge move the player?
    private float rollDuration; //How many frames to complete the dodge?
    private float rollSlowFrames; //How many frames after the fast animation ends does the player stay in slow state
    private float rollSlowSpeedMult; //Multiplier for how fast the player moves during slow frames of a roll
    private float rollCooldown; //frames after the roll ends before the player can roll again
    protected Rigidbody2D body;

    private HealthManager healthManager;
    public PlayerCombat PlayerCombat;

    int state;
    float actingGravity; //the current gravity that is acting on the player. Changes to fallingGravity when y vel is < 0
    PlayerController pc;

    float deadzone; //joystick deadzone
    Vector2 stickInput;
    bool controlFrozen;
    int rollingFrame;
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
        numAirJumps = pc.getStat("numAirJumps");
        jumpVelocity = pc.getStat("jumpVelocity");
        gravity = pc.getStat("gravity") - 0.0001f;
        fallingGravity = pc.getStat("fallingGravity");
        fallSpeedCap = pc.getStat("fallSpeedCap");
        rollDistance = pc.getStat("rollDistance");
        rollDuration = pc.getStat("rollDuration");
        rollSlowFrames = pc.getStat("rollSlowFrames");
        rollSlowSpeedMult = pc.getStat("rollSlowSpeedMult");
        rollCooldown = pc.getStat("rollCooldown");
        deadzone = pc.getStat("movementDeadzone");

        state = 1; //0 is grounded, 1 is in the air

        healthManager = GetComponent<HealthManager>();

        controlFrozen = false;
        rollingFrame = 0;

        velocityVector = new Vector2(0, 0);
        remainingAirJumps = numAirJumps;

    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") == true && !PlayerCombat.stop1)
        {
            jumpButtonDown = true;
        } //workaround for disparity between update and fixedUpdate
    }

    // Fixed update is called 50 times per second, regardless of framerate (this can be changed in the project settings)
    private void FixedUpdate()
    {
        mode = pc.getMode();
        //Controls the movemement of the player when in flying mode
        if (mode == 1)
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
                //body.velocity = velocityVectorStorage;
                froze = false;
            }

            stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0); //gets the stick input and puts it in a vector
            if (stickInput.magnitude < deadzone)
            {
                stickInput = Vector2.zero;
            }

            if (controlFrozen == false)
            {
                velocityVector = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
                velocityVectorStorage = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
            }


            //clamp the move speed
            if (velocityVector.x > moveSpeed && controlFrozen == false)
            {
                velocityVector.x = moveSpeed;
            }
            else if (velocityVector.x < -moveSpeed)
            {
                velocityVector.x = -moveSpeed;
            }


            //first do velocity based on stick movement
            if (stickInput.magnitude == Vector2.zero.magnitude && controlFrozen == false)//if player is not pressing anything horizontal
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
            }

            //if(Input.GetKey(KeyCode.W) == true && velocityVector.y == 0)
            if (velocityVector.y == 0)
            {
                state = 0;
                remainingAirJumps = numAirJumps;
            }
            if (jumpButtonDown == true && (state == 0 || remainingAirJumps > 0))//if jump button is pressed and conditions are met, then jump (add double jump later)

            {
                jump();
            }

            if (((Input.GetButton("Roll") == true) || (Input.GetAxis("Roll") < 0)) && (rollingFrame == 0 && stickInput.magnitude > 0) || rollingFrame >= 1)
            {


                if (rollingFrame == 0)
                {
                    healthManager.manaCounter = 1;
                    controlFrozen = true;
                    rollInput = stickInput;
                    healthManager.invicibilityCounter = healthManager.invicibilityLength;
                }
                doRoll(Vector2.SignedAngle(Vector2.up, rollInput)); //calls roll with the angle (0 degrees is vertical)
            }

            if (velocityVector.y < 0) //tells the player to fall at the speed of falling vs. ascending
            {
                actingGravity = fallingGravity;
            }
            else
            {
                actingGravity = gravity;
            }

            if (velocityVector.y > 0 && Input.GetButton("Jump") == false)
            {
                velocityVector.y = velocityVector.y / 1.15f;
            }

            if (state == 1 && velocityVector.y - actingGravity <= -fallSpeedCap)//if player is in the air and falling at terminal velocity
            {
                velocityVector.y = -fallSpeedCap;
            }
            else if (state == 0 && transform.localPosition.y > -2.14)
            {
                velocityVector.y -= actingGravity/2;
            }
            else if (state == 1 && velocityVector.y - gravity > -fallSpeedCap)
            {
                velocityVector.y -= actingGravity;
            }

            body.velocity = velocityVector;
            //Debug.Log("JumpVel: " + velocityVector.y + "   state: " + state + "    Button: " + Input.GetButtonDown("Jump"));
            // Debug.Log(deadzone);

            jumpButtonDown = false; //should ALWAYS be the last line in fixedUpdate() part of the workaround mentioned in update();
        }
        else
        {
            
            //velocityVector.x = 0;
            //velocityVector.y = 0;
            velocityVector = new Vector2(0, 0);
            body.velocity = velocityVector;
            froze = true;
        }

    }

    void jump()
    {
        velocityVector.y = jumpVelocity;
        if (remainingAirJumps > 0 && state == 1) //conditions for a double jump
        {
            remainingAirJumps -= 1;
        }
        state = 1;
    }

    void doRoll(float rollAngle)
    {
        if (rollingFrame < rollDuration)
        {
            if( rollAngle < 0 )
            {
                velocityVector.x = (rollDistance / rollDuration);
            }
            if (rollAngle > 0)
            {
                velocityVector.x = -(rollDistance / rollDuration);
            }

            rollingFrame++;
        }
        else if (rollingFrame < rollDuration + rollSlowFrames)
        {
            if (rollAngle < 0)
            {
                velocityVector.x = (rollDistance / rollDuration) * rollSlowSpeedMult;
            }
            if (rollAngle > 0)
            {
                velocityVector.x = -(rollDistance / rollDuration) * rollSlowSpeedMult;
            }
            rollingFrame++;
        }
        else if (rollingFrame < rollDuration + rollSlowFrames + rollCooldown)
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
