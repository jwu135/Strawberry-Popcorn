using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread2 : MonoBehaviour
{
    float scale = 0.01f;
    public bool explode = false;
    public SpecialLaucher SpecialLaucher;
    public Rigidbody2D rb;
    // Update is called once per frame
    void Update()
    {

            explode = true;
            SpecialLaucher.rb.velocity = transform.right * 0;
            rb.gravityScale = 0;
        transform.Rotate(Vector3.forward, -10.0f * Time.deltaTime);

        if (explode)
        {
            if (scale < 0.2f)
            {
                scale += 0.002f;
                transform.localScale = new Vector2((float)0.1 + scale, (float)0.1 + scale);
            }

            if (scale >= 0.2f && scale < 0.3f)
            {
                scale += 0.0025f;
                transform.localScale = new Vector2((float)0.1 + scale, (float)0.1 + scale);

            }
            if (scale >= 0.3f && scale < 0.4f)
            {
                scale += 0.0015f;
                transform.localScale = new Vector2((float)0.1 + scale, (float)0.1 + scale);

            }
            if (scale >= 0.4f)
            {
                scale += 0.001f;
                transform.localScale = new Vector2((float)0.1 + scale, (float)0.1 + scale);

            }
            if (scale >= 1.5f)
            {
                explode = false;
                Destroy(gameObject);
            }
        }

    }
}
