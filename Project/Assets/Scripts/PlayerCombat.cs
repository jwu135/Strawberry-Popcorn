using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private double timeBtwAttack;
    public double startTimeBtwAttack;
    private int weaponCycle = 1;
    private bool hit = false;
    public bool fuse = false;
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    public GameObject harpoonPrefab;
    public GameObject cloudPrefab;

    public Transform attackPos1;
    public Transform attackPos2;
    public Transform attackPos3;
    public Transform attackPos4;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform harpoon;
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
        fuse = false;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            weaponCycle = weaponCycle - 1;
            if (weaponCycle == 0)
            {
                weaponCycle = 3;
            }
            Debug.Log(weaponCycle);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
           weaponCycle = weaponCycle + 1;
            if (weaponCycle == 4)
            {
                weaponCycle = 1;
            }
            Debug.Log(weaponCycle);
        }
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0) && weaponCycle == 2 && !Input.GetMouseButton(1))
            {
                fuse = true;
                Flamethrower.size();
                Debug.Log(fuse);
                
                timeBtwAttack = startTimeBtwAttack;
                Debug.Log(timeBtwAttack);
            }

            if (Input.GetMouseButton(0) && weaponCycle == 1 && !Input.GetMouseButton(1))
            {
                Shoot1();
                timeBtwAttack = startTimeBtwAttack;
            }
            if (Input.GetMouseButton(1) && weaponCycle == 1 && !Input.GetMouseButton(0))
            {
                Shoot2();
                timeBtwAttack = startTimeBtwAttack + 0.7;
            }
            if (Input.GetMouseButton(0) && weaponCycle == 3 && !Input.GetMouseButton(1) && harpoonthrow.thrown != true)
            {
                Flame.size();
                
                Enemy.TakeDamage4(damage4);
                timeBtwAttack = startTimeBtwAttack + 0.2;
            }
            if (Input.GetMouseButton(1) && weaponCycle == 3 && !Input.GetMouseButton(0) && harpoonthrow.thrown != true)
            {
                Shoot3();
                timeBtwAttack = startTimeBtwAttack + 1;
            }
            //poison
            if (Input.GetKeyDown(KeyCode.Space) && HM.mana >= 20 && HM.mana < 50)
            {
                Shoot4();
                timeBtwAttack = startTimeBtwAttack + 1;
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
}
