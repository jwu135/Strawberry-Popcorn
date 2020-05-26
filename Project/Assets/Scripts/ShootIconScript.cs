using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootIconScript : MonoBehaviour
{
    public int timesShot = 0;
    // Update is called once per frame
    private void Awake()
    {
        if (GlobalVariable.deathCounter == 1) {
            transform.gameObject.SetActive(true);
        } else {
            transform.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    void lookAround()
    {
        if (GlobalVariable.deathCounter == 1) {
            if (timesShot >= 3) {
                Destroy(gameObject);
            }
        }
    }
}
