using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToBossRoom : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Left hitbox");
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Entered hitbox");
            GetComponent<Animator>().SetTrigger("Open");
        }
    }
}
