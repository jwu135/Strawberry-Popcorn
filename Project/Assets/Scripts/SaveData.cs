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
    public double buildHealth1;
    public double buildDmg1;
    public double buildMana1;
    public double buildHealth2;
    public double buildDmg2;
    public double buildMana2;
    public double buildHealth3;
    public double buildDmg3;
    public double buildMana3;
    public double dodgeNeeded;
    public double shieldDuration;
    public bool deathProfit;
    public bool choseCorpse1;
    public bool choseCorpse2;
    public bool choseCorpse3;
    public bool builtCorpse;
    public bool addedCorpse;
    public double weaponAmount;
    public double[] weaponChoice;
    public int[] bodyTypes;
    public float[] positionValues;
    //public List<Vector2> positions = new List<Vector2>();
   // public List<Vector2> bodies = new List<Vector2>();
    public int deathCounter;
    public float highestPhaseEncountered;
    public float highestPhaseEncounteredBoss;
    public float highestPhaseDiscussed;
    public float highestPhaseDiscussedBoss;
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
        highestPhaseEncountered = UpgradeValues.highestPhaseEncountered;
        highestPhaseEncounteredBoss = UpgradeValues.highestPhaseEncounteredBoss;
        highestPhaseDiscussed = UpgradeValues.highestPhaseDiscussed;
        highestPhaseDiscussedBoss = UpgradeValues.highestPhaseDiscussedBoss;
        continueGame = UpgradeValues.continueGame;
        buildHealth1 = UpgradeValues.buildHealth1;
        buildDmg1 = UpgradeValues.buildDmg1;
        buildMana1 = UpgradeValues.buildMana1;
        buildHealth2 = UpgradeValues.buildHealth2;
        buildDmg2 = UpgradeValues.buildDmg2;
        buildMana2 = UpgradeValues.buildMana2;
        buildHealth3 = UpgradeValues.buildHealth3;
        buildDmg3 = UpgradeValues.buildDmg3;
        buildMana3 = UpgradeValues.buildMana3;
        choseCorpse1 = UpgradeValues.choseCorpse1;
        choseCorpse2 = UpgradeValues.choseCorpse2;
        choseCorpse3 = UpgradeValues.choseCorpse3;
        builtCorpse = UpgradeValues.builtCorpse;
        addedCorpse = UpgradeValues.addedCorpse;
        dodgeNeeded = UpgradeValues.dodgeNeeded;
        shieldDuration = UpgradeValues.shieldDuration;

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
