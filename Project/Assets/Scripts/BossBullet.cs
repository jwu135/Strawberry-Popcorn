using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossBulletObject
{
    public string type;
    public float bulletSpeed; // more like a multiplier
    public bool followPlayer;
    public Vector3 scale;
    public bool accelerate;
    public Color color;
    public bool AoEShot;
    public bool breakable;
    public bool littlemother;
    public float cooldown = 1.25f;
    public bool ricochet;
    // blossom shot


    public BossBulletObject(string t, float b, bool f, float scale, bool a, bool ae, bool br, bool lm,bool ri)
    {
        this.type = t;
        this.bulletSpeed = b;
        this.followPlayer = f;
        this.scale = new Vector3(scale, scale, scale); // just gonna assume we want 1:1 scaling
        this.accelerate = a;
        this.AoEShot = ae;
        this.breakable = br;
        this.littlemother = lm;
        this.ricochet = ri;
    }
}
public class BossBullet : MonoBehaviour
{
    public GameObject Boss;
    private GameObject player;
    public GameObject explodingEye;
    public GameObject AoE;
    private string type;
    private float bulletSpeed;
    private bool followPlayer;
    private bool accelerator = false;
    private bool AoEShot = false;
    private bool breakable = false;
    private bool littlemother = false;
    private bool ricochet = false;
    private float cooldown;
    private bool active = false;
    private Vector2 origDir;
    private List<BossBulletObject> bulletTypes = new List<BossBulletObject>();

    public void Awake() // start didn't get called early enough, as setup() was running before it.
    {
        // I'm coding them here instead of making them public in the inspector as it'd be a case similar to the dialogue system, which was an actual nightmare
        // Constructor stuff:
        // type, bulletSpeed, followsPlayer, scale, accelerates, spawnsAoE, breakable, littlemother, ricochet
        bulletTypes.Add(new BossBulletObject("normal", 1, false, 1, false, false, false,false,false)) ; // normal shot with normal velocity
        
        bulletTypes.Add(new BossBulletObject("small", 1.5f, false,0.75f,false,false, false,false,false)); // small shot with fastish velocity
        
        bulletTypes.Add(new BossBulletObject("homing", 1f, true,1f,false, false, false,false,false)); // tracker shot with normal velocity but follows player
        
        bulletTypes.Add(new BossBulletObject("accelerator", 1f, false,1f,true,false,false,false,false)); // accelerator shot with increasing velocity
        
        bulletTypes.Add(new BossBulletObject("bomb", 1f, false,1f,false,true,false,false,false)); // bomb shot that spawns AoE
        
        bulletTypes.Add(new BossBulletObject("breakable", 1f, false,1,false,false,true,false,false)); // accelerator shot with increasing velocity
        
        bulletTypes.Add(new BossBulletObject("littlemother", 0.4f, false,1,false,false,false,true,false)); // little mother shot that shoots at the player

        bulletTypes.Add(new BossBulletObject("tracker", .75f, true,1.25f,false, false, true,false,true)); // tracker shot with normal velocity but follows player
        
        bulletTypes.Add(new BossBulletObject("ricochet", 1f, false,1f,false, false, true,false,true)); // tracker shot with normal velocity but follows player
        
        // if this gets too out of hand, it might be better to just use a csv file.
        player = GameObject.Find("Player");


    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (AoEShot) {
            if (col.tag == "Floor") {
                spawnAoe();
                SoundManager.PlaySound("Explosion");
                destroy();
            }
        }
        if (col.gameObject.name == "Floor") {
            if(type!="ricochet")
                explode();
            else {
                Vector3 tempRot = transform.eulerAngles; // Quaternions are not fun, so I'm just gonna stick with eulerAngles
                if(col.transform.eulerAngles.z==0)
                    tempRot.z = 360 - tempRot.z;
                else
                    tempRot.z = 180 - tempRot.z;
                transform.eulerAngles = tempRot;
            }
            
        }
        if (col.tag == "Player") {
            if (AoEShot) {
                spawnAoe();
                destroy();
            } else
                explode();
        }
      
        if (breakable) {
            Debug.Log("can be broken");
            if(col.tag == "normalAttack1"|| col.tag == "chargeAttack1") {
                Debug.Log("broke");
                col.gameObject.GetComponent<Bullet>().explode();
                explode();
            }
        }
    }
    void spawnAoe()
    {
        GameObject temp = Instantiate(AoE, transform.position, transform.rotation) as GameObject;
        temp.transform.localScale = transform.localScale;
    }
    void explode()
    {
        GameObject temp = Instantiate(explodingEye, transform.position, transform.rotation) as GameObject;
        temp.transform.localScale = transform.localScale;
        destroy();
    }
    public void destroy()
    {
        Destroy(gameObject);
    }

   
    public void setup(string t,float b)
    {
        BossBulletObject temp = bulletTypes[0]; // assume that the bullet is the default one
        foreach(BossBulletObject bulletTemp in bulletTypes) {
            if (bulletTemp.type.Equals(t)) {
                temp = bulletTemp;
                break;
            }
        }
        type = temp.type;
        bulletSpeed = temp.bulletSpeed*b;
        followPlayer = temp.followPlayer;
        transform.localScale = Vector3.Scale(transform.localScale, temp.scale); // multiplies the default scale with the scale modifier, in case we change it in the inspector for whatever reason. 
        accelerator = temp.accelerate;
        AoEShot = temp.AoEShot;
        breakable = temp.breakable;
        littlemother = temp.littlemother;
        ricochet = temp.ricochet;
        cooldown = temp.cooldown;
        origDir = (Vector2)(GameObject.Find("Player").transform.position - transform.position).normalized; ;
        active = true;

        if (type.Equals("bomb"))
            SoundManager.PlaySound("bombAfall");
    }
    void FixedUpdate()
    {
        if (active) {
            if (GetComponent<Rigidbody2D>().velocity.x>0) {
                GetComponent<SpriteRenderer>().flipY = false;
            } else {
                GetComponent<SpriteRenderer>().flipY = true;
            }
            if (followPlayer) { // for trackers && homing
                //GameObject player = GameObject.FindWithTag("Player");
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Quaternion goalRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if(type=="homing")
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, Time.deltaTime * 60f);
                if(type=="tracker")
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, Time.deltaTime * 180f);
            }
            if (accelerator) { // for accelerators
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed * 3);
            } else if (littlemother) {
                GetComponent<Rigidbody2D>().velocity = origDir*bulletSpeed;
            } else{
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // there's probably a better way to do these two
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed * 60);  
            }
            if (littlemother) {
                Vector3 directionObj = (Vector2)(GameObject.Find("Player").transform.position - transform.position).normalized;
                float angleObj = Mathf.Atan2(directionObj.y, directionObj.x) * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(0, 0, angleObj)), Time.time / 100);
                cooldown -= Time.deltaTime;
                if (cooldown <= 0) {
                    BossShoot bs = GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>();
                    GameObject proj = bs.projectile[0];
                    Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                    float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                    GameObject bullet = Instantiate(proj, transform.position, transform.rotation) as GameObject;
                    bullet.GetComponent<BossBullet>().setup("breakable", bs.bulletSpeed);
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    bullet.GetComponent<BossBullet>().enabled = true;
                    bs.addToStack(bullet);
                    cooldown = 1.25f;
                }
                
            }
            
        }
        if (Vector2.Distance(transform.position, Boss.transform.position) > 50f) {
            explode();  
        }
    }
}
