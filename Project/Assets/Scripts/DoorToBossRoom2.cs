using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToBossRoom2 : MonoBehaviour
{
    BoxCollider2D door;
    BoxCollider2D Player;
    public GameObject css;
    public DoorToBossRoom2Part2 Part2;
    public GameObject dialogueBox;
    private bool started = false;
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Left hitbox");
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
    private void Awake()
    {
        css.SetActive(false);
        dialogueBox.SetActive(false);
        if (GlobalVariable.deathCounter == 0) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().delayBtwAttack1 = 100000;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().timeBtwAttack = 100000;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().delayBtwChargeAttack1 = 100000;
        }else
            Part2.prayingSP.GetComponent<SpriteRenderer>().enabled = false;
    }

    /*
    Click to close

        wait til closed

        click to start cutscene.

    */



    private void Update()
    {
        if (Time.timeScale != 0) {
            if(Input.GetButton("Fire1")&&!started){
                StartCoroutine("gameStart");
                started = true; // trying to avoid running cutsceen mutliple times
            }
        }
    }


    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(1f);
        GetComponent<Animator>().SetTrigger("Close");
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        door = GetComponent<BoxCollider2D>();
    }
    

    public void DestroyObject()
    {
        Part2.startGame(css,dialogueBox);
        Destroy(gameObject);
    }

}
