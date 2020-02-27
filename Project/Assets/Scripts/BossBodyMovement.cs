using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBodyMovement : MonoBehaviour
{
    public static bool triggered; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FlipFirst",1f,9.0f);
    }

    void FlipFirst()
    {
        StartCoroutine(Flip());
    }

    public IEnumerator Flip()
    {
        GetComponent<Animator>().SetTrigger("Move");
        yield return new WaitForSeconds(0.5f);
        Vector3 temp = transform.position;
        temp.x *= -1;
        transform.position = temp;
        yield return 0; 
    }

}
