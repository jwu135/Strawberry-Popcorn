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
    public SpriteRenderer Bullet2;
    // Update is called once per frame
     void Start()
    {
        if (Input.GetButton("Fire2") )
        {
            Bullet.rb.velocity = transform.right * 6;
            release = true;

        }
        if (!Input.GetButton("Fire2") )
        {

            Bullet.rb.velocity = transform.right * 6;
            release = true;

        }



    }
}

