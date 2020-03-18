using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPiece : MonoBehaviour
{
    bool over = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor") {
            Destroy(GetComponent<Rigidbody2D>());
            Vector2 temp = transform.position;
            temp.y += 0.1f;
            transform.position = temp;
        }
        if (collision.gameObject.tag=="Player") {
            over = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            over = false;
        }
    }

    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }

    void lookAround()
    {
        if (Input.GetButtonDown("eat")&&over) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player.GetComponent<PlayerCombat>().evolution < 3) {
                player.GetComponent<PlayerCombat>().evolution++;
                player.GetComponent<PlayerCombat>().weaponCycle = player.GetComponent<PlayerCombat>().evolution;
            }
            //Debug.Log(GetComponent<Boss>().getPhase());
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 1) {
                (player.transform.Find("Armature").gameObject).SetActive(false);
                (player.transform.Find("ArmatureMid").gameObject).SetActive(true);
                player.GetComponent<Movement>().setArmature();
                player.transform.Find("Arm").gameObject.GetComponent<Look>().setArmature();
            }
            if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 3) {
                player.transform.Find("ArmatureMid").gameObject.SetActive(false);
                player.transform.Find("ArmatureLast").gameObject.SetActive(true);
                player.GetComponent<Movement>().setArmature();
                player.transform.Find("Arm").gameObject.GetComponent<Look>().setArmature();
            }
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>().setPhase(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase());
            Debug.Log(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase());
            Destroy(gameObject);    
        }
    }
}
