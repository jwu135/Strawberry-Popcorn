using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dodge : MonoBehaviour
{
    public float invicibilityLength;
    public float invicibilityCounter;
    // Start is called before the first frame update
    void Start()
    {
        invicibilityCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            invicibilityCounter = invicibilityLength;
        }
    }
}
