using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPhaseBossShoot : MonoBehaviour
{
    float cooldown = 2f;
    GameObject player;
    GameObject mother;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mother = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        mother.GetComponent<Enemy>().enterHit(other);
    }


    void Update()
    {
        cooldown -= Time.deltaTime;
        Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // handles look direction and shoot direction
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(0, 0, angle)), Time.time / 100);
    }
}
