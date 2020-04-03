using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartsController : MonoBehaviour
{
    public GameObject heart;
    //public GameObject[] hearts;
    public List<GameObject> hearts = new List<GameObject>();
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        int health = (int)player.GetComponent<HealthManager>().maxHealth;
        //hearts = new GameObject[(int)health];
        hearts.Add(heart);
        for(int i = 1; i < health; i++) {
            //Vector2 temp = //new Vector2(0,0);
            Vector2 temp = heart.transform.position;
            temp.x += i * 1f;
            hearts.Add(Instantiate(heart, temp , transform.rotation) as GameObject);
            hearts[i].transform.parent = GameObject.Find("Canvas").transform;
            hearts[i].transform.localScale = heart.transform.localScale;
            /*hearts[i] = Instantiate(heart, temp , transform.rotation) as GameObject;
            hearts[i].transform.parent = GameObject.Find("Canvas").transform;
            hearts[i].transform.localScale = heart.transform.localScale;*/
        }
    }
    public void updateHealth()
    {
        int breaker = 0;
        for(int i = 0; i < hearts.Count; i++) {
            hearts[i].SetActive(true);
        }
        while ((int)player.GetComponent<HealthManager>().health > hearts.Count&&breaker<1000) {
            int i = hearts.Count;
            Vector2 temp = heart.transform.position;
            temp.x += i * 1f;
            hearts.Add(Instantiate(heart, temp, transform.rotation) as GameObject);
            hearts[i].transform.parent = GameObject.Find("Canvas").transform;
            hearts[i].transform.localScale = heart.transform.localScale;
            breaker++;
        }
        if (breaker > 999)
            Debug.Log("had to break");
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
