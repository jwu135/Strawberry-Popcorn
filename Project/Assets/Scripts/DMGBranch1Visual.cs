using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMGBranch1Visual : MonoBehaviour
{
    public SpriteRenderer DMGBranch1;
    public SpriteRenderer DMGBranch2;
    public SpriteRenderer DMGBranch3;
    public SpriteRenderer DMGBranch4;
    public Sprite Eye0;
    public Sprite Eye1;
    public Sprite Eye2;
    public Sprite Eye3;
    public Sprite Eye4;
    public Sprite Eye5;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradeValues.bonusAttackDmg == 0)
        {
            DMGBranch1.sprite = Eye0;
            DMGBranch2.enabled = false;
            DMGBranch3.enabled = false;
            DMGBranch4.enabled = false;
        }
        if (UpgradeValues.bonusAttackDmg < 6)
        {
            DMGBranch3.enabled = false;
            DMGBranch4.enabled = false;
        }
        if (UpgradeValues.bonusAttackDmg < 11)
        {
            DMGBranch4.enabled = false;
        }
        if (UpgradeValues.bonusAttackDmg > 0)
        {
            DMGBranch2.enabled = true;
            DMGBranch2.sprite = Eye0;
        }
        if (UpgradeValues.bonusAttackDmg == 1)
        {
            DMGBranch1.sprite = Eye1;
        }
        if (UpgradeValues.bonusAttackDmg == 2)
        {
            DMGBranch1.sprite = Eye2;
        }
        if (UpgradeValues.bonusAttackDmg == 3)
        {
            DMGBranch1.sprite = Eye3;
        }
        if (UpgradeValues.bonusAttackDmg == 4)
        {
            DMGBranch1.sprite = Eye4;
        }
        if (UpgradeValues.bonusAttackDmg > 4)
        {
            DMGBranch1.sprite = Eye5;
        }
        if (UpgradeValues.bonusAttackDmg > 5)
        {
            DMGBranch3.enabled = true;
        }
        if (UpgradeValues.bonusAttackDmg == 6)
        {
            DMGBranch2.sprite = Eye1;
            DMGBranch3.sprite = Eye0;
        }
        if (UpgradeValues.bonusAttackDmg == 7)
        {
            DMGBranch2.sprite = Eye2;
        }
        if (UpgradeValues.bonusAttackDmg == 8)
        {
            DMGBranch2.sprite = Eye3;
        }
        if (UpgradeValues.bonusAttackDmg == 9)
        {
            DMGBranch2.sprite = Eye4;
        }
        if (UpgradeValues.bonusAttackDmg > 9)
        {
            DMGBranch2.sprite = Eye5;
        }
        if (UpgradeValues.bonusAttackDmg > 10)
        {
            DMGBranch4.enabled = true;
        }
        if (UpgradeValues.bonusAttackDmg == 11)
        {
            DMGBranch3.sprite = Eye1;
            DMGBranch4.sprite = Eye0;
        }
        if (UpgradeValues.bonusAttackDmg == 12)
        {
            DMGBranch3.sprite = Eye2;
        }
        if (UpgradeValues.bonusAttackDmg == 13)
        {
            DMGBranch3.sprite = Eye3;
        }
        if (UpgradeValues.bonusAttackDmg == 14)
        {
            DMGBranch3.sprite = Eye4;
        }
        if (UpgradeValues.bonusAttackDmg > 14)
        {
            DMGBranch3.sprite = Eye5;
        }


    }
}
