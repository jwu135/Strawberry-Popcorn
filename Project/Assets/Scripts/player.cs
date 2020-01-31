using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void losehealth()
    {
        health -= 1;
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
