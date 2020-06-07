using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{
    private GameObject SP;
    public Button win;
    // Start is called before the first frame update
    void Start()
    {
        if (win != null) {
            win.onClick.AddListener(winGame);
        }
    }

    public void winGame()
    {
        SceneManager.LoadScene("Scenes/Outro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
