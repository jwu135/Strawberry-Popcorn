using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuVolume1 : MonoBehaviour
{
    public Text volumeText;
    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    void updateText()
    {
        volumeText.text = Mathf.Round(UpgradeValues.overallvolume * 100) + "%";   
    }

    public void incVolume()
    {
        if(UpgradeValues.overallvolume < 1)
            UpgradeValues.overallvolume += .05f;
        updateText();
    }
    public void decVolume()
    {
        if (UpgradeValues.overallvolume > 0)
            UpgradeValues.overallvolume -= .05f;
        updateText();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
