using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private double ySize;
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void size()
    {
        transform.localScale = new Vector3(transform.localScale.x, 
            transform.localScale.y * 2, transform.localScale.z);
        StartCoroutine(size2());
    }

    private IEnumerator size2()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y * 2, transform.localScale.z);
        StartCoroutine(size3());
    }

    private IEnumerator size3()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y * 2, transform.localScale.z);
        StartCoroutine(size4());
    }

    private IEnumerator size4()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y / 2, transform.localScale.z);
        StartCoroutine(size5());
    }

    private IEnumerator size5()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y / 2, transform.localScale.z);
        StartCoroutine(size6());

    }

    private IEnumerator size6()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y / 2, transform.localScale.z);

    }
}
