using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builds : MonoBehaviour
{
    public int buildHPMin;
    public int buildHPMax;
    public int buildDmgMin;
    public int buildDmgMax;
    public int buildManaMin;
    public int buildManaMax;
    public bool chooseCorpse;

    public Transform standSP;
    public Transform standHP;
    public Transform standDmg;
    public Transform standMana;

    public GameObject displaySP;
    public GameObject displayHP;
    public GameObject displayDmg;
    public GameObject displayMana;

    public List<GameObject> allDisplay = new List<GameObject>();

    public Upgrade Upgrade;

    private int chosenSP;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeValues.choseCorpse1 = false;
        UpgradeValues.choseCorpse2 = false;
        UpgradeValues.choseCorpse3 = false;
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
                allDisplay.Add(Instantiate(displaySP, (standSP.position + new Vector3((0.0f + (5 * i)), 0.0f, 0.0f)), standSP.rotation));
                allDisplay[i].gameObject.name = "SPCorpse" + i;
            }

            //1st SP

            for(int i = 0; i < UpgradeValues.buildHealth1; i++)
            {
                allDisplay.Add(Instantiate(displayHP, (standHP.position + new Vector3((0.0f + (1.04f * i)), 0.0f, 0.0f)), standHP.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildDmg1; i++)
            {
                allDisplay.Add(Instantiate(displayDmg, (standDmg.position + new Vector3((0.0f + (1.04f * i)), 0.0f, 0.0f)), standDmg.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildMana1; i++)
            {
                allDisplay.Add(Instantiate(displayMana, (standMana.position + new Vector3((0.0f + (1.04f * i)), 0.0f, 0.0f)), standMana.rotation));
            }

            //2nd SP

            for (int i = 0; i < UpgradeValues.buildHealth2; i++)
            {
                allDisplay.Add(Instantiate(displayHP, (standHP.position + new Vector3((5.0f + (1.04f * i)), 0.0f, 0.0f)), standHP.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildDmg2; i++)
            {
                allDisplay.Add(Instantiate(displayDmg, (standDmg.position + new Vector3((5.0f + (1.04f * i)), 0.0f, 0.0f)), standDmg.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildMana2; i++)
            {
                allDisplay.Add(Instantiate(displayMana, (standMana.position + new Vector3((5.0f + (1.04f * i)), 0.0f, 0.0f)), standMana.rotation));
            }

            //3rd SP

            for (int i = 0; i < UpgradeValues.buildHealth3; i++)
            {
                allDisplay.Add(Instantiate(displayHP, (standHP.position + new Vector3((10.0f + (1.04f * i)), 0.0f, 0.0f)), standHP.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildDmg3; i++)
            {
                allDisplay.Add(Instantiate(displayDmg, (standDmg.position + new Vector3((10.0f + (1.04f * i)), 0.0f, 0.0f)), standDmg.rotation));
            }

            for (int i = 0; i < UpgradeValues.buildMana3; i++)
            {
                allDisplay.Add(Instantiate(displayMana, (standMana.position + new Vector3((10.0f + (1.04f * i)), 0.0f, 0.0f)), standMana.rotation));
            }

        }

    }

    // Update is called once per frame
    void Update()
    {



    }

    /*void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Builds1")
        {
            Debug.Log("B1");
            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth1();
                Upgrade.BuildDmg1();
                Upgrade.BuildMana1();
                Debug.Log(UpgradeValues.bonusHealth);
                Debug.Log(UpgradeValues.dodgeNeeded);
                Debug.Log(UpgradeValues.shieldDuration);
                chooseCorpse = true;
                Debug.Log("corpse1");
                UpgradeValues.choseCorpse1 = true;

            }
        }
        else if (col.tag == "Builds2")
        {
            Debug.Log("B2");
            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth2();
                Upgrade.BuildDmg2();
                Upgrade.BuildMana2();
                Debug.Log(UpgradeValues.bonusHealth);
                Debug.Log(UpgradeValues.dodgeNeeded);
                Debug.Log(UpgradeValues.shieldDuration);
                chooseCorpse = true;
                Debug.Log("corpse2");
                UpgradeValues.choseCorpse2 = true;
            }
        }
        else if (col.tag == "Builds3")
        {
            Debug.Log("B3");
            if (!chooseCorpse && Input.GetButtonDown("interact"))
            {
                Upgrade.BuildHealth3();
                Upgrade.BuildDmg3();
                Upgrade.BuildMana3();
                Debug.Log(UpgradeValues.bonusHealth);
                Debug.Log(UpgradeValues.dodgeNeeded);
                Debug.Log(UpgradeValues.shieldDuration);
                chooseCorpse = true;
                Debug.Log("corpse3");
                UpgradeValues.choseCorpse3 = true;
            }
        if (col.tag == "Builds")
        {*/

    public void chooseOne() {
        if (!chooseCorpse) {
            Upgrade.BuildHealth1();
            Upgrade.BuildDmg1();
            Upgrade.BuildMana1();
            Debug.Log(UpgradeValues.bonusHealth);
            Debug.Log(UpgradeValues.dodgeNeeded);
            Debug.Log(UpgradeValues.shieldDuration);
            chooseCorpse = true;
            Debug.Log("corpse1");
            GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP(allDisplay[0].transform.position);
            chosenSP = 0;
            destroyDisplay();
        }
    }
    public void chooseTwo()
    {
        if (!chooseCorpse) {
            Upgrade.BuildHealth2();
            Upgrade.BuildDmg2();
            Upgrade.BuildMana2();
            chooseCorpse = true;
            Debug.Log("corpse2");
            GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP(allDisplay[1].transform.position);
            chosenSP = 1;
            destroyDisplay();
        }
    }
    public void chooseThree()
    {
        if (!chooseCorpse) {
            Upgrade.BuildHealth3();
            Upgrade.BuildDmg3();
            Upgrade.BuildMana3();
            chooseCorpse = true;
            Debug.Log("corpse3");
            GameObject.Find("Henshin").transform.Find("New Sprite").GetComponent<Henshin>().choseSP(allDisplay[2].transform.position);
            chosenSP = 2;
            destroyDisplay();
        }
    }
    public void destroyDisplay()
    {
        int i = 0;
        foreach (GameObject disp in allDisplay) {
            if (i >= 0 && i < 3) {
                if(i==chosenSP)
                    Destroy(disp);
            }else
                Destroy(disp);
            i++;
        }
    }
    //}

    //}


}
