using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject laserobj;
    public GameObject projectile;
    public GameObject hitObj;
    public GameObject AoE;

    private int[] spawnPointsX = new int[] { -14, -7, 0, 7, 14 };
    private float[] spawnPointsY = new float[] { -7f, -3.5f, 0, 3.5f, 7f};

    [HideInInspector]
    public float nextTime;
    private float nextTimeShoot;
    private float AoeNextTime;

    private float cooldown;
    public float AoECooldown;
    public float shootCooldown;



    private int phase = 0;
    private GameObject player;

    private float[] healthmarks = {75f,50f};
    private int healthIndex = 0;


    private void Start()
    {
        nextTime = Time.time + 2f;
        nextTimeShoot = Time.time + 1f;
        AoeNextTime = Time.time + 1f;
        cooldown = 1f;
        AoECooldown = 4f;
        shootCooldown =  2f;
        player = GameObject.FindWithTag("Player");
    }
    public void startTime()
    {
        nextTime = Time.time + 1;
    }
    public void setPhase(int p)
    {
        phase = p;
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    void lookAround()
    {
        // super jank just getting this done in time for release
        if (GameObject.Find("Mother").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BossIdle")) {
            

            /*if (Vector2.Distance(player.transform.position, GameObject.Find("Mother").transform.position) > 6f && nextTimeShoot < Time.time) {
                Shoot(true);
                //Debug.Log("Shot");
                nextTimeShoot = Time.time + shootCooldown;
            } // Projectile Shoot*/

            if (phase == 0 || phase == 3) { // Main Projectile
                if (nextTimeShoot < Time.time) {
                    Shoot(false);
                    nextTimeShoot = Time.time + shootCooldown;
                    //Debug.Log("Shot");
                }

            }
            if (phase == 0 || phase == 2 || phase == 3) { // AoE
                if (Vector2.Distance(player.transform.position, GameObject.Find("Mother").transform.position) < 4f && AoeNextTime < Time.time) {
                    Physical(3);
                    AoeNextTime = Time.time + AoECooldown;
                } 
            }
            if (phase==0||phase == 1) { // Spike
                if (nextTime < Time.time) {
                    if (phase == 0) {
                        Physical(phase); // doing it this way because phase number and the pos variable happen to have the same parameters
                    } else {
                        if (player.transform.position.x > 0) {
                            Physical(phase);
                        } else {
                            Physical(phase+1);
                        }
                    }
                    nextTime = Time.time + cooldown;
                }               
            }
            /*
            if (phase==2||phase == 3) { // AoE
                if (nextTime < Time.time) {
                    if (phase > 0)
                        physicalPattern(2);
                }
            }*/
        }
    }
    void physicalPattern(int pattern = -1) {
        if(pattern == -1) pattern = Random.Range(0, 3);
        if (pattern == 0 || pattern == 1) {
            int pos = Random.Range(0, 3);
            Physical(pos,true);
        } else {
            int pos = Random.Range(0, 3);
            StartCoroutine("PhysicalWave",pos);
        }

        nextTime = Time.time + cooldown;
    }
    IEnumerator PhysicalWave(int pos)
    {
        int Reverse = 0;
        if (Random.Range(0, 2) > 0)
            Reverse = 4;
        for(int i = 0; i < 5; i++) {
            Physical(pos,false,(int)Mathf.Abs(Reverse-i));
            yield return new WaitForSeconds(.2f);
        }
    }
    void Physical(int pos,bool random = true,int place = 0) {
        GameObject physicalAttack = null;
        if (pos != 3) {
            Vector3 position = new Vector3(0, 0, 0);
            if (pos == 0) {
                // For now, make pop up instantly
                int rand = Random.Range(0, spawnPointsX.Length);
                if(random)
                    position = new Vector3(spawnPointsX[rand], -13, 0);
                else
                    position = new Vector3(spawnPointsX[place], -13, 0);
                physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
            } else if (pos == 1) {
                int rand = Random.Range(0, spawnPointsY.Length);
                if (random)
                    position = new Vector3(17.5f, spawnPointsY[rand], 0);
                else
                    position = new Vector3(17.5f, spawnPointsY[place], 0);
                Quaternion tempRotation = Quaternion.Euler(0, 0, 180);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
            } else if (pos == 2) {
                int rand = Random.Range(0, spawnPointsY.Length);
                if (random)
                    position = new Vector3(-17, spawnPointsY[rand], 0);
                else
                    position = new Vector3(-17, spawnPointsY[place], 0);
                physicalAttack = Instantiate(hitObj, position, transform.rotation) as GameObject;
            }
            physicalAttack.GetComponent<Animator>().SetTrigger("Spike");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else {
            physicalAttack = Instantiate(AoE, transform.position, AoE.transform.rotation) as GameObject;
            physicalAttack.transform.parent = transform;
            physicalAttack.GetComponent<Animator>().SetTrigger("Expand");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        }
    }
    

    void Shoot(bool laser = false)
    {
        if (laser) {
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(laserobj, temp, transform.rotation) as GameObject;
            bullet.transform.parent = transform;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            bullet.GetComponent<AttackTimer>().setTimer(1f);
            bullet.GetComponent<AttackTimer>().setHits(5f);
            bullet.GetComponent<AttackTimer>().disappear();

        } else {
            int max = 1;
            for (int i = 0; i < max; i++) {
                //float offset = (max / 2 - i) * 5;
                Vector3 temp = transform.position;
                GameObject bullet = Instantiate(projectile, temp, transform.rotation) as GameObject;
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                bullet.GetComponent<BossBullet>().enabled = true;
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;
            }
        }
        nextTime = Time.time + cooldown;
    }
}
