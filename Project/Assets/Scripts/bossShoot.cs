using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject hitObj;

    private float nextTime = 0f;
    private float cooldown = 1f;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    { 
        if (nextTime < Time.time) {
            Shoot();
            nextTime += cooldown;
        }
    }
    // Update is called once per frame
    void Shoot()
    { 
        int max = 1;
        for (int i = 0; i < max; i++) {
            //float offset = (max / 2 - i) * 5;
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(projectile, temp, transform.rotation ) as GameObject;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 4;
        }
    }
    void Hit()
    {
        
         // GameObject bullet = Instantiate(hitObj, temp, transform.rotation) as GameObject;
    }
}
