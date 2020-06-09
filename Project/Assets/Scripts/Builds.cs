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

    public List<GameObject> allDisplay = new List<GameObject>();

    public Upgrade Upgrade;

    private int chosenSP;

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
        if (col.tag == "Builds")
        {*/
    public void chooseOne() {
        if (!chooseCorpse) {
            //Upgrade.BuildHealth1();
            //Upgrade.BuildDmg1();
            //Upgrade.BuildMana1();
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
            //Upgrade.BuildHealth2();
            //Upgrade.BuildDmg2();
            //Upgrade.BuildMana2();
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
            //Upgrade.BuildHealth3();
            //Upgrade.BuildDmg3();
            //Upgrade.BuildMana3();
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
