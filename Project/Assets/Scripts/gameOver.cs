using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
   public void startGameOver(bool boss)
    {
        StartCoroutine("gameEnd",boss);
    }

    IEnumerator gameEnd(bool boss)
    {
        if (boss) {
            Destroy(GameObject.Find("Mother"));
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setMode(0);
            GameObject.FindGameObjectWithTag("DemoText").GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(15f);
        }
        SceneManager.LoadScene("Scenes/Gameover");
    }

}
