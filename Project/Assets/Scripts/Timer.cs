using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public GameObject NormalAttackCD;
    public GameObject NormalAttack1UI;
    public Text ProgressIndicator;
    public Image LoadingBar;
    double currentValue;
    public float speed;
    float float1;
    float float2;

    public PlayerCombat PlayerCombat;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentValue < 100)
        {
            currentValue += Time.deltaTime/2;
            ProgressIndicator.text = ((int)currentValue).ToString();
            NormalAttackCD.SetActive(true);
            NormalAttack1UI.SetActive(true);
        }
        else
        {
            NormalAttackCD.SetActive(false);
            ProgressIndicator.text = "Done"; 
        }

        float1 = (float)(currentValue / PlayerCombat.timeBtwChargeAttack1 );
        LoadingBar.fillAmount = float1; 
        LoadingBar.material.color = new Color(1.0f, 1.0f, 1.0f, float1);
    }
}

