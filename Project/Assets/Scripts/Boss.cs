using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    public double health = 10;
    public Image HealthBar;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void losehealth(double amnt)
    {
        health -= amnt;
        Vector2 temp = HealthBar.rectTransform.sizeDelta;
        temp.x = 505f * ((float)health / 100f);
        HealthBar.rectTransform.sizeDelta = temp;
        if (health <= 0) {
            GameObject.FindGameObjectWithTag("EventSystem").GetComponent<gameOver>().startGameOver(true);     
        }
    }
    
}
