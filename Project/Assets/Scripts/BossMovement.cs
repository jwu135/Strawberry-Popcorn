using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private float startY;
    private float startX;
    public float mod = 2;
    public float timeMod = 2;
    private void Start()
    {
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Boss Move");
        Vector3 temp = transform.position;
        temp.x = (Mathf.Sin(Time.time*timeMod)) / mod + transform.parent.transform.position.x + startX;
        //temp.y = (Mathf.Sin(Time.time) * Mathf.Cos(Time.time) * 1.5f) / mod + transform.parent.transform.position.y + startY;

        /*
        // Moves in figure-8 
        temp.x = (Mathf.Sin(Time.time))/mod + transform.parent.transform.position.x+startX;
        temp.y = (Mathf.Sin(Time.time) * Mathf.Cos(Time.time) * 1.5f)/mod + transform.parent.transform.position.y + startY;
        */
        transform.position = temp;
    }
}
