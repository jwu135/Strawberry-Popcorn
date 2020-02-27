using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    private bool spawned = false;
    private float disappearTime = 1;
    private float hits = 0.3667f; // how long til the spike goes up, according to the animation frames
    public void disappear(){
        spawned = true;
        disappearTime = Time.time + 1f;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned) {
            if (disappearTime - 1f + hits < Time.time) {
                GetComponent<BoxCollider2D>().enabled = true;
            }
            if (disappearTime < Time.time) {
                Destroy(gameObject);
            }
        }
    }
}
