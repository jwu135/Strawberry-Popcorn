using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour
{
    private bool spawned = false;
    private float disappearTime = 1;
    public void disappear(){
        spawned = true;
        disappearTime = Time.time + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned) {
            if (disappearTime < Time.time) {
                Destroy(gameObject);
            }
        }
    }
}
