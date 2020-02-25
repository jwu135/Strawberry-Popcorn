﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private double timeBtwAttack;
    public double startTimeBtwAttack;
    private int weaponCycle = 1;
    private bool hit = false;
    public GameObject bullet1Prefab;
    public GameObject bullet2Prefab;
    public GameObject harpoonPrefab;

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
    public harpoon harpoonthrow;

    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(harpoonthrow.thrown);

        if (Input.GetButton("cycleUp")) //cycle - 1
        {
            weaponCycle = weaponCycle - 1;
            if (weaponCycle == 0)
            {
                weaponCycle = 3;
            }
            Debug.Log(weaponCycle);
        }
        if (Input.GetButton("cycleDown")) //cycle + 1
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
            if (Input.GetButton("Fire1") && weaponCycle == 2 && (!Input.GetButton("Fire2") && Input.GetAxis("Roll") <= 0)) //Axis Roll being greater than zero is Fire2 on controller
            {
                Collider2D[] enemiesToDamage1 = Physics2D.OverlapCircleAll(attackPos1.position, attackRange, enemyGroup);
                Collider2D[] enemiesToDamage2 = Physics2D.OverlapCircleAll(attackPos2.position, attackRange, enemyGroup);
                Collider2D[] enemiesToDamage3 = Physics2D.OverlapCircleAll(attackPos3.position, attackRange, enemyGroup);
                Collider2D[] enemiesToDamage4 = Physics2D.OverlapCircleAll(attackPos4.position, attackRange, enemyGroup);
                for (int i = 0; i < enemiesToDamage1.Length; i++)
                {
                    if (hit == false)
                    {
                        enemiesToDamage1[i].GetComponent<Enemy>().TakeDamage(damage);
                        hit = true;
                    }
                }
                for (int i = 0; i < enemiesToDamage2.Length; i++)
                {
                    if (hit == false)
                    {
                        enemiesToDamage2[i].GetComponent<Enemy>().TakeDamage(damage);
                        hit = true;
                    }
                }
                for (int i = 0; i < enemiesToDamage3.Length; i++)
                {
                    if (hit == false)
                    {
                        enemiesToDamage3[i].GetComponent<Enemy>().TakeDamage(damage);
                        hit = true;
                    }
                }
                for (int i = 0; i < enemiesToDamage4.Length; i++)
                {
                    if (hit == false)
                    {
                        enemiesToDamage4[i].GetComponent<Enemy>().TakeDamage(damage);
                        hit = true;
                    }
                }
                hit = false;
                timeBtwAttack = startTimeBtwAttack - 0.25;
            }

            if (Input.GetButton("Fire1") && weaponCycle == 1 && (!Input.GetButton("Fire2") && Input.GetAxis("Roll") <= 0))//Axis Roll being greater than zero is Fire2 on controller
            {
                Shoot1();
                timeBtwAttack = startTimeBtwAttack;
            }
            if ((Input.GetButton("Fire2") || Input.GetAxis("Roll") > 0) && weaponCycle == 1 && !Input.GetButton("Fire1"))//Axis Roll being greater than zero is Fire2 on controller
            {
                Shoot2();
                timeBtwAttack = startTimeBtwAttack + 0.7;
            }
            if (Input.GetButton("Fire1") && weaponCycle == 3 && (!Input.GetButton("Fire2") && Input.GetAxis("Roll") <= 0) && harpoonthrow.thrown != true)//Axis Roll being greater than zero is Fire2 on controller
            {
                Flame.size();
                Enemy.TakeDamage4(damage4);
                timeBtwAttack = startTimeBtwAttack + 0.2;
            }
            if ((Input.GetButton("Fire2") || Input.GetAxis("Roll") > 0) && weaponCycle == 3 && !Input.GetButton("Fire1") && harpoonthrow.thrown != true)//Axis Roll being greater than zero is Fire2 on controller
            {
                Shoot3();
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
}
