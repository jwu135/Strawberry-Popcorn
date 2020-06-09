using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public double timeBtwAttack;
    public double timeBtwChargeAttack1;
    public double timeBtwChargeAttack2;
    public double timeBtwChargeAttack3;
    public double weaponAmount;
    public double[] weaponChoice;
    public double weaponChoice1;
    public double weaponChoice2;
    public double weaponChoice3;
    public float weaponCycle = 1;
    private bool hit = false;

    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    public GameObject harpoonPrefab;
    public GameObject cloudPrefab;
    public GameObject shield1Prefab;
    public GameObject shield2Prefab;
    public GameObject Laser;

    public BoxCollider2D LaserCollider;
    public BoxCollider2D FlameCollider;
    public BoxCollider2D HarpoonCollider;

    public SpriteRenderer LaserRenderer;
    public SpriteRenderer FlameRenderer;
    public SpriteRenderer HarpoonRenderer;
    public SpriteRenderer ArmRenderer;

    public MeshRenderer BodyRenderer;
    public MeshRenderer ShoeRenderer;

    public Transform attackPos1;
    public Transform attackPos2;
    public Transform attackPos3;
    public Transform attackPos4;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform harpoon;
    public Transform shieldPosition1;
    public Transform shieldPosition2;

    public LayerMask enemyGroup;

    public PlayableDirector cutscene1;

    public Enemy Enemy;
    public Flame Flame;
    //public Spread Spread;
    public harpoon harpoonthrow;
    public HealthManager HM;
    public Flamethrower Flamethrower;
    public BossStick BossStick;
    public Stick Stick;
    public FakeCannon FakeCannon;
    public HealthManager HealthManager;
    public PlatformMovementPhys PlatformMovementPhys;
    public SceneChanger SceneChanger;
    FlightMovementPhys flightMovementPhys;
    PlayerController playerController;
    public UpgradeValues UpgradeValues;

    public double timeBtwWeaponChange;
    public double delayBtwAttack1;
    public double delayBtwAttack2;
    public double delayBtwAttack3;
    public double delayBtwChargeAttack1;
    public double delayBtwChargeAttack2;
    public double delayBtwChargeAttack3;
    public double delayAttackCD;
    public double delayChargeAttackCD;
    public double weaponSwapCD;
    public bool fuse = false;
    public bool LASER = false;
    public bool fire2Switch = false;
    public bool stop1 = false;
    public bool launch = false;
    public bool launch2 = false;
    public bool launchVisible = false;
    public bool longPress = false;
    public float evolution;
    public float attackRange;
    public float bulletSpray;
    public float bulletSprayCap;
    public double damage;
    public double damage2;
    public double damage3;
    public double damage4;
    public double specialChargeAttack1Timer;
    public double normalAttack1Buffer;
    public double dodgeCountdown;

    private bool fire2State;
    

    public Vector2 aPosition1 = new Vector2(5, 5);
    public float hookspeed;


    void Start()
    {
        flightMovementPhys = GetComponent<FlightMovementPhys>();
        playerController = GetComponent<PlayerController>();

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        string sceneName = currentScene.name;
        if (sceneName == "MainGameplay")
        {
            dodgeCountdown = UpgradeValues.dodgeNeeded;
            Debug.Log("countdown");
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire2") == true || Input.GetAxis("Fire2") > 0.5f)
        {
            fire2State = true;
        }
        else
        {
            fire2State = false;
        }
           
        //Debug.Log(harpoonthrow.thrown);
        // fuse = false;
        // LASER = false;

        //combo normal a1+
        if (Input.GetButton("Fire1") && normalAttack1Buffer < (0.21) && weaponCycle == 1 && ((flightMovementPhys.rollingFrame == 0 && playerController.getMode() == 0) || (PlatformMovementPhys.rollingFrame == 0 && playerController.getMode() == 1)))
        {
            
            //LASER = true;
            normalAttack1Buffer += Time.deltaTime;
        }

        if ((!Input.GetButton("Fire1") && normalAttack1Buffer > 0) || ((flightMovementPhys.rollingFrame > 0 && playerController.getMode() == 0) || (PlatformMovementPhys.rollingFrame > 0 && playerController.getMode() == 1)))
        {
            //LASER = true;
            normalAttack1Buffer = 0;
        }

        if (Input.GetButton("Fire1") && weaponCycle == 1)
        {
            if(normalAttack1Buffer < 0.2)
            {
                longPress = false;
            }

            if (normalAttack1Buffer >= 0.2)
            {
                longPress = true;
            }

        }





        if (Input.GetButton("Fire2"))
        {
            fire2Switch = true;

        }
        if (!Input.GetButton("Fire2"))
        {
            fire2Switch = false;
        }

        if (Input.GetButton("Fire2") && weaponCycle == 1 && fire2Switch && timeBtwChargeAttack1 > 0)
        {
            specialChargeAttack1Timer -= Time.deltaTime;
        }
        if (!Input.GetButton("Fire2") && weaponCycle == 1 && !fire2Switch && timeBtwChargeAttack1 > 0)
        {
            specialChargeAttack1Timer = 0;
        }

        if (weaponCycle == 1 && fire2Switch && specialChargeAttack1Timer > 0)
        {
            //stop1 = true;
        }
        else
        {
            stop1 = false;
        }


        if (!Input.GetKey(KeyCode.Space) && !Input.GetButton("special"))
        {
            LASER = false;
            LaserCollider.enabled = false;
            LaserRenderer.enabled = false;
        }
        if (timeBtwWeaponChange <= 0)
        {
            if (Input.GetButtonDown("cycleUp") == true)
            {
                timeBtwWeaponChange += weaponSwapCD;

                weaponCycle = weaponCycle - 1;
                if (weaponCycle == 0)
                {
                    weaponCycle = evolution;
                }
            }
            if (Input.GetButtonDown("cycleDown") == true)
            {
                timeBtwWeaponChange += weaponSwapCD;
                if (weaponCycle + 1 > evolution)
                {
                    weaponCycle = 1;
                }
                else
                {
                    weaponCycle = weaponCycle + 1;
                }
            }
        }
        else
        {
                timeBtwWeaponChange -= Time.deltaTime;
        }
        if (weaponCycle == 1)
        {
            ArmRenderer.enabled = true;
            FlameCollider.enabled = false;
            FlameRenderer.enabled = false;
            HarpoonCollider.enabled = false;
            HarpoonRenderer.enabled = false;
        }
        if (weaponCycle == 2)
        {
            ArmRenderer.enabled = true;
            FlameCollider.enabled = false;
            FlameRenderer.enabled = false;
            HarpoonCollider.enabled = true;
            HarpoonRenderer.enabled = true;
        }
        if (weaponCycle == 3)
        {
            ArmRenderer.enabled = true;
            //FlameCollider.enabled = true;
            //FlameRenderer.enabled = true;
            HarpoonCollider.enabled = false;
            HarpoonRenderer.enabled = false;
        }
        if (timeBtwAttack <= 0)
        {            
            //strawberry shooter
            if (Input.GetButtonUp("Fire1") && weaponCycle == 1 && !fire2State && !longPress && PlatformMovementPhys.rollingFrame == 0)
            {
                Shoot1();
                timeBtwAttack = delayBtwAttack1 - delayAttackCD - UpgradeValues.bonusAttackSpd;
            }

            //Tentacle or pineapple stab
            if (Input.GetButton("Fire1") && weaponCycle == 2 && !fire2State && harpoonthrow.thrown != true && evolution > 1)
            {
                Flame.size();
                
                Enemy.TakeDamage4(damage4);
                timeBtwAttack = delayBtwAttack2 - delayAttackCD;
            }

            //jello avalanche
            if (Input.GetButtonDown("Fire1") && weaponCycle == 3 && !fire2State && evolution > 2)
            {
                // fuse = true;
                Flamethrower.size();
                //Debug.Log(fuse);
                //LASER = false;

                timeBtwAttack = delayBtwAttack3 - delayAttackCD;
            }

            //nomnomnomnomnom
            /*if (Input.GetButtonDown("eat") && evolution < 3)
            {
                
                timeBtwAttack += weaponSwapCD;
                evolution += 1;
                weaponCycle = evolution;
                cutscene1.Play();
            }*/

            //mushroom poison
           // if (Input.GetButtonDown("special") && HM.mana >= 20 && HM.mana < 50)
          //  {
           //     HM.mana -= 20;
          //      Shoot4();
          //      timeBtwAttack = 1;
         //   }
            //LASER
            if (dodgeCountdown < 1 && !LASER)
            {
                LASER = true;



                //timeBtwAttack = 1;           
            }
            if (LASER)
            {
                LaserCollider.enabled = true;
                LaserRenderer.enabled = true;
                Debug.Log("sshield up");
                StartCoroutine(LaserTimer());
            }


            //SPMAX
            //  if (Input.GetButtonDown("special") && HM.mana >= 100)
            //    {
            //       HM.mana -= 100;
            //       Enemy.damage1 = Enemy.damage1 * 1.5;
            //        Enemy.damage2 = Enemy.damage2 * 1.5;
            //        Enemy.damage3 = Enemy.damage3 * 1.5;
            //         Enemy.damage4 = Enemy.damage4 * 1.5;
            //          Enemy.damage5 = Enemy.damage5 * 1.5;
            //          delayAttackCD = 0.1;
            //         delayChargeAttackCD = 1;
            timeBtwAttack = 1;
//

          //  }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }



        if (timeBtwChargeAttack1 <= 0)
        {
            //strawberry cannon
            //if (fire2State && weaponCycle == 1 && !Input.GetButton("Fire1") && !FakeCannon.maxCharge && !FakeCannon.charging)
            if (Input.GetButton("Fire1") && weaponCycle == 1 && !fire2State && !FakeCannon.maxCharge && !FakeCannon.charging )
            {
                launch = true;
                launchVisible = true;
                timeBtwChargeAttack1 = delayBtwChargeAttack1 - delayChargeAttackCD;
                specialChargeAttack1Timer = 1.75;
            }
        }
        else 
        {
            if (!Input.GetButtonUp("Fire1") || specialChargeAttack1Timer <= 0)
            {
                //launch = true;
                //specialChargeAttack1Timer = 0;

            }


            if (!Input.GetButtonDown("Fire1") || specialChargeAttack1Timer <= 0)
            {
                timeBtwChargeAttack1 -= Time.deltaTime;
            }
        }

        if (timeBtwChargeAttack2 <= 0)
        {
            //tentacle or pineaple harpoon
            if (fire2State && weaponCycle == 2 && !Input.GetButton("Fire1") && BossStick.pierced != true && evolution > 1)
            {
                launch2 = true;
                Shoot3();
                timeBtwChargeAttack2 = delayBtwChargeAttack2 - delayChargeAttackCD;

            }

        }
        else
        {
            timeBtwChargeAttack2 -= Time.deltaTime;
        }

        //tentacle or pineaple harpoon
        if (fire2State && weaponCycle == 2 && !Input.GetButton("Fire1") && BossStick.pierced == true && evolution > 1)
        {
            Debug.Log("fsdehkjfebwjfeswjufbswejukibeju");
            BossStick.pierced = false;
        }

        if (timeBtwChargeAttack3 <= 0)
        {
            //jello wiggle shield
            if (fire2State && weaponCycle == 3 && !Input.GetButton("Fire1") && evolution > 2)
            {
                Shoot5();
                timeBtwChargeAttack3 = delayBtwChargeAttack3 - delayChargeAttackCD;
            }
        }
        else
        {
            timeBtwChargeAttack3 -= Time.deltaTime;
        }
    }





    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos1.position, attackRange);
        Gizmos.DrawWireSphere(attackPos2.position, attackRange);
        Gizmos.DrawWireSphere(attackPos3.position, attackRange);
        Gizmos.DrawWireSphere(attackPos4.position, attackRange);
    }

    void Shoot1()
    {
        bulletSpray = Random.Range(-bulletSprayCap, bulletSprayCap);
        Instantiate(bullet1Prefab, firePoint.position, (firePoint.rotation *= Quaternion.Euler(0, 0f, bulletSpray)));
        //Enemy.TakeDamage2(damage2);


        if(bulletSprayCap > 0)
        {
            firePoint.rotation *= Quaternion.Euler(0, 0f, -bulletSpray);
        }
        //maybe make subtle then biggger if longer use
        if (bulletSprayCap < 0)
        {
            firePoint.rotation *= Quaternion.Euler(0, 0f, bulletSpray);
        }




        GetComponent<Movement>().setTime();
        if(GameObject.FindGameObjectWithTag("ButtonClickIcon")!=null)
            GameObject.FindGameObjectWithTag("ButtonClickIcon").GetComponent<ShootIconScript>().timesShot++;
        SoundManager.PlaySound("playerShotSound1");
        transform.GetComponent<PlayerMuzzleFlash>().spawnFlash(new Vector3(1f,1f,1f));
    }

    public void Shoot2()
    {
        GameObject spawnedObject = (GameObject)Instantiate(bullet2Prefab, firePoint.position, firePoint.rotation);

        SoundManager.PlaySound("playerShotSound2");

        if (GameObject.FindGameObjectWithTag("ButtonClickIcon") != null)
            GameObject.FindGameObjectWithTag("ButtonClickIcon").GetComponent<ShootIconScript>().timesShot++;

        spawnedObject.transform.localScale = new Vector2( (float)(FakeCannon.copyscalex/ (float)3.1) , (float)(FakeCannon.copyscaley/ (float)3.1) );

        transform.GetComponent<PlayerMuzzleFlash>().spawnFlash(spawnedObject.transform.localScale);

        //Enemy.TakeDamage3(damage3);

    }
    void Shoot3()
    {
      //  if (!launch2)
      //  {
            GameObject spawnedObject2 = (GameObject)Instantiate(harpoonPrefab, firePoint2.position, firePoint2.rotation);
            Debug.Log(spawnedObject2.transform.position.x);
            launch2 = true;
      //  }
        if (BossStick.pierced == true)
        {
            //BossStick.aPosition1
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(spawnedObject2.transform.position.x, spawnedObject2.transform.position.y), (hookspeed * 3) * Time.deltaTime);
            hookspeed = Mathf.Clamp(Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(spawnedObject2.transform.position.x, spawnedObject2.transform.position.y)), 5, 10);
            Debug.Log(BossStick.aPosition1);
        }
        //Enemy.TakeDamage4(damage4);
    }
    void Shoot4()
    {
        Instantiate(cloudPrefab, firePoint2.position, firePoint2.rotation);
        //Enemy.TakeDamage4(damage4);
    }
    void Shoot5()
    {
        Instantiate(shield1Prefab, shieldPosition1.position, shieldPosition1.rotation);
        Instantiate(shield2Prefab, shieldPosition2.position, shieldPosition2.rotation);
        //Enemy.TakeDamage4(damage4);
    }

    public void Hurt()
    {
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        StartCoroutine(HurtFlicker());

    }
    private IEnumerator HurtFlicker()
    {
        yield return new WaitForSeconds(0.2f);
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        StartCoroutine(HurtFlicker1());

    }
    private IEnumerator HurtFlicker1()
    {
        yield return new WaitForSeconds(0.2f);
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        StartCoroutine(HurtFlicker2());

    }
    private IEnumerator HurtFlicker2()
    {
        yield return new WaitForSeconds(0.2f);
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);

    }

    private IEnumerator HurtFlicker3()
    {
        yield return new WaitForSeconds(0.2f);
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        StartCoroutine(HurtFlicker4());

    }
    private IEnumerator HurtFlicker4()
    {
        yield return new WaitForSeconds(0.2f);
        BodyRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
        ArmRenderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1f);

    }

    private IEnumerator LaserTimer()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds((float)UpgradeValues.shieldDuration);
        //HM.mana -= 50;
        LaserCollider.enabled = false;
        LaserRenderer.enabled = false;

        Debug.Log("sshield down");
        dodgeCountdown = UpgradeValues.dodgeNeeded;
        LASER = false;
        Debug.Log(LASER);

    }

    // public void SavePlayer()
    // {
    //SaveSystem.SavePlayer(this);
    //     Debug.Log("save");
    // }

    //  public void LoadPlayer()
    //  {
    //      SaveData data = SaveSystem.LoadData();

    //     UpgradeValues.bonusHealth = data.bonusHealth;
    //      UpgradeValues.bonusAttackSpd = data.bonusAttackSpd;
    //      UpgradeValues.bonusManaGain = data.bonusManaGain;
    //     weaponAmount = data.weaponAmount;
    //       Debug.Log("load");
    //   }

    //  public void HealthUp()
    //   {
    //       UpgradeValues.bonusHealth += 1;
    //       Debug.Log("hp");
    //       Debug.Log(UpgradeValues.bonusHealth);
    //   }

}
