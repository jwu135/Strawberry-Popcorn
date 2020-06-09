using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update() // would child it to player, but it gets treated as an extension of player's hitbox
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
}
