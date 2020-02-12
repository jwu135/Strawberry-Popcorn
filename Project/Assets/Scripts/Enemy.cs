﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public double health;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(double damage)
    {
        GetComponent<boss>().losehealth(damage);
        text.text = GetComponent<boss>().health.ToString() + "/" + "100";
        if (GetComponent<boss>().health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
        }
        health -= damage;
        Debug.Log("damage");
        Debug.Log(health);
    }
    public void TakeDamage2(double damage2)
    {
        GetComponent<boss>().losehealth(damage2);
        text.text = GetComponent<boss>().health.ToString() + "/" + "100";
        if (GetComponent<boss>().health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
        }
        health -= damage2;
        Debug.Log("damage");
        Debug.Log(health);
    }
    public void TakeDamage3(double damage3)
    {
        GetComponent<boss>().losehealth(damage3);
        text.text = GetComponent<boss>().health.ToString() + "/" + "100";
        if (GetComponent<boss>().health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
        }
        health -= damage3;
        Debug.Log("damage");
        Debug.Log(health);
    }

    public void TakeDamage4(double damage4)
    {
        GetComponent<boss>().losehealth(damage4);
        text.text = GetComponent<boss>().health.ToString() + "/" + "100";
        if (GetComponent<boss>().health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
        }
        health -= damage4;
        Debug.Log("damage");
        Debug.Log(health);
    }
}
