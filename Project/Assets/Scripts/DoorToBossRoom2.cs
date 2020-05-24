using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToBossRoom2 : MonoBehaviour
{
    BoxCollider2D door;
    BoxCollider2D Player;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Left hitbox");
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
        door = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        if (!door.IsTouching(Player)) {
            GetComponent<Animator>().SetTrigger("Close");
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
