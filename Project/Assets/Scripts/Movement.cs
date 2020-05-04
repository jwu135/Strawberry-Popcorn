﻿
using System.Collections;
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

        
        Debug.Log("Roll Frame:" + platMove.getRollingFrame() + "Stick Mag: " + platMove.getStickInput().magnitude);

    }

    IEnumerator afterImageStop()
    {
        yield return new WaitForSeconds(.3f);
        GetComponent<ParticleSystem>().Stop();
    }

    void animate()
    {
        /* DO NOT DELETE. Gonna look more at this later
        if (Input.GetKeyDown(KeyCode.L)) {
            
            armatureComponent.unityData = things;
            armatureComponent.GetComponent<UnityCombineMeshs>().BeginCombineMesh();
            Debug.Log("Changed");
        }
        */
        //This comes from having a seperate armature for the dodge. 
        if (Input.GetKeyDown(KeyCode.K)) {
            armatureComponent.unityData.textureAtlas[0].material = next;
            armatureComponent.GetComponent<UnityCombineMeshs>().BeginCombineMesh();
        }
        if (armatureComponent.animationName.Equals("dodge") && armatureComponent.animation.isCompleted) {
            setPrimaryArmature(primaryIndex);
        }
        // animation plays when you roll in place
        if (((Input.GetButton("Roll") == true) || (Input.GetAxis("Roll") < 0)) && (platMove.getRollingFrame() < 2 && platMove.getStickInput().magnitude > 0))
        {// add function call to PlatformMovementPhys
            Debug.Log("Attempted to animate roll");
            setPrimaryArmature(1);
            armatureComponent.animation.timeScale = 2f;
            GetComponent<ParticleSystem>().Play();
            if(direction>0)
                GetComponent<ParticleSystemRenderer>().flip = new Vector3(0f, 0f, 0f);
            else
                GetComponent<ParticleSystemRenderer>().flip = new Vector3(1f, 0f, 0f);
            //ParticleSystem.EmissionModule pm = armatureComponent.transform.GetComponent<ParticleSystem>().emission;
            //pm.enabled = true;
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
        } else {
            if (Input.GetButtonDown("Jump")) {

                armatureComponent.animation.timeScale = 1;
                armatureComponent.animation.Play("Jumping", 1);
            }
            float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.
            bool moving = mag > 0.15f && (Input.GetAxisRaw("Horizontal") > 0 || (Input.GetAxisRaw("Horizontal") < 0));
            bool last = armatureComponent.animation.lastAnimationName == "Running" || armatureComponent.animation.lastAnimationName == "backRunning";
            Vector2 pos = transform.Find("Arm").transform.localPosition;
            if (direction > 0) {
                pos.x = 0.21f;
            } else
                pos.x = -0.185f;
            transform.Find("Arm").transform.localPosition = pos;

            /*if (moving) {
                if (Input.GetAxisRaw("Horizontal") < 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection) {
                        if (direction > 0)
                            armatureComponent.animation.Play("Running", 1);
                        else
                            armatureComponent.animation.Play("backRunning", 1);
                    }
                }
                if (Input.GetAxisRaw("Horizontal") > 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection)
                        if (direction > 0)
                            armatureComponent.animation.Play("backRunning", 1);
                        else
                            armatureComponent.animation.Play("Running", 1);
                }
                if (direction != lastdirection) {

                    lastdirection = direction;
                }
            }*/
            //if(armatureComponent.animation.lastAnimationName == "Jumping"&& armatureComponent.animation.isCompleted || (armatureComponent.animation.lastAnimationName == "FALLING"&&GetComponent<Rigidbody2D>().velocity.y<0)) {
            if(armatureComponent.animation.lastAnimationName == "Jumping"&& GetComponent<Rigidbody2D>().velocity.y>-40) {
                //armatureComponent.animation.timeScale = 8;
                //armatureComponent.animation.Play("FALLING", 1);
                armatureComponent.animation.timeScale = 2;
                armatureComponent.animation.Play("Idle");
            } else if (moving) {
                if (Input.GetAxisRaw("Horizontal") < 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection) {
                        if (direction > 0)
                            armatureComponent.animation.Play("Running", 1);
                        else
                            armatureComponent.animation.Play("backRunning", 1);
                    }
                }
                if (Input.GetAxisRaw("Horizontal") > 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection)
                        if (direction > 0)
                            armatureComponent.animation.Play("backRunning", 1);
                        else
                            armatureComponent.animation.Play("Running", 1);
                }
                if (direction != lastdirection) {

                    lastdirection = direction;
                }
            } else if (armatureComponent.animation.isCompleted || last) {
                armatureComponent.animation.timeScale = 2;
                armatureComponent.animation.Play("Idle");

            }
        }
    }
}
/*using System.Collections;
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
    void Start()
    {
        armatureComponent = GameObject.FindGameObjectWithTag("ArmatureTag").GetComponent<UnityArmatureComponent>();
        armatureComponent.animation.Play("Idle");
    }

    public void airJump()
    {
        Vector3 temp = transform.position;
        temp.y -= 1.2f; // somewhat arbitrary number for offset
        GameObject airJump = Instantiate(airJumpEffect, temp, transform.rotation) as GameObject;
    }

    public void setPrimaryIndex(int index)
    {
        primaryIndex = index;
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

    void animate()
    {
        //This comes from having a seperate armature for the dodge. 

        if (armatureComponent.animationName.Equals("dodge") && armatureComponent.animation.isCompleted) {
            setPrimaryArmature(primaryIndex);
        }
        if (Input.GetButtonDown("Roll") || armatureComponent.animation.lastAnimationName == "") {

            setPrimaryArmature(1);
            armatureComponent.animation.timeScale = 2f;
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
        } else {
            if (Input.GetButtonDown("Jump")) {

                armatureComponent.animation.timeScale = 3;
                armatureComponent.animation.Play("Jumping", 1);
            }
            float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.
            bool moving = mag > 0.15f && (Input.GetAxisRaw("Horizontal") > 0 || (Input.GetAxisRaw("Horizontal") < 0));
            //bool moving = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 0;
            bool falling = GetComponent<Rigidbody2D>().velocity.y != 0; // add last velocity to compare
            bool last = armatureComponent.animation.lastAnimationName == "Running" || armatureComponent.animation.lastAnimationName == "backRunning";
            Vector2 pos = transform.Find("Arm").transform.localPosition;
            if (direction > 0) {
                pos.x = 0.21f;
            } else
                pos.x = -0.185f;
            transform.Find("Arm").transform.localPosition = pos;
            //Debug.Log(GetComponent<Rigidbody2D>().velocity);
            if (falling) {
                //Debug.Log(GetComponent<Rigidbody2D>().velocity);
            } else if (moving) {
                Debug.Log(armatureComponent.animation.timeScale);
                if (armatureComponent.animation.lastAnimationName != "dodge" || armatureComponent.animation.lastAnimationName != "dodgeback")
                    armatureComponent.animation.timeScale = Mathf.Clamp(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) / 6,1.0f,2f);
                //if (Input.GetAxisRaw("Horizontal") < 0) {
                //if (GetComponent<Rigidbody2D>().velocity.x < 0) {
                if (Input.GetAxisRaw("Horizontal") < 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection) {
                        if (direction > 0)
                            armatureComponent.animation.Play("Running", 1);
                        else
                            armatureComponent.animation.Play("backRunning", 1);
                    }
                }
                //if (GetComponent<Rigidbody2D>().velocity.x > 0) {
                if (Input.GetAxisRaw("Horizontal") > 0) {
                    if (armatureComponent.animation.isCompleted || armatureComponent.animation.lastAnimationName == "Idle" || direction != lastdirection)
                        if (direction > 0)
                            armatureComponent.animation.Play("backRunning", 1);
                        else
                            armatureComponent.animation.Play("Running", 1);
                }
                if (direction != lastdirection) {

                    lastdirection = direction;
                }

            //} else if (armatureComponent.animation.isCompleted || last) {
            } else if (armatureComponent.animation.lastAnimationName != "Idle") {
                armatureComponent.animation.timeScale = 2;
                armatureComponent.animation.Play("Idle");

            }
        }

    }
}*/
