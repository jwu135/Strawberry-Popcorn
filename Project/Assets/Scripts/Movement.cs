using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Movement : MonoBehaviour
{
    private UnityArmatureComponent armatureComponent;
    [HideInInspector]
    public float direction = 0; // left is 0, right is 1;
    private float lastdirection = 0;
    void Start()
    {
        armatureComponent = GameObject.FindGameObjectWithTag("ArmatureTag").GetComponent<UnityArmatureComponent>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            armatureComponent.animation.timeScale = 3;
            armatureComponent.animation.Play("Jumping",1);
        }
        bool moving = Input.GetAxisRaw("Horizontal") > 0 || (Input.GetAxisRaw("Horizontal") < 0);
        bool last = armatureComponent.animation.lastAnimationName == "Running" || armatureComponent.animation.lastAnimationName == "backRunning";
        Vector2 pos = transform.Find("Arm").transform.localPosition;
        if (direction > 0) {
            pos.x = 0.21f;
        } else
            pos.x = -0.185f;
        transform.Find("Arm").transform.localPosition = pos;

        if (moving) {
            
            if (Input.GetAxisRaw("Horizontal")< 0) {
                if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction!=lastdirection) {
                    if (direction > 0)
                        armatureComponent.animation.Play("Running", 1);
                    else
                        armatureComponent.animation.Play("backRunning", 1);
                }
            }
            if (Input.GetAxisRaw("Horizontal") > 0) {
                if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection)
                    if (direction > 0)
                        armatureComponent.animation.Play("backRunning", 1);
                    else
                        armatureComponent.animation.Play("Running", 1);
            }
            if (direction != lastdirection) {

                lastdirection = direction;
            }
        } else if (armatureComponent.animation.isCompleted||last) {
            armatureComponent.animation.timeScale = 1;
            armatureComponent.animation.Play("Idle");
            
        }
        // Leaving this here just in case
        /*if (Input.GetKey(KeyCode.Space)) {
            Vector3 Jump = new Vector3(0, 6, 0);
            rb.velocity = Jump;
        }
        float h = Input.GetAxis("Horizontal");
        Vector3 Movement = new Vector3(h, 0, 0);

        // Sprite flipping section, currently only for legs
        if (h<0) { 
            Vector3 temp = legs.transform.localScale;
            if (temp.y > 0)
                temp.y *= -1;
            legs.transform.localScale = temp;
        } else {
            Vector3 temp = legs.transform.localScale;
            if (temp.y < 0)
                temp.y = Mathf.Abs(temp.y);
            legs.transform.localScale = temp;
        }

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); // mouse also used in Shoot.cs

        Vector3 rotation = mouse - arm.transform.position;
        float step = Time.deltaTime * 2;
        Vector3 direction = Vector3.RotateTowards(arm.transform.forward, rotation, step, 0);
        //arm.transform.rotation = Quaternion.Angle Axis(direction),Vector3.up);
        arm.transform.rotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        
        //arm.transform.rotation = Quaternion.Euler(rotation);
        //Quaternion target = Quaternion.Euler(rotation);
        //arm.transform.rotation = Quaternion.Slerp(arm.transform.rotation,target,0);

        rb.transform.position +=  Movement.normalized  * Time.deltaTime * 4;*/
        /*if (Input.GetKey(KeyCode.LeftShift) && dodgeCounter <= 0)
        {
            dodgeCounter = 0.5f;
            // Invicibility
            healthManager.invicibilityCounter = healthManager.invicibilityLength;

            // Movement
            if (Input.GetKey(KeyCode.A))
            {
                Vector3 dodge = new Vector3(-6, 0, 0);
                rb.velocity = dodge;
            } 
            else
            {       
                Vector3 dodge = new Vector3(6, 0, 0);
                rb.velocity = dodge;
            }
        }
        
        if(dodgeCounter > 0)
        {
            dodgeCounter -= Time.deltaTime;
        }
        */

    }
}
