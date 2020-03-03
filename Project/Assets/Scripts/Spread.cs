using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : MonoBehaviour
{
    float scale = 0.01f;
    public bool explode = false;
    public SpecialLaucher SpecialLaucher;
    public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            explode = true;
            SpecialLaucher.rb.velocity = transform.right*0;
            rb.gravityScale = 0;
        }
        if (explode)
        {
            if (scale < 2f)
            {
                scale += 0.2f;
                transform.localScale = new Vector2(1 + scale, 1 + scale);
            }

            if (scale >= 2f && scale < 3f)
            {
                scale += 0.025f;
                transform.localScale = new Vector2(1 + scale, 1 + scale);

            }
            if (scale >= 3f && scale < 4f)
            {
                scale += 0.015f;
                transform.localScale = new Vector2(1 + scale, 1 + scale);

            }
            if (scale >= 4f)
            {
                scale += 0.01f;
                transform.localScale = new Vector2(1 + scale, 1 + scale);

            }
            if (scale >= 15f)
            {
                explode = false;
                Destroy(gameObject);
            }
        }

    }   
}
