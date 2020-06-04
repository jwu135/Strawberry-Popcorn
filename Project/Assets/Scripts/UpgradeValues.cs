using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeValues : MonoBehaviour
{
    public static double bonusHealth;
    public static double bonusAttackSpd;
    public static double bonusAttackDmg;
    public static double bonusManaGain;
    public static double upgradeLocation;
    public static double upgradePoints;
    public static double deathPoints;
    public static double deathPointsUsed;
    public static double buildHealth1;
    public static double buildDmg1;
    public static double buildMana1;
    public static double buildHealth2;
    public static double buildDmg2;
    public static double buildMana2;
    public static double buildHealth3;
    public static double buildDmg3;
    public static double buildMana3;
    public static bool deathProfit;
    public static bool continueGame = false;
    public static bool choseCorpse1 = false;
    public static bool choseCorpse2 = false;
    public static bool choseCorpse3 = false;
    public static bool builtCorpse = false;
    public static bool addedCorpse = false;
    public static int[] bodyTypes;
    public static float[] positionValues;
    public static List<Vector2> positions = new List<Vector2>();
    public static List<Sprite> bodies = new List<Sprite>();
    public static int deathCounter;
    public static float highestPhaseEncountered;
    public static float highestPhaseDiscussed; // parallax room
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
