using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public double maxHealth = 100;
    public double health = 100;
    public double maxMana = 100;
    public double mana = 100;


    public float invicibilityLength;
    public float invicibilityCounter;
    public float manaCounter = 1;


    public Text helthText;
    public Text manaText;

    public PlayerCombat PlayerCombat;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (invicibilityCounter > 0)
        {
            invicibilityCounter -= Time.deltaTime;
        }

        if (health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invicibilityCounter <= 0)
        {
            if (other.tag == "BossBullet")
            {
                health -= 1;
                PlayerCombat.Hurt();
                helthText.text = health.ToString() + "/" + maxHealth.ToString();
               
            }
        }
        else
        {
            if (other.tag == "BossBullet" && manaCounter == 1)
            {
                mana += 10;
                manaCounter = 0;
                manaText.text = mana.ToString() + "/" + maxMana.ToString();
            }
        }
    }


    public void updateHealth(double health, double maxHealth)
    {
        helthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void updateMana(double mana, double maxMana)
    {
        manaText.text = mana.ToString() + "/" + maxMana.ToString();
    }
}
