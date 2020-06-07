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
    private bool build1;
    private bool build2;
    private bool build3;



    // Start is called before the first frame update
    void Start()
    {
        if(UpgradeValues.deathCounter == 1)
            UpgradeValues.deathPoints = deathStandard;


        //UpgradeValues.bonusHealth += 5;
        // Debug.Log(UpgradeValues.bonusHealth);
        
        if (SceneChanger.scene.name.Equals("Gameover"))
        {
            UpgradeValues.upgradeLocation = 0;
            UpgradeValues.builtCorpse = false;

            if (UpgradeValues.deathPointsUsed < (UpgradeValues.deathPoints * UpgradeValues.deathCounter))
            {
                UpgradeValues.upgradePoints += UpgradeValues.deathPoints;
                UpgradeValues.deathPointsUsed += UpgradeValues.deathPoints;
                UpgradeValues.deathProfit = true;
                Debug.Log(UpgradeValues.upgradePoints);
                Debug.Log(UpgradeValues.deathPointsUsed);
            }
            if (build1)
            {
                BreakHealth1();
                BreakDmg1();
                BreakMana1();

            }
            if (build2)
            {
                BreakHealth2();
                BreakDmg2();
                BreakMana2();

            }
            if (build3)
            {
                BreakHealth3();
                BreakDmg3();
                BreakMana3();
                Debug.Log("breakkkkkkk");

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
        UpgradeValues.buildHealth1 = data.buildHealth1;
        UpgradeValues.buildDmg1 = data.buildDmg1;
        UpgradeValues.buildMana1 = data.buildMana1;
        UpgradeValues.buildHealth2 = data.buildHealth2;
        UpgradeValues.buildDmg2 = data.buildDmg2;
        UpgradeValues.buildMana2 = data.buildMana2;
        UpgradeValues.buildHealth3 = data.buildHealth3;
        UpgradeValues.buildDmg3 = data.buildDmg3;
        UpgradeValues.buildMana3 = data.buildMana3;
        UpgradeValues.choseCorpse1 = data.choseCorpse1;
        UpgradeValues.choseCorpse2 = data.choseCorpse2;
        UpgradeValues.choseCorpse3 = data.choseCorpse3;
        UpgradeValues.builtCorpse = data.builtCorpse;
        //weaponAmount = data.weaponAmount;
        UpgradeValues.highestPhaseEncountered = data.highestPhaseEncountered;
        UpgradeValues.highestPhaseEncounteredBoss = data.highestPhaseEncounteredBoss;
        UpgradeValues.highestPhaseDiscussed = data.highestPhaseDiscussed;
        UpgradeValues.highestPhaseDiscussedBoss = data.highestPhaseDiscussedBoss;
        UpgradeValues.positionValues = data.positionValues;
        UpgradeValues.bodyTypes = data.bodyTypes;
        UpgradeValues.addedCorpse = data.addedCorpse;

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
        if(UpgradeValues.upgradePoints > 0 && UpgradeValues.bonusHealth < 15)
        {
            SoundManager.PlaySound("gainLevel");
            UpgradeValues.upgradePoints -= 1;
            //UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusHealth += 1;
            Debug.Log("hp");
            Debug.Log(UpgradeValues.bonusHealth);
            if (UpgradeValues.upgradePoints < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
        
    }

    public void BonusAttackSpd()
    {
        if (UpgradeValues.bonusAttackSpd < 0.15 && UpgradeValues.upgradePoints > 0)
        {
            SoundManager.PlaySound("gainLevel");
            UpgradeValues.upgradePoints -= 1;
           // UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusAttackSpd += 0.01;
            Debug.Log("as");
            Debug.Log(UpgradeValues.bonusAttackSpd);
            if (UpgradeValues.upgradePoints < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
        
    }

    public void BonusAttackDmg()
    {
        if (UpgradeValues.bonusAttackDmg < 15 && UpgradeValues.upgradePoints > 0)
        {
            SoundManager.PlaySound("gainLevel");
            UpgradeValues.upgradePoints -= 1;
           // UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusAttackDmg += 1;
            Debug.Log("dmg");
            Debug.Log(UpgradeValues.bonusAttackDmg);
            if (UpgradeValues.upgradePoints < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
            
    }

    public void BonusManaGain()
    {
        if (UpgradeValues.bonusManaGain < 15 && UpgradeValues.upgradePoints > 0)
        {
            SoundManager.PlaySound("gainLevel");
            UpgradeValues.upgradePoints -= 1;
           // UpgradeValues.deathPointsUsed -= 1;
            UpgradeValues.bonusManaGain += 1;
            Debug.Log("mg");
            Debug.Log(UpgradeValues.bonusManaGain);
            if (UpgradeValues.upgradePoints < 1)
            {
                UpgradeValues.deathProfit = false;
            }
            SavePlayer();
        }
            
    }

    public void BuildHealth1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth += UpgradeValues.buildHealth1;
        build1 = true;
        SavePlayer();
    }
    public void BuildDmg1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg += UpgradeValues.buildDmg1;
        SavePlayer();
    }
    public void BuildMana1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain += UpgradeValues.buildMana1;
        SavePlayer();
    }

    public void BuildHealth2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth += UpgradeValues.buildHealth2;
        build2 = true;
        SavePlayer();
    }
    public void BuildDmg2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg += UpgradeValues.buildDmg2;
        SavePlayer();
    }
    public void BuildMana2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain += UpgradeValues.buildMana2;
        SavePlayer();
    }

    public void BuildHealth3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth += UpgradeValues.buildHealth3;
        build3 = true;
        SavePlayer();
    }
    public void BuildDmg3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg += UpgradeValues.buildDmg3;
        SavePlayer();
    }
    public void BuildMana3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain += UpgradeValues.buildMana3;
        SavePlayer();
    }

    public void BreakHealth1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth -= UpgradeValues.buildHealth1;
        build1 = false;
        SavePlayer();
    }
    public void BreakDmg1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg -= UpgradeValues.buildDmg1;
        SavePlayer();
    }
    public void BreakMana1()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain -= UpgradeValues.buildMana1;
        SavePlayer();
    }

    public void BreakHealth2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth -= UpgradeValues.buildHealth2;
        build2 = false;
        SavePlayer();
    }
    public void BreakDmg2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg -= UpgradeValues.buildDmg2;
        SavePlayer();
    }
    public void BreakMana2()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain -= UpgradeValues.buildMana2;
        SavePlayer();
    }

    public void BreakHealth3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusHealth -= UpgradeValues.buildHealth3;
        build3 = false;
        SavePlayer();
    }
    public void BreakDmg3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusAttackDmg -= UpgradeValues.buildDmg3;
        SavePlayer();
    }
    public void BreakMana3()
    {
        SoundManager.PlaySound("gainLevel");
        UpgradeValues.bonusManaGain -= UpgradeValues.buildMana3;
        SavePlayer();
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
        UpgradeValues.highestPhaseEncountered = 0;
        UpgradeValues.highestPhaseEncounteredBoss = 0;
        UpgradeValues.highestPhaseDiscussed = 0;
        UpgradeValues.highestPhaseDiscussedBoss = 0;
        UpgradeValues.buildHealth1 = 0;
        UpgradeValues.buildDmg1 = 0;
        UpgradeValues.buildMana1 = 0;
        UpgradeValues.buildHealth2 = 0;
        UpgradeValues.buildDmg2 = 0;
        UpgradeValues.buildMana2 = 0;
        UpgradeValues.buildHealth3 = 0;
        UpgradeValues.buildDmg3 = 0;
        UpgradeValues.buildMana3 = 0;
        UpgradeValues.choseCorpse1 = false;
        UpgradeValues.choseCorpse2 = false;
        UpgradeValues.choseCorpse3 = false;
        UpgradeValues.builtCorpse = false;
        UpgradeValues.addedCorpse = false;
        Debug.Log("newgame");
        Debug.Log(UpgradeValues.bonusHealth);
        Debug.Log(UpgradeValues.bonusAttackSpd);
        Debug.Log(UpgradeValues.bonusManaGain);
        SavePlayer();
    }
}

