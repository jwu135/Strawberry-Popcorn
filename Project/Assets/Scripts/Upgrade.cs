using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    //public double upgradePoints;
    public SceneChanger SceneChanger;
   // public GlobalVariable GlobalVariable;
    public double deathStandard;
    public Sprite[] berryImageDropped;


    // Start is called before the first frame update
    void Start()
    {
        UpgradeValues.deathPoints = deathStandard;
        //UpgradeValues.bonusHealth += 5;
        // Debug.Log(UpgradeValues.bonusHealth);
        if (SceneChanger.scene.name.Equals("Gameover"))
        {
            UpgradeValues.upgradeLocation = 0;

            if (!UpgradeValues.deathProfit)
            {
                UpgradeValues.upgradePoints += UpgradeValues.deathPoints;
                UpgradeValues.deathPointsUsed += UpgradeValues.deathPoints;
                UpgradeValues.deathProfit = true;
                Debug.Log(UpgradeValues.upgradePoints);
                Debug.Log(UpgradeValues.deathPointsUsed);
            }
            SavePlayer();
        }
        if (SceneChanger.scene.name.Equals("Upgrade"))
        {
            UpgradeValues.upgradeLocation = 1;
            SavePlayer();
        }

        
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
        UpgradeValues.deathProfit = data.deathProfit;
        UpgradeValues.deathCounter = data.deathCounter;
        UpgradeValues.continueGame = data.continueGame;
        //weaponAmount = data.weaponAmount;

        UpgradeValues.positionValues = data.positionValues;
        UpgradeValues.bodyTypes = data.bodyTypes;

        for (int i = 0; i < (UpgradeValues.deathCounter * 2); i++)
        {
            UpgradeValues.positionValues[i] = data.positionValues[i];

        }

        for (int i = 0; i < UpgradeValues.deathCounter; i++)
        {
            UpgradeValues.bodyTypes[i] = data.bodyTypes[i];
        }

        

        UpgradeValues.positions = new List<Vector2>();
        UpgradeValues.bodies = new List<Sprite>();

        for (int i = 0; i < (UpgradeValues.deathCounter * 2); i += 2)
        {
            //temp = ;
            Debug.Log(UpgradeValues.deathCounter);
            UpgradeValues.positions.Add(new Vector2(UpgradeValues.positionValues[i], UpgradeValues.positionValues[(i + 1)]));
        }
        
        for (int i = 0; i < UpgradeValues.deathCounter; i++)
        {
            //temp = ;
            UpgradeValues.bodies.Add(berryImageDropped[UpgradeValues.bodyTypes[i]]);
        }

        UpgradeValues.continueGame = true;
        Debug.Log("load");
    }

    public void BonusPoints()
    {
        UpgradeValues.upgradePoints += 1;
    }

    public void PhasePoints()
    {
        UpgradeValues.upgradePoints += 2;
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
        UpgradeValues.deathProfit = false;
        UpgradeValues.continueGame = false;
        UpgradeValues.deathCounter = 0;
        UpgradeValues.positions = new List<Vector2>();
        UpgradeValues.bodies = new List<Sprite>();
        GlobalVariable.positions = new List<Vector2>();
        GlobalVariable.bodies = new List<Sprite>();
        //GlobalVariable.deathCounter = 0;
        UpgradeValues.bodyTypes = new int[1000000];
        UpgradeValues.positionValues = new float[1000000];
        Debug.Log("newgame");
        Debug.Log(UpgradeValues.bonusHealth);
        Debug.Log(UpgradeValues.bonusAttackSpd);
        Debug.Log(UpgradeValues.bonusManaGain);
        SavePlayer();
    }
}

