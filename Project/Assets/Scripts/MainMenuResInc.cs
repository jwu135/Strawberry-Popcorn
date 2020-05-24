using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuResInc : MonoBehaviour
{
    override public string ToString()
    {
        GameObject.Find("EventSystem").GetComponent<MainMenuSettings>().changeRes(1);
        return "";
    }
}
