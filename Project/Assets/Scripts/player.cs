using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;
    public float maxMana = 100f;
    public float mana = 100f;

    public void losehealth()
    {
        health -= 1;
    }

    public void gainhealth()
    {
        mana -= 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
