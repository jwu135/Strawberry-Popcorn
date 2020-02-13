using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public double health;
    public Text text;
    public bool ConnectNA1 = false;
    public bool ConnectNA2 = false;
    public bool ConnectNA3 = false;
    public bool ConnectCA1 = false;
    public bool ConnectCA2 = false;
    public bool ConnectCA3 = false;
    public double damage1;
    public double damage2;
    public double damage3;
    public double damage4;
    public double damage5;
    public double damage6;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "normalAttack1")
        {
            GetComponent<Boss>().losehealth(damage1);
            text.text = GetComponent<Boss>().health.ToString() + "/" + "100";
            if (GetComponent<Boss>().health <= 0)
            {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            health -= damage1;
            Debug.Log("damage");
            Debug.Log(health);
        }
        if (other.tag == "normalAttack2")
        {
            GetComponent<Boss>().losehealth(damage2);
            text.text = GetComponent<Boss>().health.ToString() + "/" + "100";
            if (GetComponent<Boss>().health <= 0)
            {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            health -= damage2;
            Debug.Log("damage");
            Debug.Log(health);
        }
        if (other.tag == "normalAttack3")
        {
            GetComponent<Boss>().losehealth(damage3);
            text.text = GetComponent<Boss>().health.ToString() + "/" + "100";
            if (GetComponent<Boss>().health <= 0)
            {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            health -= damage3;
            Debug.Log("damage");
            Debug.Log(health);
        }
        if (other.tag == "chargeAttack1")
        {
            GetComponent<Boss>().losehealth(damage4);
            text.text = GetComponent<Boss>().health.ToString() + "/" + "100";
            if (GetComponent<Boss>().health <= 0)
            {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            health -= damage4;
            Debug.Log("damage");
            Debug.Log(health);
        }
        if (other.tag == "chargeAttack2")
        {
            GetComponent<Boss>().losehealth(damage5);
            text.text = GetComponent<Boss>().health.ToString() + "/" + "100";
            if (GetComponent<Boss>().health <= 0)
            {
                GameObject.Find("EventSystem").GetComponent<gameOver>().gameEnd();
            }
            health -= damage5;
            Debug.Log("damage");
            Debug.Log(health);
        }
        if (other.tag == "chargeAttack3")
        {

        }
    }

    public void TakeDamage(double damage)
    {
      
    }
    public void TakeDamage2(double damage2)
    {
        
    }
    public void TakeDamage3(double damage3)
    {
        
    }

    public void TakeDamage4(double damage4)
    {

    }
}
