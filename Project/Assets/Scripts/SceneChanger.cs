using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button play;
    public Button quit;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(playGame);
        if(quit!=null)
            quit.onClick.AddListener(doExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            playGame();
        }
    }
    void playGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name.Equals("Gameover"))
            SceneManager.LoadScene("Scenes/MainMenu");
        else
            SceneManager.LoadScene("Scenes/MainGameplay");
    }
    void doExitGame()
    {
        Application.Quit();
    }
}
