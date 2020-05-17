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
    private double[] healthPhases = new double[4]; // holds each health point for the boss
    private double[] maxhealthNew; // used for healthbar and whatnot
    public Image HealthBar;
    private GameObject player;
    private int healthIndex = 0;
    private int healthPhasesIndex = 0;
    private float phase = 0;
    private bool damageable = true;
    private bool disablePause = false;
    // Start is called before the first frame update
    void Start()
    {
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
                Debug.Log("Health Phase Index "+healthPhasesIndex);
                phase += 0.25f;
                healthPhasesIndex++;
                GetComponent<BossShoot>().setPhase(phase);
                Debug.Log(phase);
            }
            

            player.GetComponent<HealthManager>().activateScreenShake((float)amnt/4);
            if (healthNew[healthNew.Length - 1] <= 0) {
                GameObject.FindGameObjectWithTag("EventSystem").GetComponent<gameOver>().startGameOver(true);
            } else if (healthNew[healthIndex] <= 0) {
                setDamageable(false);
                GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"), transform.position, GameObject.FindGameObjectWithTag("PieceOne").transform.rotation) as GameObject;
                Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f) * 5;
                /*if(healthIndex!=0)
                    phase+=1f;*/
                if (phase == 1) {
                    GameObject TempCornerMother = Resources.Load("Prefabs/CornerMother") as GameObject;
                    GameObject CornerMother = Instantiate(TempCornerMother,transform.parent.transform.position+new Vector3(25f,0f,0f),transform.rotation);
                    CornerMother.transform.parent = transform.parent;
                    GameObject CornerMother2 = Instantiate(TempCornerMother, transform.parent.transform.position + new Vector3(-25f, 0f, 0f), transform.rotation);
                    CornerMother2.transform.parent = transform.parent;
                }
                swapPhase((int)phase);
                healthIndex++;
                Debug.Log(phase);
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
