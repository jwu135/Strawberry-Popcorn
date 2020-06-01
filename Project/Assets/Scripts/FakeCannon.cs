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
    private bool misfire;
    public float copyscalex;
    public float copyscaley;
    public SpriteRenderer CannonStandIn;
    public SpriteRenderer CannonMax;
    public PlayerCombat PlayerCombat;
    public HealthManager HealthManager;
    public PlatformMovementPhys PlatformMovementPhys;

    void Start()
    {
        misfire = false;
        //misfire = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            explode = true;
            release = false;
            // Debug.Log(maxCharge);
            //Debug.Log(explode);
        }

        if (!Input.GetButton("Fire1"))
        {
            explode = false;
            release = true;

            if (Input.GetButtonUp("Fire1"))
            {
                damageScale = scale;
            }
            scale = 0;
            //  Debug.Log(maxCharge);
            //Debug.Log(explode);
        }


        if (maxCharge && PlayerCombat.longPress && misfire)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {
                damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("yellow");
                PlayerCombat.launchVisible = false;
                charging = false;
            }

        }

        if (maxCharge && PlayerCombat.longPress && !misfire)
        {
            //CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {
                //damageScale = scale;
               // PlayerCombat.Shoot2();
                //PlayerCombat.launch = false;
               // Debug.Log("yellow");
               //PlayerCombat.launchVisible = false;
                charging = false;
            }

        }

        if (HealthManager.hit && !maxCharge && charging && PlayerCombat.longPress && misfire)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {


                damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                PlayerCombat.launchVisible = false;
                charging = false;

            }

        }

        if (HealthManager.hit && PlayerCombat.longPress && !misfire)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if (PlayerCombat.launch)
            {
                Debug.Log("orange");
                PlayerCombat.launch = false;
                PlayerCombat.launchVisible = false;
                charging = false;
            }

        }


        if (PlayerCombat.launchVisible && PlayerCombat.longPress && PlatformMovementPhys.rollingFrame == 0 && !maxCharge && misfire)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }

        if (PlayerCombat.launchVisible && PlayerCombat.longPress && PlatformMovementPhys.rollingFrame == 0 && !misfire && !HealthManager.hit)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        }

        if (PlatformMovementPhys.rollingFrame > 0)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        }

        if (HealthManager.hit)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);

        }

        if (maxCharge && PlayerCombat.weaponCycle == 1)
        {
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            if(scale < 2)
            {
                CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            }
            else
            {
                CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
            }
        }

        if (!maxCharge)
        {
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        }

        //charging
        if (explode && !release && !charging && scale == 0f)
        {
            charging = true;
            scale = 0.001f;
        }

        if (explode && !release && PlayerCombat.launch && PlatformMovementPhys.rollingFrame == 0 && charging)
        {

            if (scale < 2)
            {
                scale += 0.048f;
                transform.localScale = new Vector2((float)1.5 + scale, (float)1.5 + scale);
                copyscalex = (float)1.5 + scale;
                copyscaley = (float)1.5 + scale;
            }

            if (scale >= 2f)
            {
                maxCharge = true;
            }

        }


        if (!explode && release && misfire)
        {
            if (PlayerCombat.launch && PlayerCombat.longPress)
            {
                //damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                Debug.Log("messy");
                PlayerCombat.launchVisible = false;
                charging = false;
                maxCharge = false;
            }
            
            transform.localScale = new Vector2((float)1.5 , (float)1.5);
            //scale = 0;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            maxCharge = false;
            HealthManager.hit = false;
        }

        if (!explode && release && !misfire && !HealthManager.hit)
        {
            if (PlayerCombat.launch && PlayerCombat.longPress)
            {
                //damageScale = scale;
                PlayerCombat.Shoot2();
                PlayerCombat.launch = false;
                PlayerCombat.launchVisible = false;
                charging = false;
                maxCharge = false;
            }
            transform.localScale = new Vector2((float)1.5, (float)1.5);
            //scale = 0;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            maxCharge = false;
            HealthManager.hit = false;
        }

        if (!explode && release && !misfire && HealthManager.hit)
        {
            transform.localScale = new Vector2((float)1.5, (float)1.5);
            //scale = 0;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            maxCharge = false;
            HealthManager.hit = false;
            Debug.Log("WORD");
        }

        if (!misfire && PlatformMovementPhys.rollingFrame > 0)
        {
            transform.localScale = new Vector2((float)1.5, (float)1.5);
            scale = 0.001f;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            maxCharge = true;
            PlayerCombat.launch = false;
            PlayerCombat.launchVisible = false;
            //Debug.Log("doge");
            //maxCharge = false;
            //HealthManager.hit = false;
        }

        if (!misfire && HealthManager.hit)
        {
            transform.localScale = new Vector2((float)1.5, (float)1.5);
            scale = 0.001f;
            CannonStandIn.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            CannonMax.material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
            charging = false;
            PlayerCombat.launch = false;
            PlayerCombat.launchVisible = false;
            //HealthManager.hit = false;
            Debug.Log("hello");
        }

    }
}
