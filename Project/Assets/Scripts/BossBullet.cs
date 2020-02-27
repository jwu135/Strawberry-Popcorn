using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    GameObject Boss;
    // Start is called before the first frame update
    void Start()
    {
        Boss = GameObject.Find("Mother");   
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Boss.transform.position) > 400f)
            Destroy(gameObject);
    }
}
