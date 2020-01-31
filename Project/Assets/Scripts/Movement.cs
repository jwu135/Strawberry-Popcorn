using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject legs;
    private HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legs = transform.GetChild(0).gameObject;
        healthManager = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) {
            Vector3 Jump = new Vector3(0, 6, 0);
            rb.velocity = Jump;
        }
        if (Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A)) {
            Vector3 temp = legs.transform.localScale;
            if(temp.y>0)
             temp.y *= -1;
            legs.transform.localScale = temp;
        } else {
            Vector3 temp = legs.transform.localScale;
            if (temp.y < 0)
                temp.y = Mathf.Abs(temp.y);
            legs.transform.localScale = temp;
        }
            float h = Input.GetAxis("Horizontal");
        Vector3 Movement = new Vector3(h, 0, 0);
        rb.transform.position +=  Movement.normalized  * Time.deltaTime * 4;

        if (Input.GetKey(KeyCode.E))
        {
            healthManager.invicibilityCounter = healthManager.invicibilityLength;
        }
    }
}
