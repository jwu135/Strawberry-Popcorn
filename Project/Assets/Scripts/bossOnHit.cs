using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOnHit : MonoBehaviour
{
    public Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet") {
            Boss boss = GetComponent<Boss>();
            boss.losehealth(5f);
            text.text = boss.health.ToString() + "/" + "100";
            if (boss.health <= 0) {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            if (boss.health%20==0) {
                Debug.Log("dropped piece");
                GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"),transform.position,transform.rotation) as GameObject;
                Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f)*5;
                /*GameObject bullet = Instantiate(projectile, temp, transform.rotation) as GameObject;
                Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 4;*/
            }
        }
    }
}
