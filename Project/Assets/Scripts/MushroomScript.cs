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

    // Update is called once per frame
    void Update()
    {
        // I'll deal with the it not moving part later
        bool moving = Mathf.Abs(transform.parent.GetComponent<Rigidbody2D>().velocity.x) > 0;
        Debug.Log(transform.parent.GetComponent<Rigidbody2D>().velocity.x);
        Animator anim = GetComponent<Animator>();
        AnimatorClipInfo[] info = anim.GetCurrentAnimatorClipInfo(0);
        if (moving) {
            if (!standingUp) {
                anim.SetBool("Move",true);
            }
        } else {
            if(info[0].clip.name== "MushroomWalkingAnim")
                anim.SetBool("Move",false);
        }
    }
}
