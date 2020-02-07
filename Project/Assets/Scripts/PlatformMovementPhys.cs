using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementPhys : MonoBehaviour
{
    private float moveSpeed; //the maximum movement speed
    private float acceleration; //the multiplier for speeding up
    private float deceleration; //the coefficient of drag
    private float jumpVelocity;
    private float gravity;
    private float fallingGravity;
    private float fallSpeedCap;
    protected Rigidbody2D body;

    int state;
    float actingGravity; //the current gravity that is acting on the player. Changes to fallingGravity when y vel is < 0
    PlayerController pc;

    float deadzone; //joystick deadzone
    Vector2 stickInput;

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
        jumpVelocity = pc.getStat("jumpVelocity");
        gravity = pc.getStat("gravity");
        fallingGravity = pc.getStat("fallingGravity");
        fallSpeedCap = pc.getStat("fallSpeedCap");

        state = 1; //0 is grounded, 1 is in the air
        


        deadzone = 0.0007f;

        velocityVector = new Vector2(0, 0);

        mode = pc.getMode();
    }

    // Fixed update is called 50 times per second, regardless of framerate (this can be changed in the project settings)
    private void FixedUpdate()
    {
        //Controls the movemement of the player when in flying mode
        if (mode == 1)
        {
            doMovement();
        }

    }

    void doMovement()
    {
        stickInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0); //gets the stick input and puts it in a vector
        if (stickInput.magnitude < deadzone)
        {
            stickInput = Vector2.zero;
        }

        velocityVector = new Vector2( (body.velocity + (stickInput * acceleration)).x, body.velocity.y );


        //clamp the move speed
        if(velocityVector.x > moveSpeed)
        {
            velocityVector.x = moveSpeed;
        }
        else if (velocityVector.x < -moveSpeed)
        {
            velocityVector.x = -moveSpeed;
        }


        //first do velocity based on stick movement
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
        }

        if(Input.GetButton("Jump") == true && velocityVector.y == 0)//if jump button is pressed and conditions are met, then jump (add double jump later)
        {
            jump();
        }

        if(velocityVector.y < 0)
        {
            actingGravity = fallingGravity;
        }
        else
        {
            actingGravity = gravity;
        }

        if(state == 1 && velocityVector.y - actingGravity <= -fallSpeedCap )//if player is in the air and falling at terminal velocity
        {
            velocityVector.y = -fallSpeedCap;
        }
        else if(state == 1 && velocityVector.y - gravity > -fallSpeedCap)
        {
            velocityVector.y -= actingGravity;
        }

        body.velocity = velocityVector;
    }

    void jump()
    {
        velocityVector.y = jumpVelocity;
    }
}
