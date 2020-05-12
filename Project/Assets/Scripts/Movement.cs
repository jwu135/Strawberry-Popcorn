
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
    private float lastShot = 0f;
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

        
        //Debug.Log("Roll Frame:" + platMove.getRollingFrame() + "Stick Mag: " + platMove.getStickInput().magnitude);

    }

    IEnumerator afterImageStop()
    {
        yield return new WaitForSeconds(.3f);
        GetComponent<ParticleSystem>().Stop();
    }

    public void setTime()
    {
        lastShot = 2f;
    }

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

    void animate()
    {

        lastShot -= Time.deltaTime;
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