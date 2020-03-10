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
        if (Input.GetButtonDown("eat")&&over) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().evolution++;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().weaponCycle = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCombat>().evolution;
            GetComponent<BossShoot>().setPhase(GetComponent<Boss>().getPhase());
            Destroy(gameObject);    
        }
    }
}
