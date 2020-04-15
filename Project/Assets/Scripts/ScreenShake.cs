using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Update is called once per frame
    float doShake = 0;
    public float shakeMultiplier = 0.7f; //multiplier to how intense the shake should be, after calculations
    public float shakeUpper = 2; //upper bounds of the shake magnitude, determined by damage
    public float shakeLower = 0.5f; //lower bounds of the shake magnitude, determined by damage
    public float decreaseVal = 5; //how long in seconds the shake lasts
    float shakeAmount;

    public void shakeCamera( int magnitude )
    {
        Debug.Log("CameraShake " + magnitude);
        shakeAmount = magnitude * shakeMultiplier;
        if(shakeAmount < shakeLower)
        {
            shakeAmount = shakeLower;
        }
        if (shakeAmount > shakeUpper)
        {
            shakeAmount = shakeUpper;
        }
        doShake = magnitude;
    }
    public void FixedUpdate()
    {
        Debug.Log("doShake " + doShake);
        if ( doShake > 0 )
        {
            Vector3 shakeVector = new Vector3(Random.value * shakeAmount * doShake, Random.value * shakeAmount * doShake, -10);
            transform.localPosition = shakeVector;
            doShake -= Time.fixedDeltaTime * decreaseVal;
        }
        else if (doShake < 0 )
        {
            doShake = 0;
        }
    }
}
