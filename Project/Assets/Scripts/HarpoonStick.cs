using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonStick : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public bool thrown;

    // Start is called before the first frame update
    public void Start()
    {
        //this.transform.Rotate(0, 0, -90);
    }

    void Update()
    {
        thrown = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            thrown = false;
        }

        if (other.tag == "enemyBody")
        {
            thrown = false;
            //rb.useGravity = false;
            rb.isKinematic = true;
            //rb.velocity = transform.right * 0;
        }
    }


    public void OnBecameInvisible()
    {
        Destroy(gameObject);
        thrown = false;
    }
}
