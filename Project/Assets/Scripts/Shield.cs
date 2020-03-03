using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldStrength;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "BossBullet" && shieldStrength > 1)
        {
            Destroy(other.gameObject);
            shieldStrength -= 1;
            Debug.Log(shieldStrength);
            
        }
        else if (other.tag == "BossBullet" && shieldStrength <= 1)
        {
            Destroy(other.gameObject);
            shieldStrength -= 1;
            Debug.Log(shieldStrength);
            Destroy(gameObject);
        }

    }
}
