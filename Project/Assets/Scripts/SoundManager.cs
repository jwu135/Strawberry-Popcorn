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
    
    
    public static AudioClip mushroomSplat;
    public static AudioClip deathScreenClick;
    public static AudioClip menuClick;
    public static AudioClip eatingQuestionMark;
    public static AudioClip tentacleAttack;

    public static AudioClip playerTalk1;
    public static AudioClip playerTalk2;
    public static AudioClip playerTalk3;
    public static AudioClip playerTalk4;
    
    public static AudioClip motherTalk1;
    public static AudioClip motherTalk2;
    public static AudioClip motherTalk3;
    public static AudioClip motherTalk4;

    public static AudioClip playerStep1;
    public static AudioClip playerStep2;
    
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
    void Awake()
    {
        // new sounds from 5-12
        playerShotSound1 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Shot1"); // called in PlayerCombat.cs
        playerShotSound2 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Shot2"); // called in PlayerCombat.cs
        playerJump1 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Jump1"); // called in PlatformMovementPhys.cs
        playerJump2 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Jump2"); // called in PlatformMovementPhys.cs
        eyeballShot1 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/EyeBallShot"); // called in BossShoot.cs
        eyeballShot2 = Resources.Load<AudioClip>("Sounds/Chiptune/EyeBallShot2"); // called in BossShoot.cs
        //playerHit = Resources.Load<AudioClip>("Sounds/Chiptune/GettingHit"); // called in HealthManager.cs
        playerTalk1 = Resources.Load<AudioClip>("Sounds/SPVoice1"); // called in DialogueSystem.cs
        playerTalk2 = Resources.Load<AudioClip>("Sounds/SPVoice2"); // called in DialogueSystem.cs
        playerTalk3 = Resources.Load<AudioClip>("Sounds/SPVoice3"); // called in DialogueSystem.cs
        playerTalk4 = Resources.Load<AudioClip>("Sounds/SPVoice4"); // called in DialogueSystem.cs
        motherTalk1 = Resources.Load<AudioClip>("Sounds/MotherVoice1"); // called in DialogueSystem.cs
        motherTalk2 = Resources.Load<AudioClip>("Sounds/MotherVoice2"); // called in DialogueSystem.cs
        motherTalk3 = Resources.Load<AudioClip>("Sounds/MotherVoice3"); // called in DialogueSystem.cs
        motherTalk4 = Resources.Load<AudioClip>("Sounds/MotherVoice4"); // called in DialogueSystem.cs
        playerHit = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Hurt"); // called in HealthManager.cs
        enemyHit1 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Hit1"); // called in Boss.cs
        enemyHit2 = Resources.Load<AudioClip>("Sounds/Chiptune/Hit2Full"); // called in Boss.cs
        playerDodge = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Dodge"); // called in Movement.cs
        
        playerStep1 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Step1"); // called in Movement.cs
        playerStep2 = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Step2"); // called in Movement.cs
        
        mushroomSplat = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/mushroomSplat"); // called in Movement.cs
        deathScreenClick = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/deathScreenClick"); // called in Movement.cs
        menuClick = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/menuClick"); // called in Movement.cs
        eatingQuestionMark = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/EatingQuestionMark"); // called in Movement.cs
        tentacleAttack = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/TentaceAttack"); // called in Movement.cs
        
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
            case "playerTalk1":
                audioSrc.PlayOneShot(playerTalk1);
                break;
            case "playerTalk2":
                audioSrc.PlayOneShot(playerTalk2);
                break;
            case "playerTalk3":
                audioSrc.PlayOneShot(playerTalk3);
                break; 
            case "playerTalk4":
                audioSrc.PlayOneShot(playerTalk4);
                break;
            case "motherTalk1":
                audioSrc.PlayOneShot(motherTalk1);
                break;
            case "motherTalk2":
                audioSrc.PlayOneShot(motherTalk2);
                break;
            case "motherTalk3":
                audioSrc.PlayOneShot(motherTalk3);
                break; 
            case "motherTalk4":
                audioSrc.PlayOneShot(motherTalk4);
                break;

            case "mushroomSplat":
                audioSrc.PlayOneShot(mushroomSplat);
                break;
            case "deathScreenClick":
                audioSrc.PlayOneShot(deathScreenClick);
                break; 
            case "menuClick":
                audioSrc.PlayOneShot(menuClick);
                break;
            case "eatingQuestionMark":
                audioSrc.PlayOneShot(eatingQuestionMark);
                break;
            case "tentacleAttack":
                audioSrc.PlayOneShot(tentacleAttack,1f);
                Debug.Log("tried to play");
                break;

        }
    }
}
