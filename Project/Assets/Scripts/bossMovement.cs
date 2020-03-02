using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float startY;
    private float startX;
    private void Start()
    {
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
    }
    // Update is called once per frame
    void Update()
    {
        // Moves in figure-8 
        Vector3 temp = transform.position;
        temp.x = (Mathf.Sin(Time.time))/2 + transform.parent.transform.position.x+startX;
        temp.y = (Mathf.Sin(Time.time) * Mathf.Cos(Time.time) * 1.5f)/2 + transform.parent.transform.position.y + startY;
        transform.position = temp;
    }
}
