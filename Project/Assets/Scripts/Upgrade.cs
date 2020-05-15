using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public double upgradePoints;

    // Start is called before the first frame update
    void Start()
    {
        //UpgradeValues.bonusHealth += 5;
        // Debug.Log(UpgradeValues.bonusHealth);
        upgradePoints += 1;
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
        UpgradeValues.bonusAttackDmg = data.bonusAttackDmg;
        UpgradeValues.bonusManaGain = data.bonusManaGain;
        //weaponAmount = data.weaponAmount;
        Debug.Log("load");
    }

    public void BonusHealth()
    {
        if(upgradePoints > 0 && UpgradeValues.bonusHealth < 10)
        {
            upgradePoints -= 1;
            UpgradeValues.bonusHealth += 1;
            Debug.Log("hp");
            Debug.Log(UpgradeValues.bonusHealth);
        }
        
    }

    public void BonusAttackSpd()
    {
        if (UpgradeValues.bonusAttackSpd < 0.2 && upgradePoints > 0)
        {
            upgradePoints -= 1;
            UpgradeValues.bonusAttackSpd += 0.01;
            Debug.Log("as");
            Debug.Log(UpgradeValues.bonusAttackSpd);
        }
        
    }

    public void BonusAttackDmg()
    {
        if (UpgradeValues.bonusAttackDmg < 5 && upgradePoints > 0)
        {
            upgradePoints -= 1;
            UpgradeValues.bonusAttackDmg += 1;
            Debug.Log("dmg");
            Debug.Log(UpgradeValues.bonusAttackDmg);
        }
            
    }

    public void BonusManaGain()
    {
        if (UpgradeValues.bonusManaGain < 5 && upgradePoints > 0)
        {
            upgradePoints -= 1;
            UpgradeValues.bonusManaGain += 1;
            Debug.Log("mg");
            Debug.Log(UpgradeValues.bonusManaGain);
        }
            
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

