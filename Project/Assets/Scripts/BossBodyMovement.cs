using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FlipFirst",1f,4.0f);
    }

    void FlipFirst()
    {
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        GetComponent<Animator>().SetTrigger("Move");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Flipped");
        Vector3 temp = transform.position;
        temp.x *= -1;
        transform.position = temp;
        yield return 0; 
    }

}
