using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerBossShoot : MonoBehaviour
{
    float cooldown = 2f;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }
    public void offsetCooldown()
    {
        cooldown = 3f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cooldown -= Time.deltaTime;
        Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(0, 0, -angle)), Time.time / 100);
        if (cooldown <= 0) {
            BossShoot bs = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>();
            GameObject proj = bs.projectile[0];
            GameObject bullet = Instantiate(proj, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<BossBullet>().setup("breakable", bs.bulletSpeed);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            bullet.GetComponent<BossBullet>().enabled = true;
            bs.addToStack(bullet);
            cooldown = Random.Range(2f,2.25f);
        }
    }
}
