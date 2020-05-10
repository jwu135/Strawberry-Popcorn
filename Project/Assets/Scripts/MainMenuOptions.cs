using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuOptions : MonoBehaviour
{
    public GameObject[] canvas;
    public Button optionButton;
    public Button backButton;
    public Resolution[] res = new Resolution[2];

    private void Start()
    {
        
        if (optionButton != null)
            optionButton.onClick.AddListener(delegate { options(true); }) ; 
        if (backButton != null)
            backButton.onClick.AddListener(delegate { options(false); }) ; 
    }
    // Update is called once per frame
    void options(bool t)
    {
        Debug.Log(t);
        canvas[0].SetActive(!t);
        canvas[1].SetActive(t);
    }
    void Update()
    {
        
    }
}
