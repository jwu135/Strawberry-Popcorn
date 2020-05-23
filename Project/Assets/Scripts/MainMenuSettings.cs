using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    public Button changeScreen;
    private Resolution defaultRes;
    private void Start()
    {
        defaultRes = Screen.currentResolution;
        if (changeScreen != null)
            changeScreen.onClick.AddListener(toggleScreen);
    }
    public void fullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        Debug.Log(fullscreen);
        if (!Screen.fullScreen) {
            Screen.SetResolution(640, 480, true);
        } else {
            Screen.SetResolution(1920, 1080, true);
        }
    }
    void toggleScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (!Screen.fullScreen) {
            Screen.SetResolution(640, 480, true);
        } else {
            Screen.SetResolution(1920, 1080, true);
        }
    }


}
