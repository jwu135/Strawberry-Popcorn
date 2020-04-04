using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerShootSound;
    public static AudioClip playerJumpSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerShootSound = Resources.Load<AudioClip>("Sounds/shoot1");
        playerJumpSound = Resources.Load<AudioClip>("Sounds/jump1");


        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "playerShoot":
                audioSrc.PlayOneShot(playerShootSound);
                break;
            case "playerJump":
                audioSrc.PlayOneShot(playerJumpSound);
                break;
        }
    }
}
