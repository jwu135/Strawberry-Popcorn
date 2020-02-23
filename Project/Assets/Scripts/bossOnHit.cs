using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOnHit : MonoBehaviour
{
    public Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "normalAttack1"|| other.tag == "chargeAttack1" || other.tag == "chargeAttack2") {
            float damage = 1f;
            if (other.tag == "normalAttack1") damage = 1f; // left click attacks
            else if (other.tag == "chargeAttack1") damage = 5f; // right click attacks
            else damage = 5f; // harpoon

            Boss boss = GetComponent<Boss>();
            boss.losehealth(damage);
            text.text = boss.health.ToString() + "/" + "100";
            if (boss.health <= 0) {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            if (boss.health%2==0) {
                GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"),transform.position,transform.rotation) as GameObject;
                Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f)*5;
            }
        }
    }
}
