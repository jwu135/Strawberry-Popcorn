using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    int paused = 1;
    public void togglePause()
    {
        paused = 1 - paused;
    }
    // Update is called once per frame
    void Update()
    {
        if (paused == 0) {
            GameObject.Find("Player").GetComponent<HealthManager>().timer = 1;
            Time.timeScale = 0;
        }
    }
}
