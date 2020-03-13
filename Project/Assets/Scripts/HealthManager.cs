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
    public float pauseTime = 100; //in milliseconds
    private float timer = 0; //used for pause frames


    public Text helthText;
    public Text manaText;

    public PlayerCombat PlayerCombat;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {

        print("Timeleft: " + (pauseTime - timer));
        if (Time.timeScale == 0 && timer <= pauseTime)
        {
            timer += Time.unscaledDeltaTime * 1000;
        }
        else if(timer >= pauseTime)
        {
            print("Play");
            Time.timeScale = 1;
            timer = 0;
        }
    }
    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (invicibilityCounter > 0)
        {
            //Debug.Log("Invincible");
            invicibilityCounter -= 1;
        }

        if (health <= 0)
        {
            GameObject.Find("EventSystem").GetComponent<gameOver>().startGameOver(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invicibilityCounter <= 0)
        {
            if (other.tag == "BossBullet")
            {
                Time.timeScale = 0;
                print("Pause");
                health -= 1;
                PlayerCombat.Hurt();
                helthText.text = health.ToString() + "/" + maxHealth.ToString();
            }
        }
        else
        {
            if (other.tag == "BossBullet" && manaCounter == 1)
            {
                mana += 5;
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
