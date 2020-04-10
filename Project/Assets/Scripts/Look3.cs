﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look3 : MonoBehaviour
{
    [HideInInspector]
    public int usingController = 0; //mouse=0 controller=1;
    private GameObject[] armature;
    private UnityEngine.Transform armatureTransform;

    private GameObject player;
    private Transform playerTransform;
    // get player position
    private Vector3 playerPosition;
    private Camera m_camera;
    private float aimDeadzone;

    private Vector3 scaleVector;

    public PlayerCombat PlayerCombat;
    private bool switch1 = false;

    private void Start()
    {
        m_camera = Camera.main;
        player = GameObject.Find("Player");
        armature = GameObject.FindGameObjectsWithTag("ArmatureTag");
        armatureTransform = armature[0].GetComponent<Transform>();
        aimDeadzone = 0.9f;

        scaleVector = new Vector3(0.5f, 0.5f, 0.5f);
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        if (inputVector.magnitude > 0.3)
        {
            usingController = 1;
        }
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usingController = 0;
        }

        playerTransform = player.transform;
        playerPosition = m_camera.WorldToScreenPoint(playerTransform.position);

        if (usingController == 0 && !PlayerCombat.LASER) FaceMouse();
        else if (usingController == 0 && PlayerCombat.LASER)
        {
            FaceMouseSlower();
        }
        // else if (usingController == 0 && PlayerCombat.LASER && !switch1)
        //   {
        //       switch1 = true;
        //       FaceMouseSlow();
        //    }
        else if (usingController == 1) FaceController();


        //Debug.Log(usingController);

    }

    void FaceMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        armatureTransform.localScale = scaleVector;

        if (Input.mousePosition.x > playerPosition.x)
        {
            scaleVector.x = -0.5f;
            player.GetComponent<Movement>().direction = 0;
        }
        else if (Input.mousePosition.x < playerPosition.x)
        {
            scaleVector.x = 0.5f;
            player.GetComponent<Movement>().direction = 1;
        }

        //Debug.Log(transform.rotation);
    }

    //for now useless code
    public void FaceMouseSlower()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.AngleAxis(angle - 90, Vector3.forward), ((Time.deltaTime / 4) + (Time.deltaTime / 2)));
        //Debug.Log(transform.rotation);
    }

    public void FaceMouseSlow()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Debug.Log("yoooooooooooooooo");
        //  Debug.Log(transform.rotation);

        InvokeRepeating("FaceMouseSlowRepeat", 0.01f, 0.1f);


        //1s delay, repeat every 1s
        //StartCoroutine(size2());
    }

    private void FaceMouseSlowRepeat()
    {
        //yield return new WaitForSeconds(0.05f);
        if (!PlayerCombat.LASER)
        {
            CancelInvoke();
            switch1 = false;

        }
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Debug.Log(transform.rotation);
    }

    void FaceController()
    {
        Vector2 aimAxis = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));

        if (aimAxis.magnitude > aimDeadzone)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        /*else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }*/

        if (Input.GetAxis("Aim_Horizontal") > 0.1f && aimAxis.magnitude > aimDeadzone)
        {
            scaleVector.x = 0.5f;
            player.GetComponent<Movement>().direction = 1;
        }
        else if (Input.GetAxis("Aim_Horizontal") < -0.1f && aimAxis.magnitude > aimDeadzone)
        {
            scaleVector.x = -0.5f;
            player.GetComponent<Movement>().direction = 0;
        }

        armatureTransform.localScale = scaleVector;
    }
}