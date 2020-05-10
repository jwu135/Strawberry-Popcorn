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


    private int phase = 0;
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
            if (phase != 3) { // Main Projectile
                if (nextTimeShoot < Time.time) {
                    //Shoot(false,4,5,90);
                    Shoot(false,6);
                    nextTimeShoot = Time.time + shootCooldown;
                    //Debug.Log("Shot");
                }
            }
            if (phase == 3) { // Laser
                if (nextTimeShoot < Time.time) {
                    Shoot(true);
                    nextTimeShoot = Time.time + shootCooldown;
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
            
            if (phase==2||phase == 3) { // Ground spikes
                if (nextTime < Time.time) {
                        physicalPattern(2,0);
                    nextTime = Time.time + cooldown*2;
                }
            }
        }
    }
    void physicalPattern(int pattern = -1,int pos = -1) {
        if(pattern == -1) pattern = Random.Range(0, 3);
        if (pattern == 0 || pattern == 1) {
            pos = Random.Range(0, 3);
            Physical(pos,true);
        } else {
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
        nextTime = Time.time + cooldown;
    }
    void Physical(int pos,bool random = true,int place = 0) {
        
        GameObject physicalAttack = null;
        if (pos < 3) {
            Vector3 position = new Vector3(0, 0, 0);
            if (pos == 0) {
                int rand = Random.Range(0, spawnPointsX.Length);
                float randPos = Random.Range(-26.8f, 26.8f);
                if(random)
                    position = new Vector3(spawnPointsX[rand], -7.96f, 0);
                else
                    position = new Vector3(spawnPointsX[place], -7.96f, 0);
                //physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
                position = new Vector3(randPos, -7.96f, 0);
                physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
            } else if (pos == 1) {
                int rand = Random.Range(0, spawnPointsY.Length);
                if (random)
                    position = new Vector3(28.4f, spawnPointsY[rand], 0);
                else
                    position = new Vector3(28.4f, spawnPointsY[place], 0);
                Quaternion tempRotation = Quaternion.Euler(0, 0, 90);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
            } else if (pos == 2) {
                int rand = Random.Range(0, spawnPointsY.Length);
                if (random)
                    position = new Vector3(-28.5f, spawnPointsY[rand], 0);
                else
                    position = new Vector3(-28.5f, spawnPointsY[place], 0);
                
                Quaternion tempRotation = Quaternion.Euler(0, 0, 270);
                physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
                physicalAttack.transform.localScale = Vector3.Scale(physicalAttack.transform.localScale, new Vector3(-1,1)); 
            }
            
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else if (pos==3) {
            //SoundManager.PlaySound("bossAOE");
            physicalAttack = Instantiate(AoE, transform.Find("AoEAnchor").transform.position, AoE.transform.rotation) as GameObject;
            physicalAttack.transform.parent = transform.Find("AoEAnchor").transform;
            physicalAttack.GetComponent<Animator>().SetTrigger("Expand");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        } else {
            Debug.Log("Shooted");
            float posX = Random.Range(-14, 14);
            float posY = Random.Range(-7, 7);
            Vector2 position = new Vector2(posX, posY);
            Quaternion tempRotation = Quaternion.Euler(0, 0, 180);
            physicalAttack = Instantiate(hitObj, position, tempRotation) as GameObject;
            Vector3 direction = (Vector2)(player.transform.position - physicalAttack.transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            physicalAttack.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            physicalAttack.GetComponent<Animator>().SetTrigger("Spike");
            physicalAttack.GetComponent<AttackTimer>().disappear();
        }
        allProjectiles.Push(physicalAttack);
    }
    

    void Shoot(bool laser = false, int pattern = 0, int max = 1,float maxangle = 180)
    {
        if (laser) {
            SoundManager.PlaySound("bossLaser");
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(laserobj, temp, transform.rotation) as GameObject;
            bullet.transform.parent = transform;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            GetComponent<BossMovement>().setRotateable(false);
            //bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            bullet.GetComponent<AttackTimer>().setEnemy(transform);
            bullet.GetComponent<AttackTimer>().setTimer(1f);
            bullet.GetComponent<AttackTimer>().setHits(5f);
            bullet.GetComponent<AttackTimer>().disappear();
            allProjectiles.Push(bullet);
        } else {
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
                        bullet.GetComponent<BossBullet>().setup("tracker", bulletSpeed);
                        break;
                    case 3:
                        bullet.GetComponent<BossBullet>().setup("accelerator", bulletSpeed);
                        break;
                    case 4:
                        bullet.GetComponent<BossBullet>().setup("bomb", bulletSpeed);
                        break;
                    case 5:
                        bullet.GetComponent<BossBullet>().setup("large", bulletSpeed);
                        break;
                    case 6:
                        bullet.GetComponent<BossBullet>().setup("littlemother", bulletSpeed);
                        break;
                }
                allProjectiles.Push(bullet);

                offset += step;
            }
        }
        nextTime = Time.time + cooldown/5;
    }
}
