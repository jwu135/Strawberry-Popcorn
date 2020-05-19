using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject[] canvas;
    int paused = 1;
    private void Awake()
    {
        Time.timeScale = 1; //this should fix the game freezing on multiple playthroughs
    }
    public void togglePause()
    { //Playercombat and look2 still need to check time scale.
        paused = 1 - paused;
        canvas[0].SetActive(!Convert.ToBoolean(paused));
    }
    // Update is called once per frame
    void Update()
    {
        if (paused == 0) {
            if (GameObject.Find("Player") != null)
                GameObject.Find("Player").GetComponent<HealthManager>().timer = 1;
            Time.timeScale = 0;
        }
    }
}
