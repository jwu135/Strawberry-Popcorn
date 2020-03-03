using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLaucher : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    public bool thrown = true;

    // Start is called before the first frame update
    public void Start()
    {
        thrown = true;
        Debug.Log(thrown);
        rb.velocity = transform.right * speed;
        this.transform.Rotate(0, 0, -90);
    }

}
