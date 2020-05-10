﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Henshin : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Camera cam2;
    public Camera cam3;
    public float speed;
    bool henshin = false;
    public SpriteRenderer test1;
    public SpriteRenderer test2;
    public SpriteRenderer test3;
    private int counter = 0;

    public GameObject Player1;
    public GameObject Player2;

    public bool manga = false;

    // public GameObject ui_tip;
    public PlayableDirector playableDirector;

    void Start()
    {
        //cam = Camera.main;
        Player2.SetActive(false);
        cam2.enabled = false;
        cam3.enabled = false;

    }

    void Update()
    {
        if(henshin == true)
        {
            //playableDirector.Play();
           
            if (cam2.orthographicSize < 3.87f)
            {
                cam2.transform.position = new Vector3(Mathf.Lerp(cam2.transform.position.x, -7.8f, speed), Mathf.Lerp(cam2.transform.position.y, 0.03f, speed), Mathf.Lerp(cam2.transform.position.z, -10.17f, speed));
                cam2.orthographicSize = Mathf.Lerp(cam2.orthographicSize, 3.879681f, speed);
            }
            //    Debug.Log(cam.orthographicSize);

            test1.color = new Color(1, 1, 1, 1);

            if (Input.GetButtonDown("Fire1") && counter == 1)
            {

                test3.color = new Color(1, 1, 1, 1);
                counter = 2;
            }
            else if (Input.GetButtonDown("Fire1") && counter == 0)
            {
                test2.color = new Color(1, 1, 1, 1);
                counter = 1;
            }
            else if (Input.GetButtonDown("Fire1") && counter == 2)
            {
                test1.enabled = false;
                test2.enabled = false;
                test3.enabled = false;
                counter = 3;
            }


        }
    }


    /*
    public void OnTriggerExit(Collider other)
    {
        ui_tip.gameObject.SetActive(false);
    }

    */


    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Use") && other.CompareTag("Player") && (henshin == false))
        {
            // ui_tip.gameObject.SetActive(true);
            
            playableDirector.Play();
            henshin = true;
            Player1.SetActive(false);
            cam3.enabled = true;
            cam2.enabled = true;
            cam.enabled = false;
            Player2.SetActive(true);
            cam2.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, 0, speed), Mathf.Lerp(cam.transform.position.y, 3.7f, speed), Mathf.Lerp(cam.transform.position.z, -10.17f, speed));
            cam2.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8.709762f, speed);

        }
    }

}