using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public class Boss : MonoBehaviour
{
    private double health = 100;
    public float maxhealth = 100;
    public GameObject[] armatures;
    public Texture[] textures;
    public double[] healthNew = {25f,25f,25f,30f};
    private double[] maxhealthNew;
    public Image HealthBar;
    private Text text;
    private GameObject player;
    private float[] healthPoints = new float[4];
    private int healthIndex = 0;
    private int phase = 0;
    private bool damageable = true;
    private bool disablePause = false;
    // Start is called before the first frame update
    void Start()
    {
        maxhealthNew = new double[healthNew.Length+1];
        Array.Copy(healthNew, maxhealthNew, healthNew.Length);
        for(int i = 0; i < healthPoints.Length; i++) {
            healthPoints[i] = maxhealth - i*maxhealth/healthPoints.Length;
        }
        text = GameObject.Find("BossHealth").transform.Find("Text").gameObject.GetComponent<Text>(); // just wanted to try to find it without making it public
        player = GameObject.FindWithTag("Player");
        updateHealth();
    }
    public int getPhase()
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
        text.text = (healthNew[healthIndex]).ToString() + "/" + (maxhealthNew[healthIndex]).ToString();
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
    public void losehealth(double amnt)
    {
        if (damageable) {
            healthNew[healthIndex] -= amnt;
            player.GetComponent<HealthManager>().activateScreenShake((float)amnt/4);
            StartCoroutine("hit");
            if (healthNew[healthNew.Length - 1] <= 0) {
                GameObject.FindGameObjectWithTag("EventSystem").GetComponent<gameOver>().startGameOver(true);
            } else if (healthNew[healthIndex] <= 0) {
                GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"), transform.position, transform.rotation) as GameObject;
                Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f) * 5;
                healthIndex++;
                phase++;
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
    // So happy that I finally figured out how this works
    IEnumerator hit() 
    {
        armatures[0].GetComponent<UnityArmatureComponent>().unityData.textureAtlas[0].material.mainTexture = textures[1];
        yield return new WaitForSeconds(.2f);
        armatures[0].GetComponent<UnityArmatureComponent>().unityData.textureAtlas[0].material.mainTexture = textures[0];
    }
}
