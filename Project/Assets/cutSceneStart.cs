using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class cutSceneStart : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector cutScene;
    public Camera cam;
    public Camera cam2;

    public bool counter = false;

    void start()
    {
        if(GlobalVariable.deathCounter < 1)
        {
            cam.enabled = true;
            cam2.enabled = false;
            cutScene.Play();
        }
        else
        {
            cam.enabled = false;
            cam2.enabled = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (cam2.enabled == true && counter == false)
        {
            cam.enabled = true;
            cam2.enabled = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            cam.enabled = false;
            cam2.enabled = true;
            counter = true;
        }

    }
}
