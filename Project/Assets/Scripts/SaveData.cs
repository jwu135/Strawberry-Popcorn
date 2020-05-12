using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData 
{

    public double bonusHealth;
    public double bonusAttackSpd;
    public double bonusManaGain;
    public double weaponAmount;
    public double[] weaponChoice;

    public SaveData (Upgrade Upgrade)
    {
        bonusHealth = UpgradeValues.bonusHealth;
        bonusAttackSpd = UpgradeValues.bonusAttackSpd;
        bonusManaGain = UpgradeValues.bonusManaGain;
        //weaponAmount = UpgradeValues.weaponAmount;

        weaponChoice = new double[3];
       // weaponChoice[0] = UpgradeValues.weaponChoice1;
       // weaponChoice[1] = UpgradeValues.weaponChoice2;
      //  weaponChoice[2] = UpgradeValues.weaponChoice3;


    }

}
