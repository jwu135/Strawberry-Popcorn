using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCannon : MonoBehaviour
{
    public float scale = 0.0f;
    public float damageScale = 0.01f;
    public bool explode = false;
    public bool maxCharge = false;
    public bool release = false;
    public bool charging = false;
    public float copyscalex;
    public float copyscaley;
    public SpriteRenderer CannonStandIn;
    public PlayerCombat PlayerCombat;
    public HealthManager HealthManager;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            explode = true;
            release = false;
            // Debug.Log(maxCharge);
            Debug.Log(explode);
        }
        if (!Input.GetButton("Fire1"))
        {
            explode = false;
            release = true;
            //  Debug.Log(maxCharge);
            //Debug.Log(explode);
        }

        if (maxCharge && PlayerCombat.longPress)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {
                damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("messy");
                PlayerCombat.launchVisible = false;
                charging = false;
            }

        }

        if (HealthManager.hit && !maxCharge && charging && PlayerCombat.longPress)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {

                HealthManager.hit = false;
                charging = false;
                maxCharge = true;
            }

        }


        if (PlayerCombat.launchVisible && PlayerCombat.longPress)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }

        if (explode && !release && !charging && scale == 0)
        {
        charging = true;
        }

        if (explode && !release && PlayerCombat.launch)
        {

            if (scale< 2)
            {
                scale += 0.012f;
                transform.localScale = new Vector2((float)1.5 + scale, (float)1.2 + scale);
                copyscalex = (float)1.5 + scale;
                copyscaley = (float)1.2 + scale;
            }

            if (scale >= 2f)
            {               
                maxCharge = true;
                Debug.Log("hi");
            }
               
        }

        if (!explode && release)
        {
            if (PlayerCombat.launch && PlayerCombat.longPress)
            {
                damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("messy");
                PlayerCombat.launchVisible = false;
                charging = false;
                maxCharge = false;
            }
            transform.localScale = new Vector2((float)1.5 , (float)1.2);
            scale = 0;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            maxCharge = false;
        }

    }
}
