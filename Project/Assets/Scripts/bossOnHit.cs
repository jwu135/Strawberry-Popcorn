using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossOnHit : MonoBehaviour
{
    //private float health = 100f;
    public Text text;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet") {
            GetComponent<boss>().losehealth(5f);
            text.text = GetComponent<boss>().health.ToString() + "/" + "100";
            if (GetComponent<boss>().health <= 0) {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
        }
    }
}
