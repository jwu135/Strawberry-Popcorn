using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private double timeBtwAttack;
    public double startTimeBtwAttack;
    private float weaponCycle = 1;
    private bool hit = false;
    public bool fuse = false;
    public bool LASER = false;
    public float evolution;
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
    public float attackRange;
    public double damage;
    public double damage2;
    public double damage3;
    public double damage4;
    public Enemy Enemy;
    public Flame Flame;
    //public Spread Spread;
    public harpoon harpoonthrow;
    public HealthManager HM;
    public Flamethrower Flamethrower;

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponCycle = weaponCycle - 1;   
            if (weaponCycle == 0)
            {
                weaponCycle = evolution;
            }
            Debug.Log(weaponCycle);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
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
            ArmRenderer.enabled = false;
            FlameCollider.enabled = false;
            FlameRenderer.enabled = false;
            HarpoonCollider.enabled = true;
            HarpoonRenderer.enabled = true;
        }
        if (weaponCycle == 3)
        {
            ArmRenderer.enabled = false;
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
                timeBtwAttack = startTimeBtwAttack;
            }
            //strawberry cannon
            if ((Input.GetButton("Fire2") && weaponCycle == 1 && !Input.GetButton("Fire1")))
            {
                Shoot2();
                timeBtwAttack = startTimeBtwAttack + 0.7;
            }
            //Tentacle or pineapple stab
            if (Input.GetButton("Fire1") && weaponCycle == 2 && !(Input.GetButton("Fire2") && harpoonthrow.thrown != true && evolution > 1))
            {
                Flame.size();
                
                Enemy.TakeDamage4(damage4);
                timeBtwAttack = startTimeBtwAttack + 0.2;
            }
            //tentacle or pineaple harpoon
            if ((Input.GetButton("Fire2") && weaponCycle == 2 && !Input.GetButton("Fire1") && harpoonthrow.thrown != true && evolution > 1))
            {
                Shoot3();
                timeBtwAttack = startTimeBtwAttack + 1;
            }
            //jello avalanche
            if ((Input.GetButton("Fire1") && weaponCycle == 3 && !(Input.GetButton("Fire2") && evolution > 2)))
            {
                // fuse = true;
                Flamethrower.size();
                //Debug.Log(fuse);
                //LASER = false;

                timeBtwAttack = startTimeBtwAttack;
                Debug.Log(timeBtwAttack);
            }
            //jello wiggle shield
            if ((Input.GetButton("Fire2") && weaponCycle == 3 && !Input.GetButton("Fire1") && evolution > 2))
            {
                Shoot5();
                timeBtwAttack = startTimeBtwAttack + 1;
            }
            //nomnomnomnomnom
            if (Input.GetKeyDown(KeyCode.R) && evolution < 3)
            {
                evolution += 1;
                weaponCycle = evolution;
            }
            //mushroom poison
            if (Input.GetKeyDown(KeyCode.Space) && HM.mana >= 20 && HM.mana < 50)
            {
                Shoot4();
                timeBtwAttack = startTimeBtwAttack + 1;
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
                    LaserCollider.enabled = false;
                    LaserRenderer.enabled = false;
                }
                timeBtwAttack = startTimeBtwAttack + 1;
                Debug.Log(timeBtwAttack);
                
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
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
}
