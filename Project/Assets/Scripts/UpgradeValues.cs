using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeValues : MonoBehaviour
{
    public static double bonusHealth;
    public static double bonusAttackSpd;
    public static double bonusAttackDmg;
    public static double bonusManaGain;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SavePlayer()
    {

        Debug.Log("save");
    }

    public void LoadPlayer()
    {

        Debug.Log("load");
    }

    public void BonusHealth()
    {

        Debug.Log("hp");
        Debug.Log(bonusHealth);
    }

    public void BonusAttackSpd()
    {

        Debug.Log("as");
        Debug.Log(bonusAttackSpd);
    }

    public void BonusAttackDmg()
    {

        Debug.Log("dmg");
        Debug.Log(bonusAttackDmg);
    }

    public void BonusManaGain()
    {

        Debug.Log("mg");
        Debug.Log(bonusManaGain);
    }
}
