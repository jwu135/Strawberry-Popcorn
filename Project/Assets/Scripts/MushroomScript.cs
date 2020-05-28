using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomScript : MonoBehaviour
{
    public bool standingUp = false;


    public void setStand()
    {
        standingUp = false;
    }

    public void stand(float b) // no really, why does the animator controller dislike bools?
    {
        bool toggled = b > 0 ? true : false;
        standingUp = toggled;

        Debug.Log(standingUp);
    }
    // Update is called once per frame
    void Update()
    {
        // I'll deal with the it not moving part later
        //bool moving = Mathf.Abs(transform.parent.GetComponent<Rigidbody2D>().velocity.x) > 0;
        float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.
        bool moving = mag > 0.15f;
        Animator anim = GetComponent<Animator>();
        AnimatorClipInfo[] info = anim.GetCurrentAnimatorClipInfo(0);
        if (moving) {
            //transform.parent.GetComponent<PlayerController>().setMode(2);
            //transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            if (!standingUp) {
                transform.parent.GetComponent<PlatformMovementPhys>().unableToMove = true;
                anim.SetBool("Move",true);
            } else {
                transform.parent.GetComponent<PlatformMovementPhys>().unableToMove = false;
            }
        } else {
            if(info[0].clip.name== "MushroomWalkingAnim")
                anim.SetBool("Move",false);
        }
    }
}
