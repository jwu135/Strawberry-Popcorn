using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    public double health = 100;
    public float maxhealth = 100;
    private double[] healthNew = {25f,25f,25f,30f};
    private double[] maxhealthNew;
    public Image HealthBar;
    private Text text;
    private GameObject player;
    private float[] healthPoints = new float[4];
    private int healthIndex = 0;
    private int phase = 0;
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
    private void updateHealth()
    {
        Vector2 temp = HealthBar.rectTransform.sizeDelta;
        text.text = (healthNew[healthIndex]).ToString() + "/" + (maxhealthNew[healthIndex]).ToString();
        temp.x = 505f * (float)((healthNew[healthIndex]) / (maxhealthNew[healthIndex])); // changing here to show health
        HealthBar.rectTransform.sizeDelta = temp;
    }
    public void losehealth(double amnt)
    {
        healthNew[healthIndex] -= amnt;
        if (healthNew[healthNew.Length-1] <= 0) {
            GameObject.FindGameObjectWithTag("EventSystem").GetComponent<gameOver>().startGameOver(true);     
        }else if (healthNew[healthIndex] <= 0) {
            GameObject Piece = Instantiate(GameObject.FindGameObjectWithTag("PieceOne"), transform.position, transform.rotation) as GameObject;
            Piece.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.5f) * 5;
            healthIndex++;
            //Debug.Log(healthIndex);
            phase++;
        }            
        updateHealth();
    }
    
}
