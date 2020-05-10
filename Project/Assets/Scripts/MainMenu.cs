using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button menu;
  
    // Update is called once per frame
    void Update()
    {
        if (menu != null)
            menu.onClick.AddListener(mainMenu);
    }
    void mainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
