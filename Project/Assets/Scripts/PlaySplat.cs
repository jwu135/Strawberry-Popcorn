using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySplat : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            SoundManager.PlaySound("mushroomSplat");
            collision.gameObject.GetComponent<PlatformMovementPhys>().unableToMove = true;
            collision.gameObject.transform.Find("Mushroom").GetComponent<Animator>().SetTrigger("Squish");
            collision.gameObject.transform.Find("Mushroom").GetComponent<MushroomScript>().standingUp = false;

            Destroy(gameObject);
        }
    }

}
