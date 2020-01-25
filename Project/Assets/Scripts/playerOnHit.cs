using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerOnHit : MonoBehaviour
{
    public Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossBullet") {
            player player = GetComponent<player>();
            player.losehealth();
            text.text = player.health.ToString() + "/" + player.maxHealth.ToString();
            if (GetComponent<player>().health <= 0) {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
        }
    }
}
