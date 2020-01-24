using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = transform.position;
        temp.x = Mathf.Sin(Time.time);
        temp.y = Mathf.Sin(Time.time) * Mathf.Cos(Time.time)*1.5f;
        transform.position = temp;
    }
}
