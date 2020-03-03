using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private double timeBtwAttack;
    private double timeBtwChargeAttack1;
    private double timeBtwChargeAttack2;
    private double timeBtwChargeAttack3;
    private float weaponCycle = 1;
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

    public Enemy Enemy;
    public Flame Flame;
    //public Spread Spread;
    public harpoon harpoonthrow;
    public HealthManager HM;
    public Flamethrower Flamethrower;

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
    public float evolution;
    public float attackRange;
    public double damage;
    public double damage2;
    public double damage3;
    public double damage4;



    // Update is called once per frame
    void Update()
    {
        //Debug.Log(harpoonthrow.thrown);
        // fuse = false;
        // LASER = false;
       // Debug.Log(LASER);
        if (Input.GetButton("Fire1"))
        {
            //LASER = true;
          
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            LASER = false;
            LaserCollider.enabled = false;
            LaserRenderer.enabled = false;
        }
        if (timeBtwWeaponChange <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                timeBtwWeaponChange += weaponSwapCD;

                weaponCycle = weaponCycle - 1;
                if (weaponCycle == 0)
                {
                    weaponCycle = evolution;
                }
                Debug.Log(weaponCycle);
            }
            if (Input.GetKeyDown(KeyCode.E))
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
                Debug.Log(weaponCycle);
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
            FlameCollider.enabled = true;
            FlameRenderer.enabled = true;
            HarpoonCollider.enabled = false;
            HarpoonRenderer.enabled = false;
        }
        if (timeBtwAttack <= 0)
        {            
            //strawberry shooter
            if (Input.GetButton("Fire1") && weaponCycle == 1 && !(Input.GetButton("Fire2")))
            {
                Shoot1();
                timeBtwAttack = delayBtwAttack1 - delayAttackCD;
            }

            //Tentacle or pineapple stab
            if (Input.GetButton("Fire1") && weaponCycle == 2 && !Input.GetButton("Fire2") && harpoonthrow.thrown != true && evolution > 1)
            {
                Flame.size();
                
                Enemy.TakeDamage4(damage4);
                timeBtwAttack = delayBtwAttack2 - delayAttackCD;
            }

            //jello avalanche
            if (Input.GetButtonDown("Fire1") && weaponCycle == 3 && !Input.GetButton("Fire2") && evolution > 2)
            {
                // fuse = true;
                Flamethrower.size();
                //Debug.Log(fuse);
                //LASER = false;

                timeBtwAttack = delayBtwAttack3 - delayAttackCD;
                Debug.Log(timeBtwAttack);
            }

            //nomnomnomnomnom
            if (Input.GetKeyDown(KeyCode.R) && evolution < 3)
            {
                timeBtwAttack += weaponSwapCD;
                evolution += 1;
                weaponCycle = evolution;
            }
            //mushroom poison
            if (Input.GetKeyDown(KeyCode.Space) && HM.mana >= 20 && HM.mana < 50)
            {
                HM.mana -= 20;
                Shoot4();
                timeBtwAttack = 1;
            }
            //LASER
            if (Input.GetKeyDown(KeyCode.Space) && HM.mana >= 50 && HM.mana < 100)
            {
                LASER = true;
                if (LASER)
                {
                    LaserCollider.enabled = true;
                    LaserRenderer.enabled = true;
                }
                else if (!LASER)
                {
                    HM.mana -= 50;
                    LaserCollider.enabled = false;
                    LaserRenderer.enabled = false;
                }
                timeBtwAttack = 1;
                Debug.Log(timeBtwAttack);
                
            }
            //SPMAX
            if (Input.GetKeyDown(KeyCode.Space) && HM.mana == 100)
            {
                HM.mana -= 100;
                Enemy.damage1 = Enemy.damage1 * 1.5;
                Enemy.damage2 = Enemy.damage2 * 1.5;
                Enemy.damage3 = Enemy.damage3 * 1.5;
                Enemy.damage4 = Enemy.damage4 * 1.5;
                Enemy.damage5 = Enemy.damage5 * 1.5;
                delayAttackCD = 0.1;
                delayChargeAttackCD = 1;
                timeBtwAttack = 1;
                Debug.Log(timeBtwAttack);

            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (timeBtwChargeAttack1 <= 0)
        {
            //strawberry cannon
            if (Input.GetButton("Fire2") && weaponCycle == 1 && !Input.GetButton("Fire1"))
            {
                Shoot2();
                timeBtwChargeAttack1 = delayBtwChargeAttack1 - delayChargeAttackCD;
            }
        }
        else
        {
            timeBtwChargeAttack1 -= Time.deltaTime;
        }

        if (timeBtwChargeAttack2 <= 0)
        {
            //tentacle or pineaple harpoon
            if (Input.GetButton("Fire2") && weaponCycle == 2 && !Input.GetButton("Fire1") && harpoonthrow.thrown != true && evolution > 1)
            {
                Shoot3();
                timeBtwChargeAttack2 = delayBtwChargeAttack2 - delayChargeAttackCD;
            }
        }
        else
        {
            timeBtwChargeAttack2 -= Time.deltaTime;
        }

        if (timeBtwChargeAttack3 <= 0)
        {
            //jello wiggle shield
            if (Input.GetButton("Fire2") && weaponCycle == 3 && !Input.GetButton("Fire1") && evolution > 2)
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
        Instantiate(bullet1Prefab, firePoint.position, firePoint.rotation);
        //Enemy.TakeDamage2(damage2);

    }

    void Shoot2()
    {
        Instantiate(bullet2Prefab, firePoint.position, firePoint.rotation);
        //Enemy.TakeDamage3(damage3);
    }
    void Shoot3()
    {
        Instantiate(harpoonPrefab, firePoint2.position, firePoint2.rotation);
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

}
