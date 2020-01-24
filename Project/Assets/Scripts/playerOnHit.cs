using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerOnHit : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }
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
     // Player has 3 hits till gameover, on consumption of boss piece, they gain health but are capped at 3.   
  
}
