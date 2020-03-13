using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerShootSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerShootSound = Resources.Load<AudioClip>("Sounds/shoot1");

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
        }
    }
}
