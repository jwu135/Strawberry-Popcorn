using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPiece : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding with "+collision.gameObject.tag);
        Debug.Log("Collided");
        if (collision.gameObject.tag == "Player") {
            Debug.Log("On Player");
        }
    }
        // Start is called before the first frame update
        void Start()
    {
        Debug.Log("Alive");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
