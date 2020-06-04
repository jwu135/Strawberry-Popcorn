﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button play;
    public Button quit;
    public Button retry;
    public bool intro = false;
    private List<Vector2> positions;
    public Sprite berryImage;
    public Sprite[] berryImageDropped;
    public string[] quotes = new string[1];
    public GameObject berryBody;
    private GameObject currBody;
    private List<Sprite> bodies;
    public Scene scene;
    public Upgrade Upgrade;
    Vector2 temp;
    float finalPosY;
    private Sprite tempBerrySprite;
    private int lastDeathCounter = 0;
    private bool fallingBerry = false;
    private GameObject cursor;
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void quoteFiller(){
        quotes[0] = "filler";
    }


    void Start()
    {
        quoteFiller();
        lastDeathCounter = GlobalVariable.lastDeathCounter;
        if (play != null)
            play.onClick.AddListener(playGame);
        if(quit!=null)
            quit.onClick.AddListener(doExitGame);
        if(retry!=null)
            retry.onClick.AddListener(startGame);
        scene = SceneManager.GetActiveScene();

        Debug.Log(UpgradeValues.continueGame);
        cursor = GameObject.Find("Cursor");
        if (scene.name.Equals("Gameover") && !UpgradeValues.continueGame)
        {
            positions = GlobalVariable.positions;
            bodies = GlobalVariable.bodies;
            dropBerry();
            GlobalVariable.bodies = bodies;
            GlobalVariable.positions = positions;
            Debug.Log("UpgradeValues.continueGame");
           // Upgrade.SavePlayer();
        }

        if (scene.name.Equals("Gameover") && UpgradeValues.continueGame)
        {
            positions = UpgradeValues.positions;
            bodies = UpgradeValues.bodies;
            dropBerry();
            GlobalVariable.bodies = bodies;
            GlobalVariable.positions = positions;
            UpgradeValues.continueGame = false;
            Debug.Log("UpgradeValues.continueGame2");
           // Upgrade.SavePlayer();
        }
    }



    public void dropBerry()
    {
        int i = 0;
        foreach (Vector2 item in positions) {
            GameObject groundBody = Instantiate(berryBody, item, berryBody.transform.rotation);
            groundBody.GetComponent<SpriteRenderer>().sprite = bodies[i];
            if (groundBody.GetComponent<SpriteRenderer>().sprite.name.Equals("DeadStrawberry1Blood")){
                groundBody.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }
                    
            i++;
        }
        // -4.73
        if (lastDeathCounter != GlobalVariable.deathCounter) {
            bool usingController = false;
            //cursor.SetActive(false);
            cursor.GetComponent<SpriteRenderer>().enabled = false;
            GlobalVariable.lastDeathCounter = GlobalVariable.deathCounter;
            fallingBerry = true;
            float tempY = Random.Range(-4.5f, -2.5f);
            float finalTempX = Random.Range(-7f, 7f);
            if (tempY > -3.33f) { 
                float tempXLeft = Random.Range(-7f, -2.5f); // this all is for preventing the SPs from falling on the buttons.
                float tempXRight = Random.Range(2.5f, 7f);
                float swapper = Random.Range(0f, 1f);
                finalTempX = (swapper > 0.5) ? tempXLeft : tempXRight;
            }
            temp = new Vector2(finalTempX, tempY);

            //Debug.Log(UpgradeValues.deathCounter);
            //Debug.Log(temp.x);
            UpgradeValues.positionValues[(UpgradeValues.deathCounter * 2) - 2] = temp.x;
            //Debug.Log(UpgradeValues.positionValues[0]);
            UpgradeValues.positionValues[(UpgradeValues.deathCounter * 2) - 1] = temp.y;
            //Debug.Log(temp.y);
            //Debug.Log(UpgradeValues.positionValues[1]);

            positions.Add(temp);
            finalPosY = temp.y;
            temp.y = 6;
            currBody = Instantiate(berryBody, temp, berryBody.transform.rotation) as GameObject;
            tempBerrySprite = (Random.Range(0f, 1f) > 0.5f ? berryImageDropped[0] : berryImageDropped[1]);

            if(tempBerrySprite == berryImageDropped[0])
            {
                UpgradeValues.bodyTypes[(UpgradeValues.deathCounter - 1)] = 0;
            }
            else
            {
                UpgradeValues.bodyTypes[(UpgradeValues.deathCounter - 1)] = 1;
            }
            

            bodies.Add(tempBerrySprite);
            
        }
    }


    void Update()
    {
        if (scene.name.Equals("Gameover")){
            if (fallingBerry) {
                if (currBody.transform.position.y > finalPosY) {
                    Vector2 temp = currBody.transform.position;
                    temp.y -= 0.1f;
                    currBody.transform.position = temp;
                } else if (currBody.GetComponent<SpriteRenderer>().sprite.name.Equals("fallingStrawberry")) {
                    currBody.GetComponent<SpriteRenderer>().sprite = tempBerrySprite;
                    if (currBody.GetComponent<SpriteRenderer>().sprite.name.Equals("DeadStrawberry1Blood")) {
                        currBody.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
                    }
                    GameObject camera = GameObject.Find("Main Camera");
                    camera.GetComponent<ScreenShake>().shakeCamera(0.5f);
                    cursor.transform.position = currBody.transform.position;
                    //cursor.SetActive(true);
                    cursor.GetComponent<CursorMovement>().paused = true;
                    cursor.GetComponent<SpriteRenderer>().enabled = true;
                    StartCoroutine("pauseDelay");
                }
            }

        }

    }


    IEnumerator pauseDelay()
    {
        yield return new WaitForSeconds(0.5f);
        cursor.GetComponent<CursorMovement>().paused = false;
    }

    void startGame()
    {
        if (scene.name.Equals("Gameover"))
        {
            SceneManager.LoadScene("Scenes/Upgrade");
        }
        if (scene.name.Equals("Upgrade"))
        {
            SceneManager.LoadScene("Scenes/ParallaxTest");
            Debug.Log("fish1");
        }
        if (scene.name.Equals("MainMenu") && UpgradeValues.upgradeLocation == 0)
        {
            SceneManager.LoadScene("Scenes/Upgrade");
        }
        if (scene.name.Equals("MainMenu") && UpgradeValues.upgradeLocation == 1 && UpgradeValues.deathProfit)
        {
            SceneManager.LoadScene("Scenes/Upgrade");
            Debug.Log("fish2");
        }
        if (scene.name.Equals("MainMenu") && UpgradeValues.upgradeLocation == 1 && !UpgradeValues.deathProfit)
        {
            SceneManager.LoadScene("Scenes/ParallaxTest");
            Debug.Log("fish3");
        }


    }
    void playGame()
    {
        if (scene.name.Equals("Gameover"))
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        if(scene.name.Equals("Upgrade"))
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }

        if (scene.name.Equals("MainMenu"))
        {
            SceneManager.LoadScene("Scenes/Prologue1");
        }



    }

    void doExitGame()
    {
        if (!scene.name.Equals("Gameover"))
        {
            Application.Quit();
        }
    }

}
