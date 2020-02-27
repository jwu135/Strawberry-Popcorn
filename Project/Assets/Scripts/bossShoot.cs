using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject projectile;
    public GameObject hitObj;
    public GameObject AoE;

    private int[] spawnPointsX = new int[] { -8, -4, 0, 4, 8 };
    private float[] spawnPointsY = new float[] { -3f, -1.5f, 0, 1.5f, 3f};
    [HideInInspector]
    public float nextTime;
    private float cooldown;
    private GameObject player;
    private void Start()
    {
        nextTime = Time.time + 2f;
        cooldown = Time.time + 1f;
        player = GameObject.FindWithTag("Player");
    }
    public void startTime()
    {
        nextTime = Time.time + 1;
    }
    void Update()
    { 
        
        if (nextTime < Time.time) {
            if (Random.Range(0f, 1f) > 0.5f)
                Shoot();
            else
                physicalPattern();
            
            
        }
    }
    void physicalPattern() {
        int pattern = Random.Range(0,3);
        int pos = Random.Range(0, 4);
        Physical(3);
        nextTime += cooldown;
    }
    void Physical(int pos) {
        //int pos =;
        GameObject physicalAttack = null;
        if (pos != 3) {
            if (pos == 0) {
                // For now, make pop up instantly
                int rand = Random.Range(0, spawnPointsX.Length);
                Vector3 position = new Vector3(spawnPointsX[rand], -13, 0);
                physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
            } else if (pos == 1) {
                int rand = Random.Range(0, spawnPointsY.Length);
                Vector3 position = new Vector3(17.5f, spawnPointsY[rand], 0);
                Quaternion tempRotation = Quaternion.Euler(0, 0, 180);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
            } else if (pos == 2) {
                int rand = Random.Range(0, spawnPointsY.Length);
                Vector3 position = new Vector3(-17, spawnPointsY[rand], 0);
                physicalAttack = Instantiate(hitObj, position, transform.rotation) as GameObject;
            }
            physicalAttack.GetComponent<Animator>().SetTrigger("Spike");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else {
            physicalAttack = Instantiate(AoE, transform.position, AoE.transform.rotation) as GameObject;
            physicalAttack.GetComponent<Animator>().SetTrigger("Expand");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        }
    }
    

    void Shoot()
    { 
        int max = 1;
        for (int i = 0; i < max; i++) {
            //float offset = (max / 2 - i) * 5;
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(projectile, temp, transform.rotation ) as GameObject;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
        }
        nextTime += cooldown;
    }
}
