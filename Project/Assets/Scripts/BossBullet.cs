using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBulletObject
{
    public string type;
    public float bulletSpeed; // more like a multiplier
    public bool followPlayer;
    public Vector3 scale;
    public bool accelerate;
    public Color color;
    public bool AoEShot;
    // blossom shot


    public BossBulletObject(string t, float b, bool f, float scale, bool a, bool ae, Color? c = null)
    {
        this.type = t;
        this.bulletSpeed = b;
        this.followPlayer = f;
        this.scale = new Vector3(scale, scale, scale); // just gonna assume we want 1:1 scaling
        this.accelerate = a;
        this.AoEShot = ae;
        this.color = c ?? Color.white; // just temporary til we get new assets
    }
}
public class BossBullet : MonoBehaviour
{
    public GameObject Boss;
    public GameObject explodingEye;
    public GameObject AoE;
    private string type;
    private float bulletSpeed;
    private bool followPlayer;
    private bool accelerator = false;
    private bool AoEShot = false;
    private bool active = false;
    private List<BossBulletObject> bulletTypes = new List<BossBulletObject>();

    public void Awake() // start didn't get called early enough, as setup() was running before it.
    {
        // I'm coding them here instead of making them public in the inspector as it'd be a case similar to the dialogue system, which was an actual nightmare
        // Constructor stuff:
        // type, bulletSpeed, followsPlayer, scale, accelerates, spawnsAoE, color
        bulletTypes.Add(new BossBulletObject("normal", 1, false,1,false,false)); // normal shot with normal velocity
        bulletTypes.Add(new BossBulletObject("small", 1.5f, false,0.75f,false,false, new Color(0.603f,1,0))); // small shot with fastish velocity
        bulletTypes.Add(new BossBulletObject("tracker", 1f, true,1f,false, false, new Color(0f , 1f, 0.929f))); // tracker shot with normal velocity but follows player
        bulletTypes.Add(new BossBulletObject("accelerator", 1f, false,1f,true,false,new Color(1f,0.392f,0.529f))); // accelerator shot with increasing velocity
        bulletTypes.Add(new BossBulletObject("bomb", 1f, false,1f,false,true)); // accelerator shot with increasing velocity


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (AoEShot) {
            if (col.tag == "Floor") {
                spawnAoe();
                explode();
            }
        }
        if (col.tag == "Player") {
            if (AoEShot)
                spawnAoe();
            explode();
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
        GetComponent<SpriteRenderer>().color = temp.color;
        active = true;
    }
    void FixedUpdate()
    {
        if (active) {
            if (followPlayer) { // for trackers
                GameObject player = GameObject.FindWithTag("Player");
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Quaternion goalRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, Time.deltaTime * 60f);
            }
            if (accelerator) { // for accelerators
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed*3);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // there's probably a better way to do these two
                GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed * 60);  
            }
        }
        if (Vector2.Distance(transform.position, Boss.transform.position) > 50f) {
            explode();  
        }
    }
}
