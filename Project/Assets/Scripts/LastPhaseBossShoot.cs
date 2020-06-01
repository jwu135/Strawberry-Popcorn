using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPhaseBossShoot : MonoBehaviour
{
    public bool bottomEye = false;
    float cooldown = 2f;
    GameObject player;
    private GameObject mother;
    void Start()
    {
        cooldown = Random.Range(2f, 3f);
        player = GameObject.FindGameObjectWithTag("Player");
        mother = GameObject.Find("Mother's Eye");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(mother!=null)
            mother.GetComponent<Enemy>().enterHit(other);
    }

    void shoot()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0) {
            int randInt = Random.Range(0, 4);
            mother.GetComponent<BossShoot>().translateNum3(randInt, 0.8f, transform);
            cooldown = Random.Range(2f, 3f);
        }
    }

    void look()
    {
        Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // handles look direction and shoot direction
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(0, 0, -angle)), Time.time / 100);
    }
    void move(){
        Vector3 temp = transform.localPosition;
        temp.x = Mathf.Sin(Time.time / 2) * 4.5f;
        temp.y = Mathf.Sin(Time.time / 2) * Mathf.Cos(Time.time / 2) + 17.08978f;
        transform.localPosition = temp;

    }
    void lookAround()
    {
        shoot();
        look();
        if (bottomEye) {
            move();
        }
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
        
    }
}
