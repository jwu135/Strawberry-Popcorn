using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builds : MonoBehaviour
{
    public float buildHPMin;
    public float buildHPMax;
    public float buildDmgMin;
    public float buildDmgMax;
    public float buildManaMin;
    public float buildManaMax;
    public bool chooseCorpse;

    public Transform standSP;
    public Transform standHP;
    public Transform standDmg;
    public Transform standMana;

    public GameObject displaySP;
    public GameObject displayHP;
    public GameObject displayDmg;
    public GameObject displayMana;


    public Upgrade Upgrade;

    // Start is called before the first frame update
    void Start()
    {
        if (UpgradeValues.deathCounter > 0 && !UpgradeValues.builtCorpse)
        {
            UpgradeValues.buildHealth1 = Random.Range(buildHPMin, buildHPMax);
            UpgradeValues.buildDmg1 = Random.Range(buildDmgMin, buildDmgMax);
            UpgradeValues.buildMana1 = Random.Range(buildManaMin, buildManaMax);

            UpgradeValues.buildHealth2 = Random.Range(buildHPMin, buildHPMax);
            UpgradeValues.buildDmg2 = Random.Range(buildDmgMin, buildDmgMax);
            UpgradeValues.buildMana2 = Random.Range(buildManaMin, buildManaMax);

            UpgradeValues.buildHealth3 = Random.Range(buildHPMin, buildHPMax);
            UpgradeValues.buildDmg3 = Random.Range(buildDmgMin, buildDmgMax);
            UpgradeValues.buildMana3 = Random.Range(buildManaMin, buildManaMax);
            UpgradeValues.builtCorpse = true;
            Upgrade.SavePlayer();
            chooseCorpse = false;
        }
        if (UpgradeValues.deathCounter > 0)
        {
            //spawn SPs

            for (int i = 0; i < 3; i++)
            {
                Instantiate(displaySP, (standSP.position + new Vector3((0.0f + (5 * i)), 0.0f, 0.0f)), standSP.rotation);
            }

            //1st SP

            for(int i = 0; i < UpgradeValues.buildHealth1; i++)
            {
                Instantiate(displayHP, (standHP.position + new Vector3((0.0f + (1 * i)), 0.0f, 0.0f)), standHP.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildDmg1; i++)
            {
                Instantiate(displayHP, (standDmg.position + new Vector3((0.0f + (1 * i)), 0.0f, 0.0f)), standDmg.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildMana1; i++)
            {
                Instantiate(displayMana, (standMana.position + new Vector3((0.0f + (1 * i)), 0.0f, 0.0f)), standMana.rotation);
            }

            //2nd SP

            for (int i = 0; i < UpgradeValues.buildHealth2; i++)
            {
                Instantiate(displayHP, (standHP.position + new Vector3((5.0f + (1 * i)), 0.0f, 0.0f)), standHP.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildDmg2; i++)
            {
                Instantiate(displayHP, (standDmg.position + new Vector3((5.0f + (1 * i)), 0.0f, 0.0f)), standDmg.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildMana2; i++)
            {
                Instantiate(displayMana, (standMana.position + new Vector3((5.0f + (1 * i)), 0.0f, 0.0f)), standMana.rotation);
            }

            //3rd SP

            for (int i = 0; i < UpgradeValues.buildHealth3; i++)
            {
                Instantiate(displayHP, (standHP.position + new Vector3((10.0f + (1 * i)), 0.0f, 0.0f)), standHP.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildDmg3; i++)
            {
                Instantiate(displayHP, (standDmg.position + new Vector3((10.0f + (1 * i)), 0.0f, 0.0f)), standDmg.rotation);
            }

            for (int i = 0; i < UpgradeValues.buildMana3; i++)
            {
                Instantiate(displayMana, (standMana.position + new Vector3((10.0f + (1 * i)), 0.0f, 0.0f)), standMana.rotation);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {



    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Builds")
        {
            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth1();
                Upgrade.BuildDmg1();
                Upgrade.BuildMana1();
                chooseCorpse = true;
                Debug.Log("corpse1");
                GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP();
            }

            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth2();
                Upgrade.BuildDmg2();
                Upgrade.BuildMana2();
                chooseCorpse = true;
                Debug.Log("corpse2");
                GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP();
            }

            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth3();
                Upgrade.BuildDmg3();
                Upgrade.BuildMana3();
                chooseCorpse = true;
                Debug.Log("corpse3");
                GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP();
            }
        }

    }


}
