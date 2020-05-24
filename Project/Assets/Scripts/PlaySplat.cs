using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySplat : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            SoundManager.PlaySound("mushroomSplat");
            Destroy(gameObject);
        }
    }

}
