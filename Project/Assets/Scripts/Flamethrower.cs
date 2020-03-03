using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    private double ySize;
    public float fuseTimer;
    public bool lit = false;
    public PlayerCombat PCom;
    public float flameLength;
    public float flameDegrade;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            lit = true;
            //Debug.Log(lit);
            if (fuseTimer < flameDegrade)
            {
                fuseTimer += (Time.deltaTime/2 );
               // Debug.Log(fuseTimer);
            }
        }

        if (!Input.GetButton("Fire1"))
        {
            lit = false;
           // Debug.Log(lit);
            if (fuseTimer > 0)
            {
                fuseTimer -= (Time.deltaTime/2 + Time.deltaTime / 4);
             //   Debug.Log(fuseTimer);
            }
            transform.localScale = new Vector3(1,
            2, 1);
        }
    }

    public void size()
    {
        transform.localScale = new Vector3(1,
           2, 1);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y + flameLength - (fuseTimer * 4), transform.localScale.z);
       
        
            InvokeRepeating("size2", 0.1f, 0.1f);
            
                
         //1s delay, repeat every 1s
        //StartCoroutine(size2());
    }

    private void size2()
    {
        //yield return new WaitForSeconds(0.05f);
        if (!lit)
        {
            CancelInvoke();
        }
            Debug.Log("hi");
        if (transform.localScale.y - fuseTimer > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x,
                transform.localScale.y - fuseTimer, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(1,
            2, 1);
        }
        //StartCoroutine(size3());
    }

    private IEnumerator size3()
    {
        yield return new WaitForSeconds(.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y * 1, transform.localScale.z);
        StartCoroutine(size4());
    }

    private IEnumerator size4()
    {
        yield return new WaitForSeconds(10.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y - 20, transform.localScale.z);
        StartCoroutine(size5());
    }

    private IEnumerator size5()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y / 1, transform.localScale.z);
        StartCoroutine(size6());

    }

    private IEnumerator size6()
    {
        yield return new WaitForSeconds(0.05f);
        transform.localScale = new Vector3(transform.localScale.x,
            transform.localScale.y / 1, transform.localScale.z);

    }
}
