using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartsController : MonoBehaviour
{
    public GameObject heart;
    public List<GameObject> hearts = new List<GameObject>();
    public GameObject player;

    void Start()
    {
        int health = (int)player.GetComponent<HealthManager>().maxHealth;
        hearts.Add(heart);
        for(int i = 1; i < health; i++) {
            Vector2 temp = heart.transform.position;
            temp.x += i * 1.5f;
            hearts.Add(Instantiate(heart, temp , transform.rotation) as GameObject);
            hearts[i].transform.parent = GameObject.Find("Canvas").transform.Find("Border").transform.Find("health");
            hearts[i].transform.localScale = heart.transform.localScale;
        }
    }

    public void updateHealth()
    {
        for(int i = 0; i < hearts.Count; i++) {
            hearts[i].SetActive(true);
        }
        while ((int)player.GetComponent<HealthManager>().health > hearts.Count) {
            int i = hearts.Count;
            Vector2 temp = heart.transform.position;
            temp.x += i * 1.5f;
            hearts.Add(Instantiate(heart, temp, transform.rotation) as GameObject);
            hearts[i].transform.parent = GameObject.Find("Canvas").transform.Find("health");
            hearts[i].transform.localScale = heart.transform.localScale;
        }
    }
    
    public void losehealth()
    {
        GameObject heart = hearts[(int)player.GetComponent<HealthManager>().health];
        heart.SetActive(false);
    }
}
