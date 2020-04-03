using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DragonBones;
public class CutsceneSystem : MonoBehaviour
{
    public static int size = 0;
    public GameObject player;
    public GameObject boss;
    public GameObject[] objects = new GameObject[size] ;
    public GameObject dialogueBox;
    private GameObject currPiece;

    // Start is called before the first frame update
    void Awake()
    {
        Stop();
    }
    void Stop(bool first = true)
    {
        MonoBehaviour[] scripts = boss.GetComponentsInChildren<MonoBehaviour>().Concat(player.GetComponentsInChildren<MonoBehaviour>()).ToArray();
        foreach (MonoBehaviour script in scripts) {
            if (script.GetType().Name != "BossMovement"&& script.GetType().Name != "UnityArmatureComponent"&& script.GetType().Name != "UnityCombineMeshs") {
                script.enabled = false;
            }
        }
        scripts = player.GetComponentsInChildren<MonoBehaviour>();
        if (first) {
            foreach (GameObject i in objects) {
                i.SetActive(false);
            };
        }
    }

    // the following code is a result of having very little time to complete something that needed substantially more time
    public void cutscene(GameObject piece)
    {
        Stop();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<BossShoot>().destroyProjectiles();
        Time.timeScale = 1;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        boss.GetComponent<Rigidbody2D>().velocity = transform.up * -20;
        player.GetComponent<Rigidbody2D>().velocity = transform.up * -20;
        boss.transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("hurt", 0);
        player.GetComponent<Movement>().getArmature().animation.Play("Idle", 0);
        GetComponent<DialogueSystem>().dialogueBox.SetActive(true); 
        GetComponent<DialogueSystem>().restart();
        currPiece = piece;
        //StartCoroutine("buffer");
    }
    IEnumerator buffer()
    {
        yield return new WaitForSeconds(0.1f);
        int direction = (player.transform.position.x > currPiece.transform.position.x) ? 0 : 1;
        float speed = 6;
        if (direction == 0) {
            speed *= -1;
        }
        player.GetComponent<Rigidbody2D>().velocity = transform.right*speed;
        player.GetComponent<Movement>().getArmature().animation.Play("Running", 0);
        while (Mathf.Abs(player.transform.position.x - currPiece.transform.position.x)>1) {
            yield return new WaitForSeconds(0.05f);
        }
        currPiece.GetComponent<BossPiece>().eat();
        //yield return new WaitForSeconds(0.1f);
        player.GetComponent<Movement>().getArmature().animation.Play("Idle", 0);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        //player.transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("Idle", 0);
        yield return new WaitForSeconds(0.5f);

        boss.transform.Find("Armature").gameObject.GetComponent<UnityArmatureComponent>().animation.Play("bossIdle", 0);
        //DialogueDone(false);
        player.GetComponent<HealthManager>().maxHealth += 1;
        player.GetComponent<HealthManager>().health = player.GetComponent<HealthManager>().maxHealth;
        GameObject.Find("EventSystem").GetComponent<PlayerHeartsController>().updateHealth();
        GetComponent<DialogueSystem>().dialogueBox.SetActive(true);
        GetComponent<DialogueSystem>().restart2();
    }


    public void DialogueDone(bool first = true)
    {
        foreach (GameObject i in objects) {
         i.SetActive(true);
        };
        //player.SetActive(true);
        //boss.SetActive(true);
        MonoBehaviour[] scripts = boss.GetComponentsInChildren<MonoBehaviour>().Concat(player.GetComponentsInChildren<MonoBehaviour>()).ToArray();
        foreach (MonoBehaviour script in scripts) {
            if (script.GetType().Name != "BossMovement") {
                script.enabled = true;
            }
        }
        if(first)
         boss.GetComponentInChildren<BossShoot>().startTime();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
