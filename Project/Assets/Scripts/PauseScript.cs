using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject[] canvas;
    int paused = 1;
    GameObject cursor;
    public GameObject menu;
    private void Awake()
    {
        Time.timeScale = 1; //this should fix the game freezing on multiple playthroughs
    }
    public void togglePause()
    { //Playercombat and look2 still need to check time scale.
        paused = 1 - paused;
        canvas[0].SetActive(!Convert.ToBoolean(paused));
        if (paused == 1) {
            Time.timeScale = 1;
            if (cursor != null) {
                Destroy(cursor);
            }
        } else {
            cursor = Instantiate(Resources.Load("Prefabs/CursorIns")) as GameObject;
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir2 = new Vector2(dir.x, dir.y);
            cursor.transform.position = dir2;
            if (menu != null) {
                cursor.GetComponent<CursorMovementPause>().menu = menu;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (paused == 0) {
            if (GameObject.Find("Player") != null)
                GameObject.Find("Player").GetComponent<HealthManager>().timer = 1;
            if (GameObject.Find("Player2") != null)
                GameObject.Find("Player2").GetComponent<HealthManager>().timer = 1;
            Time.timeScale = 0;
        }
    }
}
