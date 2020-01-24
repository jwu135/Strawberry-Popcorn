using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Button play;
    /*public Button credits;
    public Button back;
    public GameObject Main;
    public GameObject Credit;
    bool swapper = true;*/

    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(playGame);
    }

    // 
    /*void swap()
    {
        swapper = !swapper;
        Main.SetActive(swapper);
        Credit.SetActive(!swapper);
    }*/

    // Update is called once per frame
    void Update()
    {

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
