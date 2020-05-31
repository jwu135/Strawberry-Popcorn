using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCloudHandler : MonoBehaviour
{
    public GameObject leftWisp; 
    public GameObject rightWisp;
    float leftWispCD = 5f;
    float rightWispCD = 4f;


    // Update is called once per frame
    void Update()
    {
        leftWispCD -= Time.deltaTime;
        rightWispCD -= Time.deltaTime;
        if (GameObject.FindGameObjectWithTag("Enemy").GetComponent<Boss>().getPhase() < 3) {
            if (leftWispCD <= 0) {
                Vector3 temp = transform.position;
                temp.x = Random.Range(-26.8f, 26.8f);
                GameObject left = Instantiate(leftWisp, temp, transform.rotation, transform);
                leftWispCD = Random.Range(4f, 5f);
            }
            if (rightWispCD <= 0) {
                Vector3 temp = transform.position;
                temp.x = Random.Range(-26.8f, 26.8f);
                GameObject right = Instantiate(rightWisp, transform.position, transform.rotation, transform);
                //right.transform.parent = transform;
                rightWispCD = Random.Range(4f, 5f);
            }
        }
    }
}
