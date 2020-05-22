using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    //public double upgradePoints;
    public SceneChanger SceneChanger;
    

    // Start is called before the first frame update
    void Start()
    {
        //UpgradeValues.deathPoints = 2;
        //UpgradeValues.bonusHealth += 5;
        // Debug.Log(UpgradeValues.bonusHealth);
        
        if (SceneChanger.scene.name.Equals("Gameover"))
        {
            UpgradeValues.upgradeLocation = 0;
        }
        if (SceneChanger.scene.name.Equals("Upgrade"))
        {
            UpgradeValues.upgradeLocation = 1;
        }
        //if(UpgradeValues.deathPointsUsed == 0)
        //{
      
            UpgradeValues.upgradePoints += UpgradeValues.deathPoints;
            UpgradeValues.deathPointsUsed += UpgradeValues.deathPoints;
 
        //UpgradeValues.deathProfit = true;
        //}
        UpgradeValues.deathPoints = 0;
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
        UpgradeValues.upgradeLocation = data.upgradeLocation;
        UpgradeValues.upgradePoints = data.upgradePoints;
        //UpgradeValues.deathProfit = data.deathProfit;
        UpgradeValues.deathPoints = data.deathPoints;
        UpgradeValues.deathPointsUsed = data.deathPointsUsed;
        //weaponAmount = data.weaponAmount;
        Debug.Log("load");
    }

    public void BonusHealth()
    {
        if(UpgradeValues.upgradePoints > 0 && UpgradeValues.bonusHealth < 10)
        {
            UpgradeValues.upgradePoints -= 1;
            UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusHealth += 1;
            Debug.Log("hp");
            Debug.Log(UpgradeValues.bonusHealth);
            if (UpgradeValues.deathPointsUsed < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
        
    }

    public void BonusAttackSpd()
    {
        if (UpgradeValues.bonusAttackSpd < 0.2 && UpgradeValues.upgradePoints > 0)
        {
            UpgradeValues.upgradePoints -= 1;
            UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusAttackSpd += 0.01;
            Debug.Log("as");
            Debug.Log(UpgradeValues.bonusAttackSpd);
            if (UpgradeValues.deathPointsUsed < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
        
    }

    public void BonusAttackDmg()
    {
        if (UpgradeValues.bonusAttackDmg < 5 && UpgradeValues.upgradePoints > 0)
        {
            UpgradeValues.upgradePoints -= 1;
            UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusAttackDmg += 1;
            Debug.Log("dmg");
            Debug.Log(UpgradeValues.bonusAttackDmg);
            if (UpgradeValues.deathPointsUsed < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
            
    }

    public void BonusManaGain()
    {
        if (UpgradeValues.bonusManaGain < 5 && UpgradeValues.upgradePoints > 0)
        {
            UpgradeValues.upgradePoints -= 1;
            UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusManaGain += 1;
            Debug.Log("mg");
            Debug.Log(UpgradeValues.bonusManaGain);
            if (UpgradeValues.deathPointsUsed < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
            
    }

    public void NewGame()
    {
        UpgradeValues.bonusHealth = 0;
        UpgradeValues.bonusAttackSpd = 0;
        UpgradeValues.bonusAttackDmg = 0;
        UpgradeValues.bonusManaGain = 0;
        UpgradeValues.upgradeLocation = 0;
        UpgradeValues.upgradePoints = 0;
        UpgradeValues.deathPointsUsed = 0;
        Debug.Log("newgame");
        Debug.Log(UpgradeValues.bonusHealth);
        Debug.Log(UpgradeValues.bonusAttackSpd);
        Debug.Log(UpgradeValues.bonusManaGain);
        SavePlayer();
    }
}

