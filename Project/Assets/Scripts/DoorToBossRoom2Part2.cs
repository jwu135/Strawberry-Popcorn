﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToBossRoom2Part2 : MonoBehaviour
{
    private bool gameStarted = false;
    GameObject css;
    GameObject dialogueBox;
    public GameObject prayingSP;
    public void startGame(GameObject css, GameObject dialogueBox)
    {
        gameStarted = true;
        this.css = css;
        this.dialogueBox = dialogueBox;
    }

    private void Update()
    {
        if (Time.timeScale != 0) {
            if (gameStarted && Input.GetButton("Fire1")) {
                css.SetActive(true);
                dialogueBox.SetActive(true);
                Destroy(prayingSP);
                Destroy(this);
            }
        }
    }


}
