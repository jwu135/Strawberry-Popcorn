using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; //the maximum movement speed
    public float acceleration; //the multiplier for speeding up
    public float deceleration; //the coefficient of drag
    public float numJumps; //the amount of times the player can jump before touching the ground again
    public float jumpVelocity; //the initial speed the player leaves the ground upon pressing jump
    public float gravity; //the amount of velocity removed from the players initial jump velocity until...
    public float fallingGravity; //effect of gravity while falling
    public float fallSpeedCap; //it reaches the fall speed cap
    public int mode;
    // Start is called before the first frame update
    public void setMode(int modeSet)// 0 for flight 1 for platforming
    {
        mode = modeSet;
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
        else
        {
            print("Requested a stat that does not exist");
            return -1;
        }
    }
}