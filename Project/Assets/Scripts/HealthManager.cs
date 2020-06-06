using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public double maxHealth = 12;
    public double health = 12;
    public double maxMana = 8;
    public double mana = 8;
    private double lastmana = 0;

    public float invicibilityLength;
    public float invicibilityCounter;
    public float manaCounter = 1;
    public float pauseTime = 100; //in milliseconds
    public float timer = 0; //used for pause frames
    private float scaleTime = 0f;
    private GameObject[] HitOverlayArr;
    private SpriteRenderer HitOverlay;

    public bool hit;

    public GameObject manaSprite;
    public Sprite[] allManaSprites;
    public Text helthText;
    public Text manaText;
    public GameObject manaEye;

    private float alphaLevel = 0f; //Used to control the opacity of the hit overlay
    public float alphaStep = 0.005f; //how fast the opacity decays 1f is fully opaque, 0f is full transparent

    public PlayerCombat PlayerCombat;
    public FakeCannon FakeCannon;
    public UpgradeValues UpgradeValues;

    private FlightMovementPhys flightMovementPhys;
    private PlayerController playerController;
    private PlatformMovementPhys PlatformMovementPhys;
    // Start is called before the first frame update
    void Awake()
    {
        maxHealth += UpgradeValues.bonusHealth;
        health += UpgradeValues.bonusHealth;
        HitOverlayArr = GameObject.FindGameObjectsWithTag("HitOverlay");
        HitOverlay = HitOverlayArr[0].GetComponent<SpriteRenderer>();
        hit = false;
        flightMovementPhys = GetComponent<FlightMovementPhys>();
        playerController = GetComponent<PlayerController>();
        PlatformMovementPhys = GetComponent<PlatformMovementPhys>();
        manaEye.SetActive(false);
    }

    void Update()
    {
        

        if (Time.timeScale == scaleTime && timer <= pauseTime)
        {
            timer += Time.unscaledDeltaTime * 1000;
            
        }
        else if(timer >= pauseTime)
        {
            Time.timeScale = 1;
            timer = 0;
            HitOverlay.color = new Color(1f, 1f, 1f, 0f);
        }

        if (alphaLevel - alphaStep > 0 && timer == 0 )
        {
            alphaLevel -= alphaStep;
            HitOverlay.color = new Color(1f, 1f, 1f, alphaLevel);
        }
        else if(alphaLevel - alphaStep < 0 && timer == 0 )
        {
            alphaLevel = 0;
            HitOverlay.color = new Color(1f, 1f, 1f, alphaLevel);
        }

        manaText.text = mana.ToString();
        if (mana == 0)
        {
            manaText.enabled = false;
            manaEye.SetActive(true);

        }
    }
    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (invicibilityCounter > 0)
        {
            //Debug.Log("Invincible");
            invicibilityCounter -= 1;
        }

        if (health <= 0)
        {
            GlobalVariable.deathCounter += 1;
            UpgradeValues.deathCounter += 1;
            GameObject.Find("EventSystem").GetComponent<gameOver>().startGameOver(false);
        }
        if (mana != lastmana) {
            updateMana();
            lastmana = mana;
        }
    }

    private void updateMana()
    {
        if (mana < 7) {
            manaSprite.GetComponent<SpriteRenderer>().sprite = allManaSprites[0];
        } else if (mana >= 20 && mana < 50) {
            manaSprite.GetComponent<SpriteRenderer>().sprite = allManaSprites[1];
        } else if (mana >= 50 && mana < 100) {
            manaSprite.GetComponent<SpriteRenderer>().sprite = allManaSprites[2];
        } else {
            manaSprite.GetComponent<SpriteRenderer>().sprite = allManaSprites[3];
        }
        if (mana >= 20 && mana < 50 && lastmana < 20) {
            GameObject manaObject = GameObject.FindGameObjectWithTag("ManaOverlay");
            manaObject.GetComponent<Animator>().SetTrigger("Play");
        }
        if (mana >= 50 && mana < 100 && lastmana < 50) {
            GameObject manaObject = GameObject.FindGameObjectWithTag("ManaOverlay");
            manaObject.GetComponent<Animator>().SetTrigger("Play");
        }
        if (mana >= 100 && lastmana < 100) {
            GameObject manaObject = GameObject.FindGameObjectWithTag("ManaOverlay");
            manaObject.GetComponent<Animator>().SetTrigger("Play");
        }

        //GameObject.FindGameObjectWithTag("ManaOverlay");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invicibilityCounter <= 0)
        {
            if (other.tag == "BossBullet")
            {
                Time.timeScale = scaleTime;
                health -= 1;
                SoundManager.PlaySound("playerHit");
                PlayerCombat.Hurt();
                GameObject.Find("EventSystem").GetComponent<PlayerHeartsController>().losehealth();
                if ((FakeCannon.charging && PlayerCombat.longPress) || (FakeCannon.maxCharge && PlayerCombat.longPress))
                {
                    hit = true; 
                }
                invicibilityCounter = invicibilityLength;
                //helthText.text = health.ToString() + "/" + maxHealth.ToString();
                alphaLevel = 1f;
                HitOverlay.color = new Color(1f, 1f, 1f, 1f);
                //activateScreenShake(1);
            }
        }
        else
        {
            if (other.tag == "BossBullet" && manaCounter == 1) {
                // only gives mana while rolling
                if(((flightMovementPhys.rollingFrame > 0 && playerController.getMode() == 0) || (PlatformMovementPhys.rollingFrame > 0 && playerController.getMode() == 1))){ // from RaShaun's/Ethan's scripts 

                        GameObject.Find("PerfectDodgeFlash").GetComponent<Animator>().speed = 1.5f;
                        GameObject.Find("PerfectDodgeFlash").GetComponent<Animator>().SetTrigger("Play");
                    //Time.timeScale = scaleTime;
                    if(mana > 0)
                    {
                        mana -= 1;
                       
                    }
                    manaCounter = 0;
                }

            }
        }
    }


    public void updateHealth(double health, double maxHealth)
    {
        helthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void updateMana(double mana)
    {
        manaText.text = mana.ToString();
        if(mana == 0)
        {
            manaText.enabled = false;
            manaEye.SetActive(true);

        }
    }
    public void activateScreenShake( float magnitude )
    {
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<ScreenShake>().shakeCamera( magnitude );
    }
            
}
