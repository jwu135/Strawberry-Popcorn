﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public float health = 100f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void losehealth(float amnt)
    {
        health -= amnt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
