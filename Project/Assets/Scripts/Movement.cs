using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class Movement : MonoBehaviour
{
    private UnityArmatureComponent armatureComponent;
    public GameObject[] armatures;
    public UnityDragonBonesData things;
    [HideInInspector]
    public float direction = 0; // left is 0, right is 1;
    private float lastdirection = 0;

    void Start()
    {
        armatureComponent = GameObject.FindGameObjectWithTag("ArmatureTag").GetComponent<UnityArmatureComponent>();
        armatureComponent.animation.Play("Idle");
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
    void Update()
    {
        if (Time.timeScale != 0)
        {
            animate();
        }
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
        Debug.Log(armatureComponent.animationName);
        if (Input.GetKeyDown(KeyCode.W)) {

        }
        if (armatureComponent.animationName.Equals("dodge")&&armatureComponent.animation.isCompleted) {
            setPrimaryArmature(0);
        }
        if (Input.GetButtonDown("Roll") || armatureComponent.animation.lastAnimationName == "") {
            
            setPrimaryArmature(1);
            armatureComponent.animation.timeScale = 2f  ;
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
        } else  {
            if (Input.GetButtonDown("Jump")) {

                armatureComponent.animation.timeScale = 3;
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

            if (moving) {
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
