using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject explodingStrawberry;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            explode();   
        }
    }

    public void explode()
    {
        GameObject temp = Instantiate(explodingStrawberry, transform.position, transform.rotation) as GameObject;
        temp.transform.localScale = transform.localScale;
        destroy();

    }
    
    public void destroy() {
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
