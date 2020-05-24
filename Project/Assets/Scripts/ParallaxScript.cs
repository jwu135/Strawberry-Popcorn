using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxScript : MonoBehaviour
{
    public GameObject player;

    // Only used for locking camera now
    void Update()
    {
  
        // stuff for locking camera
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos = player.transform.position; // find as reference
        pos.y += 3.11f;
        pos.z = -100f;
        pos.x = Mathf.Clamp(pos.x, 3.59f, 37f);
        transform.position = pos;
        //Debug.Log(pos.x);
        
    }
}
