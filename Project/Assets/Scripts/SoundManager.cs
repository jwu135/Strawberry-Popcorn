using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerShootSound1;
    public static AudioClip playerShootSound2;
    public static AudioClip playerShootSound3;
    public static AudioClip playerJumpSound;
    public static AudioClip bossLaserSound;
    public static AudioClip bossAOESound;
    public static AudioClip hitSound1;
    public static AudioClip hitSound2;
    public static AudioClip chargingSound1;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerShootSound1 = Resources.Load<AudioClip>("Sounds/shoot1");
        playerShootSound2 = Resources.Load<AudioClip>("Sounds/shoot2");
        playerShootSound3 = Resources.Load<AudioClip>("Sounds/shoot3");
        playerJumpSound = Resources.Load<AudioClip>("Sounds/jump1");
        bossLaserSound = Resources.Load<AudioClip>("Sounds/bossLaser");
        bossAOESound = Resources.Load<AudioClip>("Sounds/bossAOE");
        chargingSound1 = Resources.Load<AudioClip>("Sounds/chargingSound1");
        hitSound2 = Resources.Load<AudioClip>("Sounds/hitSmall");
        hitSound1 = Resources.Load<AudioClip>("Sounds/hit");
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
            case "playerShoot1":
                audioSrc.PlayOneShot(playerShootSound1);
                break;
            case "playerShoot2":
                audioSrc.PlayOneShot(playerShootSound2);
                break;
            case "playerShoot3":
                audioSrc.PlayOneShot(playerShootSound3);
                break;
            case "playerJump":
                audioSrc.PlayOneShot(playerJumpSound);
                break;
            case "bossLaser":
                audioSrc.PlayOneShot(bossLaserSound);
                break;
            case "bossAOE":
                audioSrc.PlayOneShot(bossAOESound);
                break;
            case "hitSound1":
                audioSrc.PlayOneShot(hitSound1,2);
                break;
            case "hitSound2":
                audioSrc.PlayOneShot(hitSound2);
                break;
            case "chargingSound1":
                audioSrc.PlayOneShot(chargingSound1);
                break;

        }
    }
}
