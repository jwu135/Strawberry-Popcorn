using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    public Button changeScreen;
    private void Start()
    {
        if (changeScreen != null)
            changeScreen.onClick.AddListener(toggleScreen);
    }
    void toggleScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
