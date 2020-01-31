using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    public float health = 100f;

    public float invicibilityLength;
    public float invicibilityCounter;

    public Text helthText;

    // Start is called before the first frame update
    void Start()
    {
        helthText = GetComponent<Text>();
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
                helthText.text = health.ToString() + "/" + maxHealth.ToString();
               
            }
        }
    }


    public void updateHealth(float health, float maxhealth)
    {
        helthText.text = health.ToString() + "/" + maxhealth.ToString();
    }
}
