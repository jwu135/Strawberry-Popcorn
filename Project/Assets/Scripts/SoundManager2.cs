using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour
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

    public static AudioClip bombsAfall;
    public static AudioClip Explosion;
    public static AudioClip gainLevel;
    public static AudioClip pieceFall;


    public static AudioClip crunch;
    public static AudioClip munch;
    public static AudioClip royaltySplat;

    public static AudioClip playerShootSound1;
    public static AudioClip playerShootSound2;
    public static AudioClip playerShootSound3;
    public static AudioClip playerJumpSound;
    public static AudioClip bossLaserSound;
    public static AudioClip bossAOESound;
    public static AudioClip hitSound1;
    public static AudioClip hitSound2;
    public static AudioClip chargingSound1;
   // static AudioSource audioSrc;
    public AudioSource audioSrc2;
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

        bombsAfall = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/bombAfall"); // called in BossBullet.cs
        Explosion = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/Explosion"); // called in BossBullet.cs
        gainLevel = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/gainLevel"); // called in BossPieceUpgrade.cs
        pieceFall = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/pieceFall"); // called in Boss.cs



        crunch = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/crunch"); // called in Boss.cs
        munch = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/munch"); // called in Boss.cs
        royaltySplat = Resources.Load<AudioClip>("Sounds/ChiptuneSoft/royaltySplat"); // called in Boss.cs




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
      // audioSrc = GetComponent<AudioSource>();
        audioSrc2 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public  void Play(string clip)
    {
        switch (clip)
        {
            case "eatingQuestionMark":
                audioSrc2.clip = eatingQuestionMark;
                audioSrc2.Play();
                break;
        }
    }

    public void Stop(string clip)
    {
        switch (clip)
        {
            case "eatingQuestionMark":
                //audioSrc2.clip = eatingQuestionMark;
                audioSrc2.Stop();
                break;
        }
    }
}
