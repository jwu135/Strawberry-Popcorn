using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    override public string ToString()
    {
        Application.Quit();
        return "Goodbye";
    }
}
