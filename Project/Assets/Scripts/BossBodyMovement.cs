using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class BossBodyMovement : MonoBehaviour
{
    public static bool triggered;
    public float moveTowardsPlayer;
    public float distanceBeforeMoving;
    private float flipTime;

    private bool ableToMove = true;
    private bool grounded = false;

    // Gravity stuff
    private float dVelocity = 1f;
    private float gravity = 10f;
    private float upwardVel = 400f;
    private float uVeloctiy = 0;
    public bool downwardsGravity = true;
    private int downwardInt = 1;
    // Start is called before the first frame update
    void Start()
    {
        flipTime = Time.time + 2f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (downwardsGravity) {
            if (collision.gameObject.tag == "Floor") {
                grounded = true;
            }
        } else {
            if (collision.gameObject.tag == "Ceiling") {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (downwardsGravity) {
            if (collision.gameObject.tag == "Floor") {
                grounded = false;
            }
        } else {
            if (collision.gameObject.tag == "Ceiling") {
                grounded = false;
            }
        }

    }

    void Update()
    {
        if (Time.timeScale != 0f) {
            Move();
        }
    }

    private void Jump(){
        grounded = false;
        uVeloctiy = upwardVel;
        Vector2 temp = GetComponent<Rigidbody2D>().velocity;
        dVelocity = uVeloctiy*downwardInt;
        ableToMove = false;
        StartCoroutine("Dash");
    }

    private void Move()
    {
        Vector3 tempScale = transform.localScale;
        tempScale.y = Mathf.Abs(tempScale.y);
        if (downwardsGravity) {
            downwardInt = 1;
        } else {
            downwardInt = -1;
            tempScale.y = tempScale.y*-1f;
        }
        transform.localScale = tempScale;
        GameObject player =  GameObject.FindGameObjectWithTag("Player");
        
        if (flipTime < Time.time) {
            float rand = Random.Range(0f, 1f);
            if (rand > 0.8f) {
                //FlipFirst();
            } else  {
                if (grounded) {
                    //StartCoroutine("JumpDelay");
                }else
                    Debug.Log("Couldn't jump");
            }
            flipTime = Time.time + 6f;
        }

        if (!grounded) {
            dVelocity -= gravity*downwardInt;
            GetComponent<Rigidbody2D>().AddForce(transform.up * dVelocity);
        } else {
            dVelocity = 0;
            Vector2 temp = GetComponent<Rigidbody2D>().velocity;
            temp.y = dVelocity;
            GetComponent<Rigidbody2D>().velocity = temp;
        }


        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BossIdle") && ableToMove) {
            Vector2 temp = transform.transform.position;
            Vector2 tempPlay = player.transform.position;
            //if (Vector2.Distance(player.transform.position, transform.transform.position) > distanceBeforeMoving) {
            if (Mathf.Abs(player.transform.position.x - transform.transform.position.x) > distanceBeforeMoving) {
                if (tempPlay.x - temp.x > 0) {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(moveTowardsPlayer, 0));

                } else {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveTowardsPlayer, 0));

                }
            }
            if (tempPlay.x - temp.x > 0) {
                transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>()._armature.flipX = true;
            } else {
                transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>()._armature.flipX = false;
            }
        }
    }
    void FlipFirst()
    {
        StartCoroutine(Flip());
    }

    private IEnumerator JumpDelay()
    {
        transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("jumpprepare",1);
        ableToMove = false;
        yield return new WaitForSeconds(0.5f);
        ableToMove = true;
        transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("bossIdle");
        Jump();
    }

    private IEnumerator Dash()
    {
        while(dVelocity>0)
            yield return new WaitForSeconds(0.1f);
        Vector3 direction = (Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(direction.normalized*20000);
        ableToMove = true;
    }

    public IEnumerator Flip()
    {
        GameObject.Find("Mother's Eye").GetComponent<Boss>().setDamageable(false);
        if (transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.lastAnimationName != "hurt") {
            ableToMove = false;
            transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.timeScale = 2;
            transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("digging", 1);
            yield return new WaitForSeconds(0.5f);
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<Animator>().SetTrigger("Move");
            yield return new WaitForSeconds(0.5f);
            Vector3 temp = transform.position;
            if (Random.Range(0f, 1f) > 0.5f) temp.x = 10.34f;
            else
                temp.x = -10.34f;
            transform.position = temp;
            yield return new WaitForSeconds(0.5f);
            gameObject.AddComponent<Rigidbody2D>();
            GetComponent<Rigidbody2D>().gravityScale = 0f;
            GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
            GetComponent<Rigidbody2D>().freezeRotation = true;
            if (transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.lastAnimationName != "hurt") {
                transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.timeScale = -2;
                transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("digging", 1);
            }
            yield return new WaitForSeconds(0.5f);
            transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.timeScale = 1;
            if (transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.lastAnimationName != "hurt")
                transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("bossIdle");
            ableToMove = true;
        }
        GameObject.Find("Mother's Eye").GetComponent<Boss>().GetComponent<Boss>().setDamageable(true);
    }

}
