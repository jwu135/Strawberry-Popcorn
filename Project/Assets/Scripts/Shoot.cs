using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    private float nextTime = 0f;
    private float cooldown = 1f;

    private int usingController = 0; //0=keyboard 1=controller
    void Update()
    {
        if (Input.GetAxis("Aim_Vertical") != 0 || Input.GetAxis("Aim_Horizontal") != 0)
        {
            usingController = 1;
        }
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 )
        {
            usingController = 0;
        }
        Debug.Log(usingController);
        if (nextTime<Time.time) { 
            if (Input.GetButton("Fire1")) {
                int max = 1;
                for (int i = 0; i < max; i++) {
                    //float offset = (max / 2 - i) * 5; // Makes multiple projectiles offset from each other
                    Vector3 temp = transform.position/* + offset*/;
                    GameObject bullet = Instantiate(projectile, temp, transform.rotation) as GameObject;
                    Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 direction = (Vector2)(mouse - transform.position).normalized;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 4;
                }
                nextTime = Time.time+cooldown;
            }
        }
    }
}