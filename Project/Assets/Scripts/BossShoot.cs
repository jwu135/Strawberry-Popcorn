using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject laserobj;
    public GameObject[] projectile;
    public GameObject hitObj;
    public GameObject AoE;
    public GameObject portal;
    private Stack allProjectiles = new Stack();

    private int[] spawnPointsX = new int[] { -14, -7, 0, 7, 14 };
    private float[] spawnPointsY = new float[] { -3f, -.75f, 1.5f, 3.75f, 6f};

    [HideInInspector]
    public float nextTime;
    private float nextTimeShoot;
    private float AoeNextTime;

    private float cooldown;
    public float AoECooldown;
    public float shootCooldown;


    private float phase = 0;
    private GameObject player;

    private float[] healthmarks = {75f,50f};
    private int healthIndex = 0;

    public void destroyProjectiles()
    {
        foreach(GameObject projectile in allProjectiles) {
            Destroy(projectile);
        }
    }
    public void addToStack(GameObject bullet)
    {
        allProjectiles.Push(bullet);
    }

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
        AoeNextTime = Time.time + AoECooldown;
        nextTimeShoot = Time.time + shootCooldown;
    }
    public void setPhase(float p)
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
        if (GameObject.Find("Mother").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BossIdle")) {
            // Projectile stuff
            if (nextTimeShoot < Time.time) {
                if (GlobalVariable.deathCounter == 100) { // stuff for first run of the boss. Change to some high number if you're testing boss phase stuff
                        Shoot(false, 2, 10, cd: 0.33f);

                } else {
                    float rand = Random.Range(0f, 1f);
                    // Phase 1 stuff
                    if (phase == 0f) {
                        Shoot(false, 0, cd: 1f);
                    } else if (phase == 0.25f) {
                        int randInt = Random.Range(0, 1);
                        translateNum(randInt, 1f);
                    } else if (phase == 0.5f || phase == 0.75f) {
                        int randInt = Random.Range(0, 2);
                        translateNum(randInt, 1f);
                    }
                    // Phase 2 stuff
                    else if (phase == 1f|| phase == 1.25f) {
                        int randInt = Random.Range(0, 2);
                        translateNum(randInt, 0.9f);
                    }else if (phase == 1.5f) {
                        int randInt = Random.Range(0, 3);
                        translateNum(randInt, 0.9f);
                    } else if (phase == 1.75f) {
                        int randInt = Random.Range(0, 4);
                        translateNum(randInt, 0.9f);

                    }
                    // Phase 3 stuff
                    else if(phase>=2f){
                        float randFloat = Random.Range(0f, 1f);
                        if (randFloat > 0.9f) {
                            Shoot(true);
                            nextTimeShoot = Time.time + 1;
                        } else {
                            if (phase == 2f) {
                                int randInt = Random.Range(0, 4);
                                translateNum(randInt, 0.8f);

                            } else if (phase == 2.25f) {
                                int randInt = Random.Range(0, 6);
                                translateNum(randInt, 0.8f);

                            } else if (phase >= 2.5f) {
                                int randInt = Random.Range(0, 7);
                                translateNum(randInt, 0.8f);

                            } /*else if (phase == 3f) {
                                Shoot(false, 1, cd: 1f);
                            }*/
                        }
                    }
                    // Misc handling
                    else {
                        Shoot(false);
                        Debug.Log("Fell outside of normal phases");
                    }
                }
            }

            // Tentacle stuff || 
            if (nextTime < Time.time) {
                if (0.75f>phase&&phase >= 0) { // Spike
                                               //Physical(0);
                    Physical(0);
                    nextTime = Time.time + cooldown * 2;
                } else if (phase<1.25f&&phase >= .75f) {
                    if (player.transform.position.x > 10) {
                        Physical(1);
                    } else if (player.transform.position.x < -10) {
                        Physical(2);
                    } else {
                        Physical(0);
                    }
                    nextTime = Time.time + cooldown * 2;
                }
                else if (phase<2.75f&&phase >= 1.25f) {
                    if (player.transform.position.x > 10) {
                        Physical(1);
                    } else if (player.transform.position.x < -10) {
                        Physical(2);
                    } else {
                        float randFloat = Random.Range(0f, 1f);
                        if (randFloat > 0.8f) {
                            physicalPattern(2, 0);
                        } else {
                            Physical(0);
                        }
                    }
                    nextTime = Time.time + cooldown * 2;
                } else if (phase >= 2.75f) {
                    if (player.transform.position.x > 12) {
                        Physical(1);
                    } else if (player.transform.position.x < -12) {
                        Physical(2);
                    } else {
                        float randFloat = Random.Range(0f, 1f);
                        if (randFloat >= 0.9f) {
                            physicalPattern(2, 0);
                        } else if (randFloat < 0.9f &&randFloat>0.5f) {
                            Physical(4);
                        } else {
                            Physical(0);
                        }
                    }
                    nextTime = Time.time + cooldown * 2;
                }
            }
         
        }
    }
    void physicalPattern(int pattern = -1,int pos = -1) {
        if(pattern == -1) pattern = Random.Range(0, 3);
        if (pattern == 0 || pattern == 1) {
            pos = Random.Range(0, 3);
            Physical(pos,true);
        } else { // pattern 2 is wave
            if(pos==-1)pos = Random.Range(0, 3);
            StartCoroutine("PhysicalWave",pos);
        }
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
        //nextTime = Time.time + cooldown;
    }
    void Physical(int pos,bool random = true,int place = 0) {
        
        GameObject physicalAttack = null;
        if (pos < 3) {
            Vector3 position = new Vector3(0, 0, 0);
            if (pos == 0) { // Ground tentacles
                int rand = Random.Range(0, spawnPointsX.Length);
                float randPos = Random.Range(-26.8f, 26.8f);
                float offset = Random.Range(-4f, 4f) + player.transform.position.x;
                offset = Mathf.Clamp(offset,-26.8f, 26.8f);
                if (random)
                    position = new Vector3(offset, -7.96f, 0);
                else
                    position = new Vector3(spawnPointsX[place], -7.96f, 0);
                physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
            } else if (pos == 1) { // right wall tentacles
                int rand = Random.Range(0, spawnPointsY.Length);
                float offset = Random.Range(-2f, 2f) + player.transform.position.y;
                if (random)
                    position = new Vector3(28.4f, offset, 0);
                else
                    position = new Vector3(28.4f, spawnPointsY[place], 0);
                Quaternion tempRotation = Quaternion.Euler(0, 0, 90);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
            } else if (pos == 2) { // left wall tentacles
                int rand = Random.Range(0, spawnPointsY.Length);
                float offset = Random.Range(-2f, 2f) + player.transform.position.y;
                if (random)
                    position = new Vector3(-28.5f, offset, 0);
                else
                    position = new Vector3(-28.5f, spawnPointsY[place], 0);
                
                Quaternion tempRotation = Quaternion.Euler(0, 0, 270);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
                physicalAttack.transform.localScale = Vector3.Scale(physicalAttack.transform.localScale, new Vector3(-1,1)); 
            }
            
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else if (pos==3) {
            physicalAttack = Instantiate(AoE, transform.Find("AoEAnchor").transform.position, AoE.transform.rotation) as GameObject;
            physicalAttack.transform.parent = transform.Find("AoEAnchor").transform;
            physicalAttack.GetComponent<Animator>().SetTrigger("Expand");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else {
            float posX = Random.Range(-2, 2);
            float posY = Random.Range(-2, 2);
            Vector2 position = new Vector2(posX, posY) + (Vector2)player.transform.position;
            Vector3 direction = (Vector2)((Vector2)player.transform.position - position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //physicalAttack.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            Quaternion tempRotation = Quaternion.Euler(0, 0, angle);

            physicalAttack = Instantiate(portal, position, tempRotation) as GameObject;
            
        }
        allProjectiles.Push(physicalAttack);
    }
    
    private void translateNum(int index, float cooldown)
    {
        int[] arr = {0,2,6,4,7,8,3,1};
        Shoot(false,arr[index],cd:cooldown);
    }

    void Shoot(bool laser = false, int pattern = 0, int max = 1,float maxangle = 180,float cd = 2f)
    {
        if (laser) {
            SoundManager.PlaySound("bossLaser");
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(laserobj, temp, transform.rotation) as GameObject;
            bullet.transform.parent = transform;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GetComponent<BossMovement>().setRotateable(false);
            bullet.GetComponent<AttackTimer>().setEnemy(transform);
            bullet.GetComponent<AttackTimer>().setTimer(1f);
            bullet.GetComponent<AttackTimer>().setHits(5f);
            bullet.GetComponent<AttackTimer>().disappear();
            allProjectiles.Push(bullet);
        } else {
            /*var randomInt = Random.Range(1, 2);
            switch (randomInt) {
                case 1:
                    SoundManager.PlaySound("eyeballShot1");
                    break;
                case 2:
                    SoundManager.PlaySound("eyeballShot2");
                    break;
            }*/
            SoundManager.PlaySound("eyeballShot1");
            float offset = 0;
            float step = 0;
            if (max != 1) {
                offset = -maxangle/2;
                step = maxangle / (max - 1);
                
            }
            for (int i = 0; i < max; i++) {
                Vector3 temp = transform.position;
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                float offsetX = 0f;
                float offsetY = 0f;
                if (offset != 0) {
                    offsetX = max == 1 ? 0 : Mathf.Sin(offset * Mathf.Deg2Rad);
                    offsetY = max == 1 ? 0 : Mathf.Cos(offset * Mathf.Deg2Rad);
                }
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + offset;
                GameObject bullet = Instantiate(projectile[pattern], temp, transform.rotation) as GameObject; 
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                bullet.GetComponent<BossBullet>().enabled = true;
                //int projNum=0;
                //pattern = 4;
                switch (pattern) 
                {
                    case 0:
                        bullet.GetComponent<BossBullet>().setup("normal", bulletSpeed);
                        break;
                    case 1:
                        bullet.GetComponent<BossBullet>().setup("small", bulletSpeed);
                        break;
                    case 2:
                        bullet.GetComponent<BossBullet>().setup("homing", bulletSpeed);
                        break;
                    case 3:
                        bullet.GetComponent<BossBullet>().setup("accelerator", bulletSpeed);
                        break;
                    case 4:
                        bullet.GetComponent<BossBullet>().setup("bomb", bulletSpeed);
                        break;
                    case 5:
                        bullet.GetComponent<BossBullet>().setup("breakable", bulletSpeed);
                        break;
                    case 6:
                        bullet.GetComponent<BossBullet>().setup("littlemother", bulletSpeed);
                        break;
                    case 7:
                        bullet.GetComponent<BossBullet>().setup("tracker", bulletSpeed);
                        break;
                    case 8:
                        bullet.GetComponent<BossBullet>().setup("ricochet", bulletSpeed);
                        break;
                }
                allProjectiles.Push(bullet);

                offset += step;
            }
        }
        nextTimeShoot = Time.time + cd;
    }
}
