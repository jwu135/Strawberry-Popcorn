using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCannon : MonoBehaviour
{
    public float scale = 0.01f;
    public bool explode = false;
    public bool maxCharge = false;
    public bool release = false;
    public float copyscalex;
    public float copyscaley;
    public SpriteRenderer CannonStandIn;
    public PlayerCombat PlayerCombat;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            explode = true;
            release = false;
            // Debug.Log(maxCharge);
            Debug.Log(explode);
        }
        if (!Input.GetButton("Fire2"))
        {
            explode = false;
            release = true;
            //  Debug.Log(maxCharge);
            //Debug.Log(explode);
        }

        if (maxCharge)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("messy");
                PlayerCombat.launchVisible = false;
            }

        }

        if (PlayerCombat.launchVisible)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }

            if (explode && !release && PlayerCombat.launch)
            {
                if (scale< 4.6)
                {
                    scale += 0.013f;
                    transform.localScale = new Vector2((float)2.5 + scale, (float)2 + scale);
                    copyscalex = (float)2.5 + scale;
                    copyscaley = (float)2 + scale;
                }

                if (scale >= 4.6f)
                {               
                    maxCharge = true;
                    Debug.Log("hi");
                }
            }

        if (!explode && release)
        {
            if (PlayerCombat.launch)
            {
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("messy");
                PlayerCombat.launchVisible = false;
            }
            transform.localScale = new Vector2((float)2.5 , (float)2);
            scale = 0;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            maxCharge = false;
        }

    }
}
