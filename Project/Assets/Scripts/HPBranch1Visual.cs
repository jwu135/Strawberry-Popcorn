using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBranch1Visual : MonoBehaviour
{
    public SpriteRenderer HPBranch1;
    public SpriteRenderer HPBranch2;
    public SpriteRenderer HPBranch3;
    public SpriteRenderer HPBranch4;
    public Sprite Eye0;
    public Sprite Eye1;
    public Sprite Eye2;
    public Sprite Eye3;
    public Sprite Eye4;
    public Sprite Eye5;


    // Start is called before the first frame update
    void Start()
    {
        HPBranch2.enabled = false;
        HPBranch3.enabled = false;
        HPBranch4.enabled = false;
        Debug.Log(UpgradeValues.bonusHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(UpgradeValues.bonusHealth == 0)
        {
            HPBranch1.sprite = Eye0;
            HPBranch2.enabled = false;
            HPBranch3.enabled = false;
            HPBranch4.enabled = false;
        }
        if (UpgradeValues.bonusHealth < 6)
        {
            HPBranch3.enabled = false;
            HPBranch4.enabled = false;
        }
        if (UpgradeValues.bonusHealth < 11)
        {
            HPBranch4.enabled = false;
        }
        if (UpgradeValues.bonusHealth > 0)
        {
            HPBranch2.enabled = true;
            HPBranch2.sprite = Eye0;
        }
        if (UpgradeValues.bonusHealth == 1)
        {
            HPBranch1.sprite = Eye1;
        }
        if (UpgradeValues.bonusHealth == 2)
        {
            HPBranch1.sprite = Eye2;
        }
        if (UpgradeValues.bonusHealth == 3)
        {
            HPBranch1.sprite = Eye3;
        }
        if (UpgradeValues.bonusHealth == 4)
        {
            HPBranch1.sprite = Eye4;
        }
        if (UpgradeValues.bonusHealth > 4)
        {
            HPBranch1.sprite = Eye5;
        }
        if (UpgradeValues.bonusHealth > 5)
        {
            HPBranch3.enabled = true;
        }
        if (UpgradeValues.bonusHealth == 6)
        {
            HPBranch2.sprite = Eye1;
            HPBranch3.sprite = Eye0;
        }
        if (UpgradeValues.bonusHealth == 7)
        {
            HPBranch2.sprite = Eye2;
        }
        if (UpgradeValues.bonusHealth == 8)
        {
            HPBranch2.sprite = Eye3;
        }
        if (UpgradeValues.bonusHealth == 9)
        {
            HPBranch2.sprite = Eye4;
        }
        if (UpgradeValues.bonusHealth > 9)
        {
            HPBranch2.sprite = Eye5;
        }
        if (UpgradeValues.bonusHealth > 10)
        {
            HPBranch4.enabled = true;
        }
        if (UpgradeValues.bonusHealth == 11)
        {
            HPBranch3.sprite = Eye1;
            HPBranch4.sprite = Eye0;
        }
        if (UpgradeValues.bonusHealth == 12)
        {
            HPBranch3.sprite = Eye2;
        }
        if (UpgradeValues.bonusHealth == 13)
        {
            HPBranch3.sprite = Eye3;
        }
        if (UpgradeValues.bonusHealth == 14)
        {
            HPBranch3.sprite = Eye4;
        }
        if (UpgradeValues.bonusHealth > 14)
        {
            HPBranch3.sprite = Eye5;
        }


    }
}
