using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class Boss : MonoBehaviour
{
    //private double health = 100;
    public float maxhealth = 100;
    public GameObject[] armatures;
    public Texture[] textures;
    public double[] healthNew = {25f,25f,25f,30f};
    public GameObject[] platforms;
    public GameObject[] lights;
    public GameObject finalPhaseHurtArea;
    private double[] healthPhases = new double[4]; // holds each health point for the boss
    private double[] maxhealthNew; // used for healthbar and whatnot
    public Image HealthBar;
    [HideInInspector]
    public GameObject[] mothers = new GameObject[4];
    private GameObject player;
    private int healthIndex = 0;
    private int healthPhasesIndex = 0;
    private float phase = 0;
    private bool damageable = true;
    private bool disablePause = false;
    private GameObject CornerMother;
    private GameObject CornerMother2;
    // Start is called before the first frame update

    void midGameOverride() // taken from RaShaun's Upgrade.cs, needed so I don't have to worry about the gameover screen issue
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
    }
    void Start()
    {
        // hard coding these for now
        healthNew[0] = 60;
        healthNew[1] = 190;
        healthNew[2] = 270;
        healthNew[3] = 360;
        if(UpgradeValues.deathCounter==0)
            midGameOverride();
        maxhealthNew = new double[healthNew.Length+1];
        Array.Copy(healthNew, maxhealthNew, healthNew.Length);
        swapPhase(0);
        player = GameObject.FindWithTag("Player");
        updateHealth();
    }
    public float getPhase()
    {
        return phase;
    }
    public void setDamageable(bool truth)
    {
        damageable = truth;
        GetComponent<CircleCollider2D>().enabled = (damageable) ? true : false;
    }
    private void updateHealth()
    {
        Vector2 temp = HealthBar.rectTransform.sizeDelta;
        temp.x = 375.56f * (float)((healthNew[healthIndex]) / (maxhealthNew[healthIndex])); // changing here to show health
        HealthBar.rectTransform.sizeDelta = temp;
    }
    private void Update()
    {
        if (disablePause) {
            Time.timeScale = 1;
            player.GetComponent<HealthManager>().timer = 0;
            //player.tag = "";
            disablePause = false;
        }
    }

    private void swapPhase(int index,int phases = 4)
    {
        double tempHealth = healthNew[index];
        for(int i = 0; i < phases; i++) {
            healthPhases[i] = tempHealth - ((i+1) * tempHealth / phases);
        }
        healthPhasesIndex = 0;
    }

    public void toggleSprite(bool enable)
    {
        foreach (GameObject mother in mothers) {
            mother.GetComponent<SpriteRenderer>().enabled = enable;
        }
    }

    public void destroyBuildDestroy()
    {
        foreach (GameObject tempFloor in platforms) {
            Destroy(tempFloor);
        }
        
        GameObject finalMothers = Resources.Load("Prefabs/4thPhaseMothers") as GameObject;
        player.GetComponent<PlayerController>().setMode(0);
        mothers[0] = gameObject;
        mothers[1] = Instantiate(finalMothers, new Vector3(26.46f, 4.1f, 0f), transform.rotation, transform.parent);
        mothers[2] = Instantiate(finalMothers, new Vector3(-28.44f, 4.1f, 0f), transform.rotation, transform.parent);
        mothers[3] = Instantiate(finalMothers, new Vector3(-1.892f, -7.8265f, 0f), transform.rotation, transform.parent);
        mothers[3].GetComponent<LastPhaseBossShoot>().bottomEye = true;
        foreach (GameObject mother in mothers) {
            mother.transform.localScale = Vector3.Scale(mother.transform.localScale, new Vector3(0.75f, 0.75f, 1f));
        }
        //toggleSprite(false);
    }
    public void losehealth(double amnt)
    {
        if (damageable) {
            /*if (amnt < 2)
                SoundManager.PlaySound("enemyHit2");
            else
                SoundManager.PlaySound("enemyHit1");*/
            SoundManager.PlaySound("enemyHit1");
            healthNew[healthIndex] -= amnt;

            
            while (healthPhasesIndex < healthPhases.Length && healthNew[healthIndex] <= healthPhases[healthPhasesIndex]) {
                phase += 0.25f;
                healthPhasesIndex++;
                GetComponent<BossShoot>().setPhase(phase);
                if (phase % 0.5f == 0&& phase%1f != 0) {
                    float randDrop = UnityEngine.Random.Range(0f, 1f);
                    if (randDrop > 0.25f) {
                        GameObject tempObj = Resources.Load("Prefabs/UpgradePiece") as GameObject;
                        GameObject upgradePiece = Instantiate(tempObj, transform.position, transform.rotation);
                        SoundManager.PlaySound("pieceFall");
                    }
                }
            }

            float shakeAmount = Mathf.Clamp((float)amnt / 7f,0f,1.6f);
            player.GetComponent<HealthManager>().activateScreenShake(shakeAmount);
            if (healthNew[healthNew.Length - 1] <= 0) {
                GameObject.FindGameObjectWithTag("EventSystem").GetComponent<gameOver>().startGameOver(true);
            } else if (healthNew[healthIndex] <= 0) {
                setDamageable(false);
                GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"), transform.position, GameObject.FindGameObjectWithTag("PieceOne").transform.rotation) as GameObject;
                SoundManager.PlaySound("pieceFall");
                Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f) * 5;
                GetComponent<SpriteRenderer>().enabled = false;
                /*if(healthIndex!=0)
                    phase+=1f;*/
                if (phase > UpgradeValues.highestPhaseEncountered) {
                    UpgradeValues.highestPhaseEncountered = phase;
                    UpgradeValues.highestPhaseEncounteredBoss = phase; // I could've combined these two, but oh well
                }
                if (phase == 1) {
                    GameObject TempCornerMother = Resources.Load("Prefabs/CornerMother") as GameObject;
                    CornerMother = Instantiate(TempCornerMother,transform.parent.transform.position+new Vector3(25f,0f,0f),transform.rotation);
                    CornerMother.transform.parent = transform.parent;
                    CornerMother2 = Instantiate(TempCornerMother, transform.parent.transform.position + new Vector3(-25f, 0f, 0f), transform.rotation);
                    CornerMother2.transform.parent = transform.parent;
                    CornerMother.GetComponent<CornerBossShoot>().offsetCooldown();
                }
                if (phase == 3) {
                    
                    //finalPhaseHurtArea.SetActive(true);
                    Piece.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                    Destroy(CornerMother);
                    Destroy(CornerMother2);
                    lights[0].SetActive(false);
                    lights[1].SetActive(true);
                   
                    
                    //Camera.main.orthographicSize = 25; // this might be necessary
                }
                GameObject.Find("Border").GetComponent<Animator>().SetTrigger("Down");
                swapPhase((int)phase);
                healthIndex++;
                Movement movement = player.GetComponent<Movement>();
                movement.getArmature().animation.Stop();
                movement.setPrimaryArmature(movement.getPrimaryIndex());
                movement.getArmature().animation.Play("Idle");
                GameObject.FindGameObjectWithTag("EventSystem").GetComponent<CutsceneSystem>().cutscene(Piece);
                disablePause = true;
            }
            updateHealth();
        }
    }

    // So happy that I finally figured out how this works // And now it's not needed anymore :D
    IEnumerator hit() 
    {
        armatures[0].GetComponent<UnityArmatureComponent>().unityData.textureAtlas[0].material.mainTexture = textures[1];
        yield return new WaitForSeconds(.2f);
        armatures[0].GetComponent<UnityArmatureComponent>().unityData.textureAtlas[0].material.mainTexture = textures[0];
    }
}
