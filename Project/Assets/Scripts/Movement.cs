using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject legs;
    private GameObject arm;
    private HealthManager healthManager;
    private float dodgeCounter;
    //private float offset = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legs = transform.GetChild(1).gameObject; // temporarily getting sprites this way
        arm = transform.GetChild(0).gameObject;
        healthManager = GetComponent<HealthManager>();
        dodgeCounter = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
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

        rb.transform.position +=  Movement.normalized  * Time.deltaTime * 4;
        if (Input.GetKey(KeyCode.LeftShift) && dodgeCounter <= 0)
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

    }
}
