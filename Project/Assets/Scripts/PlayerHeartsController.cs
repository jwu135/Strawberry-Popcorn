using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartsController : MonoBehaviour
{
    public GameObject heart;
    public GameObject[] hearts;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        int health = (int)player.GetComponent<HealthManager>().maxHealth;
        hearts = new GameObject[(int)health];
        hearts[0] = heart;
        for(int i = 1; i < health; i++) {
            //Vector2 temp = //new Vector2(0,0);
            Vector2 temp = heart.transform.position;
            temp.x += i * 1f;
            hearts[i] = Instantiate(heart, temp , transform.rotation) as GameObject;
            hearts[i].transform.parent = GameObject.Find("Canvas").transform;
            //temp = new Vector2(0, 0);
            //hearts[i].transform.position = temp;
            hearts[i].transform.localScale = heart.transform.localScale;
        }
    }
    public void losehealth()
    {
        GameObject heart = hearts[(int)player.GetComponent<HealthManager>().health];
        heart.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
