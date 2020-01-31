using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossShoot : MonoBehaviour
{
    //public GameObject hit;
    public GameObject projectile;
    public GameObject hitObj;

    private int[] spawnPointsX = new int[] { -8, -4, 0, 4, 8 };
    private float[] spawnPointsY = new float[] { -3f, -1.5f, 0, 1.5f, 3f};
    private float nextTime = 0f;
    private float cooldown = 1f;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    { 
        
        if (nextTime < Time.time) {
            if (Random.Range(0f, 1f) > 0.5f)
                Shoot();
            else
                Physical();
            
            nextTime += cooldown;
        }
    }

    // range, height, width, angle, damage, 
    void Physical() {
        int pos = Random.Range(0,3);
        GameObject physicalAttack = null;
        if (pos == 0) {
            // For now, make pop up instantly
            int rand = Random.Range(0, spawnPointsX.Length);
            Vector3 position = new Vector3(spawnPointsX[rand], -13, 0);
            physicalAttack = Instantiate(hitObj, position, hitObj.transform.rotation) as GameObject;
            //physicalAttack.GetComponent<Animation>().Play("SpikeMoveUp");
           
        } else if (pos==1) {
            int rand = Random.Range(0, spawnPointsY.Length);
            Vector3 position = new Vector3(17.5f,spawnPointsY[rand], 0);
            //tempRotation = transform.rotation;
            Quaternion tempRotation = Quaternion.Euler(0, 0, 180);
            physicalAttack = Instantiate(hitObj, position,tempRotation ) as GameObject;
        } else {
            int rand = Random.Range(0, spawnPointsY.Length);
            Vector3 position = new Vector3(-17, spawnPointsY[rand], 0);
            physicalAttack = Instantiate(hitObj, position, transform.rotation) as GameObject;
        }
        physicalAttack.GetComponent<Animator>().SetTrigger("Spike");
        physicalAttack.GetComponent<attackTimer>().disappear();
    }
    

    void Shoot()
    { 
        int max = 1;
        for (int i = 0; i < max; i++) {
            //float offset = (max / 2 - i) * 5;
            Vector3 temp = transform.position;
            GameObject bullet = Instantiate(projectile, temp, transform.rotation ) as GameObject;
            Vector3 direction = (Vector2)(player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * 4;
        }
    }
    void Hit()
    {
        
         // GameObject bullet = Instantiate(hitObj, temp, transform.rotation) as GameObject;
    }
}
