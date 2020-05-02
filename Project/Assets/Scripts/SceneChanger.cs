using System.Collections;
using System.Collections.Generic;
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
    public GameObject berryBody;
    private GameObject currBody;
    private List<Sprite> bodies;
    private Scene scene;
    Vector2 temp;
    float finalPosY;
    private Sprite tempBerrySprite;
    private int lastDeathCounter = 0;
    private bool fallingBerry = false;
    void Start()
    {
        lastDeathCounter = GlobalVariable.lastDeathCounter;
        if (play != null)
            play.onClick.AddListener(playGame);
        if(quit!=null)
            quit.onClick.AddListener(doExitGame);
        if(retry!=null)
            retry.onClick.AddListener(startGame);
        scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("Gameover")) {
            positions = GlobalVariable.positions;
            bodies = GlobalVariable.bodies;
            dropBerry();
            GlobalVariable.bodies = bodies;
            GlobalVariable.positions = positions;
        }
    }



    public void dropBerry()
    {
        int i = 0;
        foreach (Vector2 item in positions) {
            Debug.Log(i);
            GameObject groundBody = Instantiate(berryBody, item, berryBody.transform.rotation);
            groundBody.GetComponent<SpriteRenderer>().sprite = bodies[i];
            if (groundBody.GetComponent<SpriteRenderer>().sprite.name.Equals("DeadStrawberry1Blood")){
                groundBody.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
            }
                    
            i++;
        }
    // -4.73
        if (lastDeathCounter != GlobalVariable.deathCounter) {
            GlobalVariable.lastDeathCounter = GlobalVariable.deathCounter;
            fallingBerry = true;
            temp = new Vector2(Random.Range(-7f, 7f), Random.Range(-4.5f, -2.5f));
            positions.Add(temp);
            finalPosY = temp.y;
            temp.y = 6;
            currBody = Instantiate(berryBody, temp, berryBody.transform.rotation) as GameObject;
            tempBerrySprite = (Random.Range(0f, 1f) > 0.5f ? berryImageDropped[0] : berryImageDropped[1]);
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
                }
            }

        }
        if (Input.GetButtonDown("Jump")&&!intro) {
            startGame();
        }
    }
    void startGame()
    {
        SceneManager.LoadScene("Scenes/MainGameplay");
    }
    void playGame()
    {
        if(scene.name.Equals("Gameover"))
            SceneManager.LoadScene("Scenes/MainMenu");
        else
            SceneManager.LoadScene("Scenes/Intro");
    }
    void doExitGame()
    {
        Application.Quit();
    }
}
