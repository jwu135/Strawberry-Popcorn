using System.Collections;
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
    public int[] bodyTypes;
    public float[] positionValues;
    //public List<Vector2> positions = new List<Vector2>();
   // public List<Vector2> bodies = new List<Vector2>();
    public int deathCounter;
    public bool continueGame = false;

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
        deathCounter = UpgradeValues.deathCounter;
        continueGame = UpgradeValues.continueGame;

        //bodyTypes = new int[UpgradeValues.deathCounter];
       // positionValues = new float[(UpgradeValues.deathCounter * 2)];

        bodyTypes = new int[1000000];
        positionValues = new float[1000000];

        for (int i = 0; i < (UpgradeValues.deathCounter * 2); i++)
        {     
            positionValues[i] = UpgradeValues.positionValues[i];

        }

        for (int i = 0; i < UpgradeValues.deathCounter; i++)
        {
           bodyTypes[i] = UpgradeValues.bodyTypes[i];
        }


        //weaponAmount = UpgradeValues.weaponAmount;

        weaponChoice = new double[3];
       // weaponChoice[0] = UpgradeValues.weaponChoice1;
       // weaponChoice[1] = UpgradeValues.weaponChoice2;
      //  weaponChoice[2] = UpgradeValues.weaponChoice3;


    }

}
