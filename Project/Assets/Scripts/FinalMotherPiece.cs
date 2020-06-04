using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMotherPiece : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) {
            move();
        }   
    }
    void move()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, 0), Time.deltaTime/2);
    }
}
