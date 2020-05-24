﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour // literally just ended up becoming the "AllExternalBossAttackHandlerScript"
{
    private bool spawned = false;
    private float disappearTime = 1;
    private float timeToDisappearAfter = 0.99f;
    private float hits = 0.5f; // how long til the spike goes up, according to the animation frames
    private Transform enemy;
    public Sprite[] orangeSprites;
    public Sprite orangeAoE;
    public Sprite[] purpleSprites;

    private bool portal = false;
    private float portalCD = 2f;
    public void disappear() {
        spawned = true;
        disappearTime = Time.time + timeToDisappearAfter;
        enable(0);
    }
    public void playSound()
    {
        //SoundManager.PlaySound("bossAOE");
    }
    public void playTentacleSound()
    {
        SoundManager.PlaySound("tentacleAttack");
    }
    public void setEnemy(Transform e)
    {
        enemy = e;
    }
    private void canRotate()
    {
        enemy.GetComponent<BossMovement>().setRotateable(true);
    }
    public void animationPlay()
    {
        GetComponent<Animator>().SetTrigger("Spike");
        playSound();
    }

    public void setTimer(float time)
    {
        timeToDisappearAfter = time;
    }
    public void setHits(float hit)
    {
        hits = hit;
    }

    public void spikeChange()
    {
        GetComponent<SpriteRenderer>().sprite = orangeSprites[0];
        StartCoroutine("enableDelay");
    }

    IEnumerator enableDelay() {
        yield return new WaitForSeconds(.1f);
        enable(1);
    }

    public void portalSpawn(){
        GameObject player = GameObject.Find("Player");
        GameObject bs = GameObject.FindGameObjectWithTag("Enemy");
        GameObject hitObj = bs.GetComponent<BossShoot>().hitObj;

        GameObject physicalAttack = Instantiate(hitObj, transform.position, transform.rotation) as GameObject;
        physicalAttack.transform.eulerAngles = new Vector3(0, 0,transform.eulerAngles.z- 90);
        // could do some stuff to hit from one side of the portal or the other, depending on which side the player is closer to.
        bs.GetComponent<BossShoot>().addToStack(physicalAttack);
        portal = true;
    }
    private void Update()
    {
        if (portal) {
            portalCD -= Time.deltaTime;
            if (portalCD <= 0) {
                GetComponent<Animator>().SetTrigger("Close");
                portalCD = 5f;
            }
        }
    }
    public void AoEChange()
    {
        GetComponent<SpriteRenderer>().sprite = orangeAoE;
        enable(1);
    }

    public void enable(float togg) // because the animator doesn't like bools :/
    {
        bool toggled = togg > 0 ? true : false;
        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().enabled = toggled;
        else if (GetComponent<CircleCollider2D>())
            GetComponent<CircleCollider2D>().enabled = toggled;
        else if (GetComponent<PolygonCollider2D>())
            GetComponent<PolygonCollider2D>().enabled = toggled;
    }
    public void destroy()
    {
        if (enemy != null)
            canRotate();
        Destroy(gameObject);
    }
}
