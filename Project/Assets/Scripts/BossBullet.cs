using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public GameObject Boss;
    public GameObject explodingEye;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            explode();
        }
    }
    void explode()
    {
        GameObject temp = Instantiate(explodingEye, transform.position, transform.rotation) as GameObject;
        temp.transform.localScale = transform.localScale;
        destroy();

    }
    public void destroy()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    // Update is called once per frame
    void lookAround()
    {
        if (Vector2.Distance(transform.position, Boss.transform.position) > 50f) {
            explode();  
        }
    }
}
