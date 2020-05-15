//author: Ethan Rafael

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementPhys : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag
    private float turnAroundSpeed; //speed of turning around
    private float numAirJumps; //the amount of times the player can jump, where 1 is a single double jump
    private float jumpVelocity; // instantaneous velocity given to the player when they press jump
    private float gravity; //how fast the player accelerates downwards when velocity is upwards
    private float fallingGravity; //how fast the player accelerates downwards when velocity is downwards
    private float fallSpeedCap; // the fastest fall speed the player can go when they are not pressing down
    private float fastFallingGravity; // the speed at which the player accelerates downwards when fast fall is initiated
    private float fastFallSpeedCap; //how fast the player falls when they hold down 
    private float fastFallMinVel; //velocity the player must be moving vertically in order for fast fall to work
    private float rollDistance; //How far should a dodge move the player?
    private float rollDuration; //How many frames to complete the dodge?
    private float rollSlowFrames; //How many frames after the fast animation ends does the player stay in slow state
    private float rollSlowSpeedMult; //Multiplier for how fast the player moves during slow frames of a roll
    private float rollOutSpeedMult; //Speed mult after a roll
    private float rollCooldown; //frames after the roll ends before the player can roll again
    protected Rigidbody2D body;

    private HealthManager healthManager;
    public PlayerCombat PlayerCombat;

    public LayerMask whatIsGround;
    bool ableToJump; //needed for scenes other than MainGameplay
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
        numAirJumps = pc.getStat("numAirJumps");
        jumpVelocity = pc.getStat("jumpVelocity");
        gravity = pc.getStat("gravity") - 0.00001f;
        fallingGravity = pc.getStat("fallingGravity") - 0.00001f;
        fallSpeedCap = pc.getStat("fallSpeedCap") - 0.00001f;
        fastFallingGravity = pc.getStat("fastFallingGravity") - 0.00001f;
        fastFallSpeedCap = pc.getStat("fastFallSpeedCap") - 0.00001f;
        fastFallMinVel = pc.getStat("fastFallMinVel");
        rollDistance = pc.getStat("rollDistance");
        rollDuration = pc.getStat("rollDuration");
        rollSlowFrames = pc.getStat("rollSlowFrames");
        rollSlowSpeedMult = pc.getStat("rollSlowSpeedMult");
        rollOutSpeedMult = pc.getStat("rollOutSpeedMult");
        rollCooldown = pc.getStat("rollCooldown");
        deadzone = pc.getStat("movementDeadzone");

        ableToJump = GetComponent<JumpDisabler>() ? GetComponent<JumpDisabler>().ableToJump : true; //if the script is attached, set to value.                

        state = true; //flase is grounded, true is in the air

        healthManager = GetComponent<HealthManager>();

        controlFrozen = false;
        rollingFrame = 0;

        velocityVector = new Vector2(0, 0);
        remainingAirJumps = numAirJumps;

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") == true && !PlayerCombat.stop1 && ableToJump)
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
        //Debug.Log("actingGravity = " + actingGravity);
        //Debug.Log("vertical vel = " + velocityVector.y);
        //Debug.Log("fast falling = " + isFastFalling);
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
                if ((velocityVector.x < 0 && stickInput.x < 0) || (velocityVector.x > 0 && stickInput.x > 0))//check if it should use acceleration or deceleration
                {
                    velocityVector = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
                }
                else if ((velocityVector.x > 0 && stickInput.x < 0) || (velocityVector.x < 0 && stickInput.x > 0))
                {
                    //Debug.Log("Decelerate");
                    velocityVector = new Vector2((body.velocity + (stickInput * turnAroundSpeed)).x, body.velocity.y);
                }
                else
                {
                    velocityVector = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
                }

                //velocityVectorStorage = new Vector2((body.velocity + (stickInput * acceleration)).x, body.velocity.y);
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
            if (stickInput.magnitude == Vector2.zero.magnitude && controlFrozen == false)//if player is not pressing anything horizontal
            {
                if (velocityVector.x > 0)
                {
                    if (velocityVector.x - (deceleration) < 0)
                    {
                        velocityVector.x = 0;
                    }
                    else
                    {
                        velocityVector.x -= (deceleration);
                    }
                }
                if (velocityVector.x < 0)
                {
                    if (velocityVector.x + (deceleration) > 0)
                    {
                        velocityVector.x = 0;
                    }
                    else
                    {
                        velocityVector.x += (deceleration);
                    }
                }
            }

            //if(Input.GetKey(KeyCode.W) == true && velocityVector.y == 0)
            if (velocityVector.y == 0)
            {
                state = false;
                isFastFalling = false;
                remainingAirJumps = numAirJumps;
            }
            if (jumpButtonDown == true && (state == false || remainingAirJumps > 0))//if jump button is pressed and conditions are met, then jump (add double jump later)

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
                    GetComponent<Movement>().rollTime = true;
                }
                doRoll(Vector2.SignedAngle(Vector2.up, rollInput)); //calls roll with the angle (0 degrees is vertical)
            }

            if (isFastFalling == true)
            {
                actingGravity = fastFallingGravity;
            }
            else if (velocityVector.y < 0) //tells the player to fall at the speed of falling vs. ascending
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

            if (state == true && isFastFalling == true && velocityVector.y - actingGravity <= -fastFallSpeedCap)//if player is in the air and falling at terminal velocity and is fast falling
            {
                //Debug.Log("fastFall!");
                velocityVector.y = -fastFallSpeedCap;
            }
            else if (state == true && isFastFalling == false && velocityVector.y - actingGravity <= -fallSpeedCap)//if player is in the air and falling at terminal velocity and is not fast falling
            {
                //Debug.Log("1");
                velocityVector.y = -fallSpeedCap;
            }
            else if (state == false && gameObject.layer != 12)
            {
                //Debug.Log("2");
                gameObject.layer = 12;
            }
            else if (state == false && transform.localPosition.y > -25)
            {
                //Debug.Log("3");
                velocityVector.y -= fastFallSpeedCap / 25;
            }
            else if (state == true && isFastFalling == false && velocityVector.y - gravity > -fallSpeedCap)
            {
                //Debug.Log("4");
                velocityVector.y -= actingGravity;
            }
            else if (state == true && isFastFalling == true && velocityVector.y - gravity > -fastFallSpeedCap)
            {
                //Debug.Log("5");
                velocityVector.y -= actingGravity;
            }

            if (state == true && Input.GetAxis("Vertical") < -0.5f && velocityVector.y < fastFallMinVel)
            {
                isFastFalling = true;
            }

            if (state == true && Input.GetAxis("Vertical") < -0.5f)
            {
                gameObject.layer = 10;
            }
            else if (state == false && Input.GetAxis("Vertical") < -0.5f) //if player is grounded, give them a nudge downwards
            {
                velocityVector.y = -fallSpeedCap;
                gameObject.layer = 10;
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
        string sound = (remainingAirJumps > 0 && state == true) ? "playerJump1" : "playerJump2";
        SoundManager.PlaySound(sound);
        velocityVector.y = jumpVelocity;
        if (remainingAirJumps > 0 && state == true) //conditions for a double jump
        {
            GetComponent<Movement>().airJump();
            remainingAirJumps -= 1;
        }
        state = true;
    }

    void doRoll(float rollAngle)
    {
        if (rollingFrame < rollDuration) //roll in full speed frames
        {
            if (rollAngle < 0)
            {
                velocityVector.x = (rollDistance / rollDuration);
            }
            if (rollAngle > 0)
            {
                velocityVector.x = -(rollDistance / rollDuration);
            }

            rollingFrame++;
        }
        else if (rollingFrame < rollDuration + rollSlowFrames) //roll in slow frames
        {
            if (rollAngle < 0 && state == false)
            {
                velocityVector.x = (rollDistance / rollDuration) * rollSlowSpeedMult;
            }
            if (rollAngle > 0 && state == false)
            {
                velocityVector.x = -(rollDistance / rollDuration) * rollSlowSpeedMult;
            }
            rollingFrame++;
        }
        else if (rollingFrame == rollDuration + rollSlowFrames) //roll is over
        {
            if ((stickInput.x > 0 || stickInput.x < 0)) //if player is moving left or right
            {
                if (rollAngle < 0)
                {
                    velocityVector.x = (rollDistance / rollDuration) * rollOutSpeedMult;
                }
                if (rollAngle > 0)
                {
                    velocityVector.x = -(rollDistance / rollDuration) * rollOutSpeedMult;
                }
            }
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
