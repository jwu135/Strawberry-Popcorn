using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button menu;
  
    // Update is called once per frame
    void Start()
    {
        if (menu != null)
            menu.onClick.AddListener(mainMenu);
    }
    private void Update()
    {
        
        Debug.Log(Time.timeScale);
    }
    void mainMenu()
    {
        GameObject.Find("Player").GetComponent<HealthManager>().timer = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scenes/MainMenu");
    }
}
