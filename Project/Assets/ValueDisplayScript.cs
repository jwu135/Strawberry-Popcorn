using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Text>().text = "x "+UpgradeValues.upgradePoints;    
        if (GlobalVariable.deathCounter <= 1) {
            UpgradeValues.upgradePoints = 2;
            UpgradeValues.deathPointsUsed = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "x " + UpgradeValues.upgradePoints;
    }
}
