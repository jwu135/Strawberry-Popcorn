using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayScript : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "x " + UpgradeValues.upgradePoints;
    }
}
