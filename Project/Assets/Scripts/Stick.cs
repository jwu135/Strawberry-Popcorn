using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{

    public Vector2 aPosition1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemyBody")
        {
            this.transform.parent = other.transform;
            //aPosition1 = new Vector2(transform.position.x, transform.position.y);
           // Debug.Log(aPosition1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "enemyBody")
        {
            aPosition1 = new Vector2(transform.position.x, transform.position.y);
           // Debug.Log(aPosition1);
        }
    }
}
