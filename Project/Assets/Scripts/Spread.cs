using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : MonoBehaviour
{

    public GameObject sporeCloudPrefab;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(sporeCloudPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
          
}
