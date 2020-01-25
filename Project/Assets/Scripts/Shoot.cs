using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    private float nextTime = 0f;
    private float cooldown = 1f;

    void Update()
    {
        if (nextTime<Time.time) { 
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetButton("Fire1")) {
                int max = 1;
                for (int i = 0; i < max; i++) {
                    //float offset = (max / 2 - i) * 5; // Makes multiple projectiles offset from each other
                    Vector3 temp = transform.position/* + offset*/;
                    GameObject bullet = Instantiate(projectile, temp, transform.rotation) as GameObject;
                    Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 direction = (Vector2)(mouse - transform.position).normalized;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 4;
                }
                nextTime += cooldown;
            }
        }
    }
}