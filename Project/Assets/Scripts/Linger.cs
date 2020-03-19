using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linger : MonoBehaviour
{
    public float speed;
    public Spread2 Spread2;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > 0 && Spread2.explode)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
