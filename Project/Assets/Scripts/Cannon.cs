using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    float scale = 0.01f;
    public bool explode = false;
    public bool maxCharge = false;
    public bool release = false;
    public Bullet Bullet;
    public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && !maxCharge && !release)
        {
            explode = true;
            Bullet.rb.velocity = transform.right * 0;
            rb.gravityScale = 0;
            // Debug.Log(maxCharge);
            Debug.Log(explode);
        }
        if (!Input.GetButton("Fire2") || maxCharge)
        {
            explode = false;
            release = true;
            Bullet.rb.velocity = transform.right * 6;
            //  Debug.Log(maxCharge);
            Debug.Log(explode);
        }

        if (explode && !release)
        {
            if (scale < 7)
            {
                scale += 0.02f;
                transform.localScale = new Vector2(1 + scale, 1 + scale);
            }

            if (scale >= 7f)
            {               
                maxCharge = true;
                Debug.Log("hi");
            }
        }

    }
}

