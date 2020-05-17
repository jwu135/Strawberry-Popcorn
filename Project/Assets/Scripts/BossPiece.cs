using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class BossPiece : MonoBehaviour
{
    bool over = false;
    public Sprite[] arms = new Sprite[2];
    public GameObject[] healthbars = new GameObject[3];
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor"&&collision.gameObject.name=="Floor") { // as far as I know, only the groundfloor satisfies both of these
            Destroy(GetComponent<Rigidbody2D>());
            transform.rotation = new Quaternion(0f, 0f,0f,0f);
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

    public void eat()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<PlayerCombat>().evolution < 3) {
            player.GetComponent<PlayerCombat>().evolution++;
            //player.GetComponent<PlayerCombat>().weaponCycle = player.GetComponent<PlayerCombat>().evolution;
        }
        /*
        string armatureSwap = player.GetComponent<Movement>().getArmature().name;
        switch (armatureSwap) {
            case "Armature":
                (player.transform.Find("Armature").gameObject).SetActive(false);
                (player.transform.Find("ArmatureArmed").gameObject).SetActive(true);
                break;
          
        }*/



        Movement movement = player.GetComponent<Movement>();
        Vector2 scale = movement.getArmature().transform.localScale;
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 1) {
            (player.transform.Find("Armature").gameObject).SetActive(false);
            (player.transform.Find("ArmatureMid").gameObject).SetActive(true);
            movement.setPrimaryIndex(movement.findIndex("ArmatureMid"));
            player.GetComponent<Movement>().setArmature();
            player.transform.Find("Arm").gameObject.GetComponent<Look>().setArmature();
            player.transform.Find("Arm").gameObject.GetComponent<SpriteRenderer>().sprite = arms[0];
            healthbars[0].SetActive(false);
            healthbars[1].SetActive(true);

        }
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 2) {
            healthbars[1].SetActive(false);
            healthbars[2].SetActive(true);
        }
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() == 3) {
            player.transform.Find("ArmatureMid").gameObject.SetActive(false);
            player.transform.Find("ArmatureLast").gameObject.SetActive(true);
            movement.setPrimaryIndex(movement.findIndex("ArmatureLast"));
            player.GetComponent<Movement>().setArmature();
            player.transform.Find("Arm").gameObject.GetComponent<Look>().setArmature();
            player.transform.Find("Arm").gameObject.GetComponent<SpriteRenderer>().sprite = arms[1];
        }
        player.GetComponent<Movement>().getArmature().transform.localScale = scale;

        GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>().setPhase(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase());
        Debug.Log(GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase());
        player.transform.Find("Arm").transform.GetComponent<Look>().setArmature();
        GameObject.Find("EventSystem").GetComponent<CutsceneSystem>().eaten = true;
        Destroy(gameObject);
    }

    void lookAround()
    {
        if (Input.GetButtonDown("interact") &&over) {
            eat();
        }
    }
}
