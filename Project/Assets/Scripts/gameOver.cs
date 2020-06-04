        using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    

    public void startGameOver(bool boss)
    {
        StartCoroutine("gameEnd",boss);
    }

    IEnumerator gameEnd(bool boss)
    {
        if (boss) {
            GameObject MotherEye = GameObject.Find("Mother's Eye");
            MotherEye.GetComponent<BossShoot>().destroyProjectiles();
            GameObject.Find("Border").GetComponent<Animator>().SetTrigger("Down");

            GameObject FinalMotherPrefab = Resources.Load("Prefabs/FinalDroppedMother") as GameObject;
            GameObject FinalMother = Instantiate(FinalMotherPrefab, MotherEye.transform.position,FinalMotherPrefab.transform.rotation) as GameObject;
            //FinalMother.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f) * 5;


            Destroy(GameObject.Find("Mother"));
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().setMode(0);
            //GameObject.FindGameObjectWithTag("DemoText").GetComponent<Text>().enabled = true;
            yield return new WaitForSeconds(2f);
        } else {
            SceneManager.LoadScene("Scenes/Gameover");

        }
    }
}
