using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyMovement : MonoBehaviour
{
    public static bool triggered;
    public float moveTowardsPlayer;
    public float distanceBeforeMoving;
    private float flipTime;

    private bool ableToMove = true;
    private bool grounded = false;

    // Gravity stuff
    private float dVelocity = 0.01f;
    private float gravity = 0.001f;
    private float upwardVel = -0.1f;
    private float uVeloctiy = 0;

    // Start is called before the first frame update
    void Start()
    {
        flipTime = Time.time + 2f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            grounded = false;
        }
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    void lookAround()
    {
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        
        if (flipTime < Time.time) {
            float rand = Random.Range(0f, 1f);
            if (rand > 0.8f) {
                FlipFirst();
            } else  {
                if (grounded) {
                    /* working jump
                    grounded = false;
                    uVeloctiy = upwardVel;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0,upwardVel);*/

                    grounded = false;
                    uVeloctiy = upwardVel;
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0,upwardVel);
                    ableToMove = false;
                    StartCoroutine("Dash");
                    /*
                    Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                    bullet.GetComponent<BossBullet>().enabled = true;
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed; 
                    */
                }
            }
            flipTime = Time.time + 6f;
        }
 
            if (!grounded) {
                dVelocity += gravity + uVeloctiy;
                uVeloctiy /= 200;
                Vector2 temp = transform.transform.position;
                temp.y -= dVelocity;
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, temp.y);
                transform.transform.position = temp;
            } else {
                dVelocity = 0;
            }

        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BossIdle")&&ableToMove) {
            if (Vector2.Distance(player.transform.position, transform.transform.position) > distanceBeforeMoving) {
                Vector2 temp = transform.transform.position;
                Vector2 tempPlay = player.transform.position;
                if (tempPlay.x - temp.x > 0)
                    temp.x += moveTowardsPlayer;
                else
                    temp.x -= moveTowardsPlayer;
                transform.transform.position = temp;
            }
        }

    }
    void FlipFirst()
    {
        StartCoroutine(Flip());
    }

    private IEnumerator Dash()
    {
        //Debug.Log("Gonna do the thing");
        yield return new WaitForSeconds(0.3f);
        //Debug.Log("Did the thing");
        Vector3 direction = (Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 500;
        ableToMove = true;
    }

    public IEnumerator Flip()
    {
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<Animator>().SetTrigger("Move");
        yield return new WaitForSeconds(0.5f);
        Vector3 temp = transform.position;
        temp.x *= -1;
        transform.position = temp;
        yield return new WaitForSeconds(0.5f);   
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().interpolation= RigidbodyInterpolation2D.Interpolate;
        yield return 0; 
    }

}
