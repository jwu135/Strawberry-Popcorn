using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStick : MonoBehaviour
{
    public bool pierced;
    public Vector2 aPosition1;
    public Vector2 hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        pierced = false;
    }

    // Update is called once per frame
    void Update()
    {
       // aPosition1 = new Vector2(hitPoint.x, hitPoint.y);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "chargeAttack2")
        {
            pierced = true;

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // If a missile hits this object
        if (other.transform.tag == "chargeAttack2")
        {

            // Spawn an explosion at each point of contact
            foreach (ContactPoint2D missileHit in other.contacts)
            {
                Vector2 hitPoint = missileHit.point;
                aPosition1 = new Vector2(hitPoint.x, hitPoint.y);
                Debug.Log(aPosition1);
                Debug.Log("gnjkirrejkfjkerjkerfker");
            }
        }
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "chargeAttack2")
        {
            pierced = false;

        }
    }
}
