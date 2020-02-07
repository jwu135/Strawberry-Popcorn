using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Flip",6.0f,6.0f);
    }

    void Flip()
    {

        Vector3 temp = transform.position;
        temp.x *= -1;
        transform.position = temp;
    }
}
