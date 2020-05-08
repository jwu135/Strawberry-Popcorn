using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossBulletObject
{
    public string type;
    public float bulletSpeed; // more like a multiplier
    public bool followPlayer;
    public Vector3 scale;
    public BossBulletObject(string t, float b,bool f,float scale)
    {
        this.type = t;
        this.bulletSpeed = b;
        this.followPlayer = f;
        this.scale = new Vector3(scale, scale, scale); // just gonna assume we want 1:1 scaling
    }
}
public class BossBullet : MonoBehaviour
{
    public GameObject Boss;
    public GameObject explodingEye;
    private string type;
    private float bulletSpeed;
    private bool followPlayer;
    private bool active = false;
    private List<BossBulletObject> bulletTypes = new List<BossBulletObject>();

    public void Awake() // start didn't get called early enough, as setup() was running before it.
    {
        // I'm coding them here instead of making them public in the inspector as it'd be a case similar to the dialogue system, which was an actual nightmare
        // Constructor stuff:
        // type, bulletSpeed, followsPlayer, scale
        bulletTypes.Add(new BossBulletObject("normal", 1, false,1)); // normal shot with normal velocity
        bulletTypes.Add(new BossBulletObject("small", 1.5f, false,0.75f)); // normal shot with normal velocity
        bulletTypes.Add(new BossBulletObject("tracker", 1f, true,1f)); // tracker shot with normal velocity but follows player


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            explode();
        }
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
        
        
        active = true;
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    // Update is called once per frame
    void lookAround()
    {
        if (active) {
            if (followPlayer) {
                GameObject player = GameObject.FindWithTag("Player");
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
                Quaternion goalRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, Time.deltaTime * 60f);
                Debug.Log(GetComponent<Rigidbody2D>().rotation);
            }
            //GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // there's probably a better way to do these two
            GetComponent<Rigidbody2D>().AddForce(transform.right * bulletSpeed*60);

        }
        if (Vector2.Distance(transform.position, Boss.transform.position) > 50f) {
            explode();  
        }
    }
}
