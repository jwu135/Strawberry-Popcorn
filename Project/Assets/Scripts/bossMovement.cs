using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Moves in figure-8 
        Vector3 temp = transform.position;
        temp.x = (Mathf.Sin(Time.time))/2 + transform.parent.transform.position.x;
        temp.y = (Mathf.Sin(Time.time) * Mathf.Cos(Time.time) * 1.5f)/2 + transform.parent.transform.position.y + 1f;
        transform.position = temp;
    }
}
