using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //a "frame" is a unit of time that is defined in Edit->Project Settings->Time->Fixed Timestep
    //this does NOT IN ANY WAY correlate to the framerate that the user is getting
    public float moveSpeed; //the maximum movement speed
    public float acceleration; //the multiplier for speeding up
    public float deceleration; //the coefficient of drag
    public float numJumps; //the amount of times the player can jump before touching the ground again
    public float jumpVelocity; //the initial speed the player leaves the ground upon pressing jump
    public float gravity; //the amount of velocity removed from the players initial jump velocity until...
    public float fallingGravity; //effect of gravity while falling (should be higher than regular gravity)
    public float fallSpeedCap; //it reaches the fall speed cap
    public float rollDistance; //How far should a dodge move the player?
    public float rollDuration; //How many frames should the dodge take
    public float rollSlowFrames; //How many frames after the fast animation ends does the player stay in slow state
    public float rollSlowSpeedMult; //Multiplier for how fast the player moves during slow frames of a roll
    public float rollCooldown; //frames after the roll ends before the player can roll again
    public int mode;
    Rigidbody2D body;
    FlightMovementPhys mode0;
    PlatformMovementPhys mode1;


    private void Start()
    {
        mode0 = GetComponent<FlightMovementPhys>();
        mode1 = GetComponent<PlatformMovementPhys>();
        body = GetComponent<Rigidbody2D>();
        body.interpolation = RigidbodyInterpolation2D.Interpolate;
        setMode(mode);
    }
    // Start is called before the first frame update
    public void setMode(int modeSet)// 0 for flight 1 for platforming
    {
        mode = modeSet;
        if(modeSet == 0)
        {
            mode0.initMode();
        }
        else if(modeSet == 1)
        {
            mode1.initMode();
        }
    }

    public int getMode()
    {
        return mode;
    }

    public float getStat(string stat)
    {
        if( string.Equals(stat, "moveSpeed"))
        {
            return moveSpeed;
        }
        else if (string.Equals(stat, "acceleration"))
        {
            return acceleration;
        }
        else if (string.Equals(stat, "deceleration"))
        {
            return deceleration;
        }
        else if (string.Equals(stat, "numJumps"))
        {
            return numJumps;
        }
        else if (string.Equals(stat, "jumpVelocity"))
        {
            return jumpVelocity;
        }
        else if (string.Equals(stat, "gravity"))
        {
            return gravity;
        }
        else if (string.Equals(stat, "fallingGravity"))
        {
            return fallingGravity;
        }
        else if (string.Equals(stat, "fallSpeedCap"))
        {
            return fallSpeedCap;
        }
        else if (string.Equals(stat, "rollDistance"))
        {
            return rollDistance;
        }
        else if (string.Equals(stat, "rollDuration"))
        {
            return rollDuration;
        }
        else if (string.Equals(stat, "rollSlowFrames"))
        {
            return rollSlowFrames;
        }
        else if (string.Equals(stat, "rollSlowSpeedMult"))
        {
            return rollSlowSpeedMult;
        }
        else if (string.Equals(stat, "rollCooldown"))
        {
            return rollCooldown;
        }
        else
        {
            print("Requested a stat that does not exist");
            return -1;
        }
    }
}