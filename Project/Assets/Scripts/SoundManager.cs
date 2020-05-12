using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerShotSound1;
    public static AudioClip playerShotSound2;
    public static AudioClip playerJump1;
    public static AudioClip playerJump2;
    public static AudioClip eyeballShot1;
    public static AudioClip eyeballShot2;
    public static AudioClip playerHit;
    public static AudioClip enemyHit1;
    public static AudioClip enemyHit2;
    public static AudioClip playerDodge;
    public static AudioClip playerCharging;

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
        // new sounds from 5-12
        playerShotSound1 = Resources.Load<AudioClip>("Sounds/Chiptune/Shot1");
        playerShotSound2 = Resources.Load<AudioClip>("Sounds/Chiptune/Shot2");
        playerJump1 = Resources.Load<AudioClip>("Sounds/Chiptune/Jump1");
        playerJump2 = Resources.Load<AudioClip>("Sounds/Chiptune/Jump2");
        eyeballShot1 = Resources.Load<AudioClip>("Sounds/Chiptune/EyeBallShot");
        eyeballShot2 = Resources.Load<AudioClip>("Sounds/Chiptune/EyeBallShot2");
        playerHit = Resources.Load<AudioClip>("Sounds/Chiptune/GettingHit");
        enemyHit1 = Resources.Load<AudioClip>("Sounds/Chiptune/Hit1");
        enemyHit2 = Resources.Load<AudioClip>("Sounds/Chiptune/Hit2");
        playerDodge = Resources.Load<AudioClip>("Sounds/Chiptune/Dodge");
        playerCharging = Resources.Load<AudioClip>("Sounds/Chiptune/ChargingUp");

        // leaving these just in case
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
            case "playerShotSound1":
                audioSrc.PlayOneShot(playerShotSound1);
                break;
            case "playerShotSound2":
                audioSrc.PlayOneShot(playerShotSound2);
                break;
            case "playerJump1":
                audioSrc.PlayOneShot(playerJump1);
                break;
            case "playerJump2":
                audioSrc.PlayOneShot(playerJump2);
                break;
            case "eyeballShot1":
                audioSrc.PlayOneShot(eyeballShot1);
                break;
            case "eyeballShot2":
                audioSrc.PlayOneShot(eyeballShot2);
                break;
            case "playerHit":
                audioSrc.PlayOneShot(playerHit);
                break;
            case "enemyHit1":
                audioSrc.PlayOneShot(enemyHit1);
                break;
            case "enemyHit2":
                audioSrc.PlayOneShot(enemyHit2);
                break;
            case "playerDodge":
                audioSrc.PlayOneShot(playerDodge);
                break;
            case "playerCharging":
                audioSrc.PlayOneShot(playerCharging);
                break;

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
