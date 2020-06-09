using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Henshin : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Camera cam2;
    public Camera cam3;
    public float speed;
    bool henshin = false;
    bool over = false;
    private int counter = 0;

    private bool musicEnabled = false;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject sp;
    public GameObject cursor;
    public GameObject crossHair;
    public PlayableDirector Prologue;
    public GameObject buildsCamera;
    void Start()
    {
        //cam = Camera.main;
        //cam3.enabled = false;

        if (UpgradeValues.deathCounter > 0)
        {
            Player1.SetActive(false);
            cam.enabled = false;
            sp.SetActive(false);
            Destroy(GameObject.Find("Stopper"));
            GameObject.Find("EventSystem").GetComponent<Builds>().enabled = true;
            Player2.SetActive(false);
            cursor = Instantiate(cursor);
            cursor.AddComponent<BuildsCursor>(); // didn't even know this was possible til just now
            cursor.GetComponent<BoxCollider2D>().isTrigger = true;
            cursor.GetComponent<SpriteRenderer>().sortingOrder = 20;
            crossHair.SetActive(false);
        }
        else
        {
            Player2.SetActive(false);
            cam2.enabled = false;
            Prologue.enabled = false;
        }

        cam3.enabled = false;
        if (UpgradeValues.deathCounter > 0) {
            buildsCamera = Instantiate(Resources.Load("Prefabs/Main Camera Builds")) as GameObject;
            cam.enabled = false;
            cam2.enabled = false;
            cam3.enabled = false;
        }
    }

    void Update()
    {
        if (UpgradeValues.deathCounter > 0) {
            if (cam2.orthographicSize < 8.7f && buildsCamera.activeSelf == false) {          
                cam2.transform.position = new Vector3(Mathf.Lerp(cam2.transform.position.x, -3.33f, speed), Mathf.Lerp(cam2.transform.position.y, 2.5f, speed), Mathf.Lerp(cam2.transform.position.z, 0, speed));
                cam2.orthographicSize = Mathf.Lerp(cam2.orthographicSize, 8.709762f, speed);
            }
        }
        if (henshin == true)
        {
            if (UpgradeValues.deathCounter == 0) {
                if (cam2.orthographicSize < 8.7f && cam3.enabled == false) {
                    cam2.transform.position = new Vector3(Mathf.Lerp(cam2.transform.position.x, -3.33f, speed), Mathf.Lerp(cam2.transform.position.y, 2.5f, speed), Mathf.Lerp(cam2.transform.position.z, 0, speed));
                    cam2.orthographicSize = Mathf.Lerp(cam2.orthographicSize, 8.709762f, speed);
                }
            }
            

                //Debug.Log(cam.orthographicSize);
            /*
        test1.color = new Color(1, 1, 1, 1);
        
        if (Input.GetButtonDown("Fire1") && counter == 1)
        {

            test3.color = new Color(1, 1, 1, 1);
            counter = 2;
        }
        else if (Input.GetButtonDown("Fire1") && counter == 0)
        {
            test2.color = new Color(1, 1, 1, 1);
            counter = 1;
        }
        else if (Input.GetButtonDown("Fire1") && counter == 2)
        {
            test1.enabled = false;
            test2.enabled = false;
            test3.enabled = false;
            counter = 3;
        }
        */
            if (Input.GetButtonDown("interact"))
            {
                cutsceneEnd();
            }
               
        }else if(henshin == false&& over&& Input.GetButtonDown("Use")&&UpgradeValues.deathCounter==0) { // had to move these around, since unity's callstack causes trigger collisions to be called before update, making interact skip through the cutscene
            henshin = true;
            Prologue.enabled = true;
            Prologue.Play();
            cam3.enabled = true;
            Player1.SetActive(false);
            // cam3.enabled = false;
            cam2.enabled = true;
            cam.enabled = false;
            Player2.SetActive(true);
            Player2.GetComponent<PlatformMovementPhys>().unableToMove = true;
            Player2.GetComponent<PlatformMovementPhys>().ableToJump = false;
            cam2.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, -3.33f, speed), Mathf.Lerp(cam.transform.position.y, 2.5f, speed), Mathf.Lerp(cam.transform.position.z, 0, speed));
            cam2.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8.709762f, speed);
            sp.SetActive(false);
        }
    }

    public void cutsceneEnd()
    {
        Player2.GetComponent<PlatformMovementPhys>().unableToMove = false;
        Player2.GetComponent<PlatformMovementPhys>().ableToJump = true;
        cam3.enabled = false;
        Prologue.Stop();
        if (UpgradeValues.deathCounter == 0) {
            if (!musicEnabled) {
                musicEnabled = true;
                GameObject.FindGameObjectWithTag("music").GetComponent<MusicManagerRoom2>().play();
            }
            Destroy(GameObject.Find("Stopper"));
        }
    }

    public void choseSP()
    {
        Destroy(cursor);
        if (buildsCamera == null) {
            Debug.Log("null");
        }
        if (!musicEnabled) {
            musicEnabled = true;
            GameObject.FindGameObjectWithTag("music").GetComponent<MusicManagerRoom2>().play();
        }
        Player2.SetActive(true);
        cam2.enabled = true;
        buildsCamera.SetActive(false);
        cam2.transform.position = new Vector3(Mathf.Lerp(buildsCamera.transform.position.x, -3.33f, speed), Mathf.Lerp(buildsCamera.transform.position.y, 2.5f, speed), Mathf.Lerp(buildsCamera.transform.position.z, 0, speed));
        cam2.orthographicSize = Mathf.Lerp(buildsCamera.GetComponent<Camera>().orthographicSize, 8.709762f, speed);
        crossHair.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (henshin == false)) {
            over = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (henshin == false)) {
            over = false;
        }
    }
}
