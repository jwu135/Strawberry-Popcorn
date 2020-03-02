using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
       //player.SetActive(false);
        //boss.SetActive(false);
        //MonoBehaviour[][] scripts = [2][2];
        //scripts[0] = boss.GetComponents<MonoBehaviour>();
        MonoBehaviour[] scripts = boss.GetComponentsInChildren<MonoBehaviour>().Concat(player.GetComponentsInChildren<MonoBehaviour>()).ToArray();
        foreach (MonoBehaviour script in scripts) {
            if (script.GetType().Name != "BossMovement") {
                script.enabled = false;
            }
        }
        scripts = player.GetComponentsInChildren<MonoBehaviour>();

        foreach (GameObject i in objects){
            i.SetActive(false);
        };
    }

    public void DialogueDone()
    {
        foreach (GameObject i in objects) {
         i.SetActive(true);
        };
        //player.SetActive(true);
        //boss.SetActive(true);
        MonoBehaviour[] scripts = boss.GetComponentsInChildren<MonoBehaviour>().Concat(player.GetComponentsInChildren<MonoBehaviour>()).ToArray();
        foreach (MonoBehaviour script in scripts) {
            //script.enabled = true;
        }
        boss.GetComponentInChildren<BossShoot>().startTime();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
