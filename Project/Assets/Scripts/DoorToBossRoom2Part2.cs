using System.Collections;
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
            if (gameStarted && Input.GetButton("interact")) {
                css.SetActive(true); // cutSceneStart gets Started
                dialogueBox.SetActive(true);
                Destroy(prayingSP);
                Destroy(this);
            }
        }
    }


}
