using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaBranch1Visual : MonoBehaviour
{
    public SpriteRenderer ManaBranch1;
    public SpriteRenderer ManaBranch2;
    public SpriteRenderer ManaBranch3;
    public SpriteRenderer ManaBranch4;
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
        if (UpgradeValues.bonusManaGain == 0)
        {
            ManaBranch1.sprite = Eye0;
            ManaBranch2.enabled = false;
            ManaBranch3.enabled = false;
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.bonusManaGain < 6)
        {
            ManaBranch3.enabled = false;
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.bonusManaGain < 11)
        {
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.bonusManaGain > 0)
        {
            ManaBranch2.enabled = true;
            ManaBranch2.sprite = Eye0;
        }
        if (UpgradeValues.bonusManaGain == 1)
        {
            ManaBranch1.sprite = Eye1;
        }
        if (UpgradeValues.bonusManaGain == 2)
        {
            ManaBranch1.sprite = Eye2;
        }
        if (UpgradeValues.bonusManaGain == 3)
        {
            ManaBranch1.sprite = Eye3;
        }
        if (UpgradeValues.bonusManaGain == 4)
        {
            ManaBranch1.sprite = Eye4;
        }
        if (UpgradeValues.bonusManaGain > 4)
        {
            ManaBranch1.sprite = Eye5;
        }
        if (UpgradeValues.bonusManaGain > 5)
        {
            ManaBranch3.enabled = true;
        }
        if (UpgradeValues.bonusManaGain == 6)
        {
            ManaBranch2.sprite = Eye1;
            ManaBranch3.sprite = Eye0;
        }
        if (UpgradeValues.bonusManaGain == 7)
        {
            ManaBranch2.sprite = Eye2;
        }
        if (UpgradeValues.bonusManaGain == 8)
        {
            ManaBranch2.sprite = Eye3;
        }
        if (UpgradeValues.bonusManaGain == 9)
        {
            ManaBranch2.sprite = Eye4;
        }
        if (UpgradeValues.bonusManaGain > 9)
        {
            ManaBranch2.sprite = Eye5;
        }
        if (UpgradeValues.bonusManaGain > 10)
        {
            ManaBranch4.enabled = true;
        }
        if (UpgradeValues.bonusManaGain == 11)
        {
            ManaBranch3.sprite = Eye1;
            ManaBranch4.sprite = Eye0;
        }
        if (UpgradeValues.bonusManaGain == 12)
        {
            ManaBranch3.sprite = Eye2;
        }
        if (UpgradeValues.bonusManaGain == 13)
        {
            ManaBranch3.sprite = Eye3;
        }
        if (UpgradeValues.bonusManaGain == 14)
        {
            ManaBranch3.sprite = Eye4;
        }
        if (UpgradeValues.bonusManaGain > 14)
        {
            ManaBranch3.sprite = Eye5;
        }


    }
}
