using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    private bool spawned = false;
    private float disappearTime = 1;
    private float timeToDisappearAfter = 0.99f;
    private float hits = 0.5f; // how long til the spike goes up, according to the animation frames

    public Sprite[] orangeSprites;
    public Sprite orangeAoE;
    public Sprite[] purpleSprites;


    public void disappear(){
        spawned = true;
        disappearTime = Time.time + timeToDisappearAfter;
        enable(0);
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
        if (GetComponent<BoxCollider2D>())
            Debug.Log("Yup");
        StartCoroutine("enableDelay");
    }

    IEnumerator enableDelay() {
        yield return new WaitForSeconds(.1f);
        enable(1);
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
        if (GetComponent<BoxCollider2D>())
            Debug.Log("Tried to destroy spike");
        Debug.Log("Destroyed");
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (spawned) {
            /*if (disappearTime - timeToDisappearAfter -0.01f + hits < Time.time) {
                enable(1);
            }
            if (disappearTime < Time.time) {
                destroy();
            }*/
        }
    }
}
