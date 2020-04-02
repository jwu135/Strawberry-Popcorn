using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public double maxHealth = 100;
    public double health = 100;
    public double maxMana = 100;
    public double mana = 100;

    public float invicibilityLength;
    public float invicibilityCounter;
    public float manaCounter = 1;
    public float pauseTime = 100; //in milliseconds
    private float timer = 0; //used for pause frames
    private float scaleTime = 0f;
    private GameObject[] HitOverlayArr;
    private SpriteRenderer HitOverlay;


    public Text helthText;
    public Text manaText;

    private float alphaLevel = 1f; //Used to control the opacity of the hit overlay
    public float alphaStep = 0.005f; //how fast the opacity decays 1f is fully opaque, 0f is full transparent

    public PlayerCombat PlayerCombat;

    // Start is called before the first frame update
    void Start()
    {
        HitOverlayArr = GameObject.FindGameObjectsWithTag("HitOverlay");
        HitOverlay = HitOverlayArr[0].GetComponent<SpriteRenderer>();
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
            GameObject.Find("EventSystem").GetComponent<gameOver>().startGameOver(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invicibilityCounter <= 0)
        {
            if (other.tag == "BossBullet")
            {
                Time.timeScale = scaleTime;
                health -= 1;
                PlayerCombat.Hurt();
                helthText.text = health.ToString() + "/" + maxHealth.ToString();
                alphaLevel = 1f;
                HitOverlay.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        else
        {
            if (other.tag == "BossBullet" && manaCounter == 1)
            {
                Time.timeScale = scaleTime;
                mana += 5;
                manaCounter = 0;
                manaText.text = mana.ToString() + "/" + maxMana.ToString();
            }
        }
    }


    public void updateHealth(double health, double maxHealth)
    {
        helthText.text = health.ToString() + "/" + maxHealth.ToString();
    }

    public void updateMana(double mana, double maxMana)
    {
        manaText.text = mana.ToString() + "/" + maxMana.ToString();
    }
}
