﻿
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Movement : MonoBehaviour
{
    public GameObject airJumpEffect;
    public Material next;
    private UnityArmatureComponent armatureComponent;
    public GameObject[] armatures;
    public UnityDragonBonesData things;
    [HideInInspector]
    public float direction = 0; // left is 0, right is 1;
    private float lastdirection = 0;
    private int primaryIndex = 0;
    private int primaryDodgeIndex = 1;
    private int primaryArmedIndex = 2;
    private float lastShot = 0f;
    public bool rollTime = false;
    public bool jumped = false;
    private bool step1Played = false;
    private bool step2Played = false;
    PlatformMovementPhys platMove;
    void Start()
    {
        armatureComponent = GameObject.FindGameObjectWithTag("ArmatureTag").GetComponent<UnityArmatureComponent>();
        armatureComponent.animation.Play("Idle");
        platMove = GetComponent<PlatformMovementPhys>();
    }

    public void airJump()
    {
        Vector3 temp = transform.position;
        temp.y -= -2f; // somewhat arbitrary number for offset
        GameObject airJump = Instantiate(airJumpEffect, transform.position, transform.rotation) as GameObject;
    }

    public void setPrimaryIndex(int index)
    {
        primaryIndex = index;
        primaryDodgeIndex = index + 1;
        primaryArmedIndex = index + 2;
    }
    public int getPrimaryIndex()
    {
        return primaryIndex;
    }
    public void setPrimaryArmature(int index)
    {
        foreach (GameObject i in armatures) {
            i.SetActive(false);
        }
        armatures[index].SetActive(true);
        setArmature();
    }

    public void setArmature()
    {
        armatureComponent = GameObject.FindGameObjectWithTag("ArmatureTag").GetComponent<UnityArmatureComponent>();
        transform.Find("Arm").transform.GetComponent<Look>().setArmature();
    }
    public UnityArmatureComponent getArmature()
    {
        return armatureComponent;
    }
    public int findIndex(string name)
    {
        for (int i = 0; i < armatures.Length; i++) {
            if (string.Equals(armatures[i].name, name))
                return i;
        }
        return 0;
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            animate();
        }
    }

    IEnumerator afterImageStop()
    {
        while(armatureComponent.animationName.Equals("dodge") && !armatureComponent.animation.isCompleted)
            yield return new WaitForSeconds(.1f);

        GetComponent<ParticleSystem>().Stop();
    }

    public void setTime()
    {
        lastShot = 2f;
    }

    private void armedSwap(bool toArmed)
    {
        if (toArmed) {
            setPrimaryArmature(primaryArmedIndex);
        } else {
            setPrimaryArmature(primaryIndex);
        }
    }

    // OCT (override central time)
    // there's probably a less spacing consuming way of handling these overrides, but I'm too tired for that
    public void flip(float dir, float scale, Look Arm = null)
    {
        if (lastShot > 0f) {
            direction = dir;
            Arm.setScaleVector(scale);
        } else {
            if (Input.GetAxisRaw("Horizontal") < 0) {
                direction = 1;
                Arm.setScaleVector(0.5f);
            } else if (Input.GetAxisRaw("Horizontal") > 0){
                direction = 0;
                Arm.setScaleVector(-0.5f);
            }
        }
    }
    public void flip(float dir, float scale, LookSimple Arm = null)
    {
        if (lastShot > 0f) {
            direction = dir;
            Arm.setScaleVector(scale);
        } else {
            if (Input.GetAxisRaw("Horizontal") < 0) {
                direction = 1;
                Arm.setScaleVector(0.5f);
            } else if (Input.GetAxisRaw("Horizontal") > 0){
                direction = 0;
                Arm.setScaleVector(-0.5f);
            }
        }
    }
    public void flip(float dir, float scale, Look2 Arm = null)
    {
        if (lastShot > 0f) {
            direction = dir;
            Arm.setScaleVector(scale);
        } else {
            if (Input.GetAxisRaw("Horizontal") < 0) {
                direction = 1;
                Arm.setScaleVector(0.5f);
            } else if (Input.GetAxisRaw("Horizontal") > 0){
                direction = 0;
                Arm.setScaleVector(-0.5f);
            }
        }
    }
    public void flip(float dir, float scale, Look3 Arm = null)
    {
        if (lastShot > 0f) {
            direction = dir;
            Arm.setScaleVector(scale);
        } else {
            if (Input.GetAxisRaw("Horizontal") < 0) {
                direction = 1;
                Arm.setScaleVector(0.5f);
            } else if (Input.GetAxisRaw("Horizontal") > 0){
                direction = 0;
                Arm.setScaleVector(-0.5f);
            }
        }
    }

    void animate()
    {

        lastShot -= Time.deltaTime;
        bool movedLast = armatureComponent.animation.lastAnimationName.Equals("Running") || armatureComponent.animation.lastAnimationName.Equals("backRunning");
        string lastAnimation = armatureComponent.animation.lastAnimationName;
        if (armatureComponent.animationName.Equals("dodge") && armatureComponent.animation.isCompleted) {
            setPrimaryArmature(primaryIndex);
            /*  if (lastShot > 0)
                setPrimaryArmature(primaryIndex);
            else {
                setPrimaryArmature(primaryArmedIndex);
            }
            if (movedLast) {
                armatureComponent.animation.Play(lastAnimation);
            }
        } else if(!armatureComponent.animationName.Equals("dodge")){
            if (lastShot > 0) {
                setPrimaryArmature(primaryIndex);
                if (movedLast) {
                    armatureComponent.animation.Play(lastAnimation);
                }
            } else {
                setPrimaryArmature(primaryArmedIndex);
                if (movedLast) {
                    armatureComponent.animation.Play(lastAnimation);
                }
            }
        */
        }
        if (armatureComponent.animation.GetStates()[0].name.Equals("Idle")) {
            step1Played = false;
            step2Played = false;
        }
        if (GetComponent<Rigidbody2D>().velocity.y == 0) {
            // this took a lot of time...
            if (armatureComponent.animation.GetStates()[0].name.Equals("Running")) {
                double timeSpent = Math.Round(armatureComponent.animation.GetStates()[0].currentTime, 1);
                if (timeSpent >= 0 && timeSpent <= .4 && !step1Played) {
                    step1Played = true;
                    SoundManager.PlaySound("Step1");
                } else if (timeSpent >= 1 && timeSpent <= 1.3 && !step2Played) {
                    step2Played = true;
                    SoundManager.PlaySound("Step2");
                }
                if (armatureComponent.animation.isCompleted) {
                    step1Played = false;
                    step2Played = false;
                }
            }
            if (armatureComponent.animation.GetStates()[0].name.Equals("backRunning")) {
                double timeSpent = Math.Round(armatureComponent.animation.GetStates()[0].currentTime, 1);
                if (timeSpent >= 0.3 && timeSpent <= .6 && !step1Played) {
                    step1Played = true;
                    SoundManager.PlaySound("Step1");
                } else if (timeSpent >= 1.1 && timeSpent <= 1.4 && !step2Played) {
                    step2Played = true;
                    SoundManager.PlaySound("Step2");
                }
                if (armatureComponent.animation.isCompleted) {
                    step1Played = false;
                    step2Played = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            UnityEngine.Transform temp = GameObject.Find("crosshairAttack").transform;
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>().Shoot(obj:temp);
        }
        if (rollTime) // set in PlatformMovementPhys.cs
        {
            SoundManager.PlaySound("playerDodge");
            setPrimaryArmature(primaryDodgeIndex);
            armatureComponent.animation.timeScale = 2f;
            GetComponent<ParticleSystem>().Play();
            if(direction>0)
                GetComponent<ParticleSystemRenderer>().flip = new Vector3(0f, 0f, 0f);
            else
                GetComponent<ParticleSystemRenderer>().flip = new Vector3(1f, 0f, 0f);
            if (Input.GetAxisRaw("Horizontal") < 0) {
                if (direction > 0) // direction set in Look.cs
                    armatureComponent.animation.Play("dodge", 1);
                else
                    armatureComponent.animation.Play("dodgeback", 1);
            } else if (Input.GetAxisRaw("Horizontal") > 0) {
                if (direction > 0)
                    armatureComponent.animation.Play("dodgeback", 1);
                else
                    armatureComponent.animation.Play("dodge", 1);
            }
            StartCoroutine("afterImageStop");
            rollTime = false;
        } else {
            if (jumped) {
                armatureComponent.animation.timeScale = 2;
                armatureComponent.animation.Play("Jumping", 1);
                jumped = false;
            }
            float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.
            bool moving = mag > 0.15f && (GetComponent<Rigidbody2D>().velocity.x > 0 || (GetComponent<Rigidbody2D>().velocity.x < 0));
            bool last = armatureComponent.animation.lastAnimationName == "Running" || armatureComponent.animation.lastAnimationName == "backRunning";


            /* direction 1 is left of player
                direction 0
                Right = backRunning, left = Running

                direction 1
                Right = Running, left = backRunning





            */


            Vector2 pos = transform.Find("Arm").transform.localPosition;
            if (direction > 0) 
                pos.x = 0.21f;
            else
                pos.x = -0.185f;

            transform.Find("Arm").transform.localPosition = pos;


            if (armatureComponent.animation.lastAnimationName == "Jumping" && GetComponent<Rigidbody2D>().velocity.y == 0) {
                //armatureComponent.animation.timeScale = 8;
                //armatureComponent.animation.Play("FALLING", 1);
                //armatureComponent.animation.timeScale = 5;
                armatureComponent.animation.Play("Idle",1);
            } else if (moving&&armatureComponent.animationName!="Jumping"&&armatureComponent.animation.lastAnimationName!="Jumping") {
                float speed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x);
                float maxMoveSpeed = GetComponent<PlayerController>().getStat("moveSpeed");
                speed = (speed / maxMoveSpeed) * 2.2f; // normalize the speed between 0 and 2.2f
                speed = Mathf.Clamp(speed, 1f, 2.2f);
                armatureComponent.animation.timeScale = speed; // for some reason it just worked this time 

                // if the player is doing literally anything other than Running right now, then allow them to run
                
                if(armatureComponent.animation.lastAnimationName == "Idle"|| armatureComponent.animationName == "backRunning" || direction != lastdirection || (armatureComponent.animation.isCompleted&& armatureComponent.animation.lastAnimationName == "Running")) {
                    if (direction > 0&& Input.GetAxisRaw("Horizontal") < 0||direction<=0&& Input.GetAxisRaw("Horizontal") > 0) {
                        armatureComponent.animation.Play("Running", 1);
                    }
                }
                // if the player is doing literally anything other than backRunning right now, then allow them to run
                if (armatureComponent.animation.lastAnimationName == "Idle" || armatureComponent.animationName == "Running" || direction != lastdirection || (armatureComponent.animation.isCompleted && armatureComponent.animation.lastAnimationName == "backRunning")) {
                    if (direction <= 0 && Input.GetAxisRaw("Horizontal") < 0 || direction > 0 && Input.GetAxisRaw("Horizontal") > 0) {
                        armatureComponent.animation.Play("backRunning", 1);
                    }
                }



                if (direction != lastdirection) {
                    lastdirection = direction;
                }


            } else if (armatureComponent.animation.isCompleted || last) {
            //} else if (GetComponent<Rigidbody2D>().velocity.y==0) {
                armatureComponent.animation.timeScale = 2;
                armatureComponent.animation.Play("Idle");
            }
        }
    }
}