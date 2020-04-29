using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMuzzleFlash : MonoBehaviour
{
    public GameObject muzzleflash;
    // Start is called before the first frame update
    public void spawnFlash(Vector3 scale)
    {
        GameObject temp = GameObject.Find("Arm").transform.Find("bullet").gameObject;
        GameObject flash = Instantiate(muzzleflash, temp.transform.position, temp.transform.rotation) as GameObject;
        flash.transform.localScale = scale*1.5f;
    }

}
