using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harpoon : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public GameObject harpoonPrefab;
    public bool thrown;
    public bool touched = true;

    // Start is called before the first frame update
    public void Start()
    {
        thrown = true;
        Debug.Log(thrown);
        rb.velocity = transform.right * speed;
        this.transform.Rotate(0, 0, -90);
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
            if (touched)
            {
                GameObject spawnedObject2 = (GameObject)Instantiate(harpoonPrefab, transform.position, transform.rotation);
                Debug.Log(spawnedObject2.transform.position.x);
                touched = false;
                //rb.velocity = transform.right * 0;
            }
        }
    }


    public void OnBecameInvisible()
    {
        Destroy(gameObject);
        thrown = false;
    }
}
