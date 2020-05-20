﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData 
{

    public double bonusHealth;
    public double bonusAttackSpd;
    public double bonusAttackDmg;
    public double bonusManaGain;
    public double upgradeLocation;
    public double upgradePoints;
    public double deathPoints;
    public double deathPointsUsed;
    public bool deathProfit;
    public double weaponAmount;
    public double[] weaponChoice;

    public SaveData (Upgrade Upgrade)
    {
        bonusHealth = UpgradeValues.bonusHealth;
        bonusAttackSpd = UpgradeValues.bonusAttackSpd;
        bonusAttackDmg = UpgradeValues.bonusAttackDmg;
        bonusManaGain = UpgradeValues.bonusManaGain;
        upgradeLocation = UpgradeValues.upgradeLocation;
        upgradePoints = UpgradeValues.upgradePoints;
        deathProfit = UpgradeValues.deathProfit;
        deathPoints = UpgradeValues.deathPoints;
        deathPointsUsed = UpgradeValues.deathPointsUsed;


        //weaponAmount = UpgradeValues.weaponAmount;

        weaponChoice = new double[3];
       // weaponChoice[0] = UpgradeValues.weaponChoice1;
       // weaponChoice[1] = UpgradeValues.weaponChoice2;
      //  weaponChoice[2] = UpgradeValues.weaponChoice3;


    }

}
