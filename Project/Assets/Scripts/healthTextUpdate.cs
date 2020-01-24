using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthTextUpdate : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }
    public void updateHealth(float health, float maxhealth)
    {
        text.text = health.ToString() + "/" + maxhealth.ToString();
    }
}
