using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSystem : MonoBehaviour
{
    public static int size = 0;
    public GameObject player;
    public GameObject boss;
    public GameObject[] objects = new GameObject[size] ;
    

    // Start is called before the first frame update
    void Awake()
    {
       player.SetActive(false);
       boss.SetActive(false);
        foreach (GameObject i in objects){
            i.SetActive(false);
        };
    }

    public void DialogueDone()
    {
        foreach (GameObject i in objects) {
         i.SetActive(true);
        };
        player.SetActive(true);
        boss.SetActive(true);
        boss.GetComponentInChildren<BossShoot>().startTime();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
