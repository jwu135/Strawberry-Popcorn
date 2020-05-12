using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        //UpgradeValues.bonusHealth += 5;
       // Debug.Log(UpgradeValues.bonusHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("save");
    }

    public void LoadPlayer()
    {
        SaveData data = SaveSystem.LoadData();

        UpgradeValues.bonusHealth = data.bonusHealth;
        UpgradeValues.bonusAttackSpd = data.bonusAttackSpd;
        UpgradeValues.bonusManaGain = data.bonusManaGain;
        //weaponAmount = data.weaponAmount;
        Debug.Log("load");
    }

    public void BonusHealth()
    {
        UpgradeValues.bonusHealth += 1;
        Debug.Log("hp");
        Debug.Log(UpgradeValues.bonusHealth);
    }

    public void BonusAttackSpd()
    {
        if (UpgradeValues.bonusAttackSpd < 0.2)
        UpgradeValues.bonusAttackSpd += 0.01;
        Debug.Log("as");
        Debug.Log(UpgradeValues.bonusAttackSpd);
    }

    public void BonusManaGain()
    {
        if (UpgradeValues.bonusManaGain < 5)
            UpgradeValues.bonusManaGain += 1;
        Debug.Log("mg");
        Debug.Log(UpgradeValues.bonusManaGain);
    }

    public void NewGame()
    {
        UpgradeValues.bonusHealth = 0;
        UpgradeValues.bonusAttackSpd = 0;
        UpgradeValues.bonusManaGain = 0;
        Debug.Log("newgame");
        Debug.Log(UpgradeValues.bonusHealth);
        Debug.Log(UpgradeValues.bonusAttackSpd);
        Debug.Log(UpgradeValues.bonusManaGain);
        SavePlayer();
    }
}

