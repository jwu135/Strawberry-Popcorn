using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSettings : MonoBehaviour
{
    public Toggle changeScreen;
    public Text resolutionText;
    private Resolution defaultRes;
    private Resolution[] resolutions = new Resolution[6];
    private int resIndex = 0;
    private void Start()
    {
        resolutions[0].width = 640;
        resolutions[0].height = 360;
        resolutions[1].width = 1280;
        resolutions[1].height = 720;
        resolutions[2].width = 1920;
        resolutions[2].height = 1080;
        resolutions[3].width = 2432;
        resolutions[3].height = 1368;
        resolutions[4].width = 3200;
        resolutions[4].height = 1800;
        resolutions[5].width = 3840;
        resolutions[5].height = 2160;

        int dif = 10000;
        for (int i = 0; i < resolutions.Length; i++) {
            int temp = Mathf.Abs(Screen.currentResolution.width - resolutions[i].width);
            if (temp < dif) {
                dif = temp;
                resIndex = i;
            }
        }
        resolutionText.text = resolutions[resIndex].width + " x " + resolutions[resIndex].height;
    }
    void Awake()
    {
        if (changeScreen != null) {
            changeScreen.GetComponent<Toggle>().isOn = Screen.fullScreen;

            
        }
    }
    public void changeRes(int amt)
    {
        resIndex += amt;
        resIndex = Mathf.Clamp(resIndex, 0, 5);
        resolutionText.text = resolutions[resIndex].width + " x " + resolutions[resIndex].height;
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, Screen.fullScreen);
        Debug.Log("changed res");
    }
    public void fullscreen(bool fullscreen)
    {
        Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, fullscreen);
        Debug.Log(fullscreen);
    }
    void toggleScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (!Screen.fullScreen) {
            Screen.SetResolution(resolutions[resIndex].width, resolutions[resIndex].height, true);
        } else {
            Screen.SetResolution(1920, 1080, true);
        }
    }


}
