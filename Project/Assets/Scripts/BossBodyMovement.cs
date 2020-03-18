﻿using System.Collections;
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
    private float dVelocity = 1f;
    private float gravity = 10f;
    private float upwardVel = 400f;
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
    void FixedUpdate()
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
            if (rand > 1f) {
                FlipFirst();
            } else  {
                if (grounded) {
                    grounded = false;
                    uVeloctiy = upwardVel;
                    Vector2 temp = GetComponent<Rigidbody2D>().velocity;
                    dVelocity = uVeloctiy;
                    ableToMove = false;
                    StartCoroutine("Dash");
                }else
                    Debug.Log("Couldn't jump");
            }
            flipTime = Time.time + 3f;
        }

        if (!grounded) {
            dVelocity -= gravity;
            GetComponent<Rigidbody2D>().AddForce(transform.up * dVelocity);
        } else {
            dVelocity = 0;
            Vector2 temp = GetComponent<Rigidbody2D>().velocity;
            temp.y = dVelocity;
            GetComponent<Rigidbody2D>().velocity = temp;
        }


        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BossIdle") && ableToMove) {
            Vector2 temp = transform.transform.position;
            Vector2 scale = transform.transform.localScale;
            Vector2 tempPlay = player.transform.position;
            if (Vector2.Distance(player.transform.position, transform.transform.position) > distanceBeforeMoving) {
                if (tempPlay.x - temp.x > 0) {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(moveTowardsPlayer, 0));

                } else {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-moveTowardsPlayer, 0));

                }
            }
            if (tempPlay.x - temp.x > 0) {
                scale.x = Mathf.Abs(scale.x) * -1;
            } else {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
    }
    void FlipFirst()
    {
        StartCoroutine(Flip());
    }

    private IEnumerator Dash()
    {
        while(dVelocity>0)
            yield return new WaitForSeconds(0.1f);
        Vector3 direction = (Vector2)(GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(direction.normalized*10000);
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
