using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    private bool spawned = false;
    private float disappearTime = 1;
    private float timeToDisappearAfter = 0.99f;
    private float hits = 0.5f; // how long til the spike goes up, according to the animation frames
    public void disappear(){
        spawned = true;
        disappearTime = Time.time + timeToDisappearAfter;
        if(GetComponent<BoxCollider2D>())
        GetComponent<BoxCollider2D>().enabled = false;
        else if (GetComponent<CircleCollider2D>()) {
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    public void setTimer(float time)
    {
        timeToDisappearAfter = time;
    }
    public void setHits(float hit)
    {
        hits = hit;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned) {
            if (disappearTime - timeToDisappearAfter -0.01f + hits < Time.time) {
                if (GetComponent<BoxCollider2D>())
                    GetComponent<BoxCollider2D>().enabled = true;
                else if (GetComponent<CircleCollider2D>())
                    GetComponent<CircleCollider2D>().enabled = true;
            }
            if (disappearTime < Time.time) {
                Destroy(gameObject);
            }
        }
    }
}
