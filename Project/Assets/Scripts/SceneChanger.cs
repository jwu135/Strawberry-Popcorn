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
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(playGame);
        if(quit!=null)
            quit.onClick.AddListener(doExitGame);
        if(retry!=null)
            retry.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            playGame();
        }
        if (Input.GetButtonDown("Fire1")) {
            startGame();
        }
    }
    void startGame()
    {
        SceneManager.LoadScene("Scenes/MainGameplay");
    }
    void playGame()
    {
        Scene scene = SceneManager.GetActiveScene();
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
