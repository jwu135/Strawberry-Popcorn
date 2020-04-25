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
    public GameObject berryBody;
    private GameObject currBody;
    private Scene scene;
    void Start()
    {
        
        if (play != null)
            play.onClick.AddListener(playGame);
        if(quit!=null)
            quit.onClick.AddListener(doExitGame);
        if(retry!=null)
            retry.onClick.AddListener(startGame);
        scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("Gameover")) {
            positions = GlobalVariable.positions;
            dropBerry();
            GlobalVariable.positions = positions;
        }
    }



    public void dropBerry()
    {
        foreach (Vector2 item in positions) {
            Instantiate(berryBody, item, berryBody.transform.rotation);
        }
        Vector2 temp = new Vector2(Random.Range(-7f, 7f), -2.5f);
        positions.Add(temp);
        temp.y = 6;
        currBody = Instantiate(berryBody, temp, berryBody.transform.rotation) as GameObject;
    }


    void Update()
    {
        if (scene.name.Equals("Gameover")){
            if (currBody.transform.position.y > -2.5f) {
                Vector2 temp = currBody.transform.position;
                temp.y -= 0.1f;
                currBody.transform.position = temp;
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
