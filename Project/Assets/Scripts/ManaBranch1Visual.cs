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
        ManaBranch2.enabled = false;
        ManaBranch3.enabled = false;
        ManaBranch4.enabled = false;
        Debug.Log(UpgradeValues.shieldDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradeValues.dodgeNeeded  == 8)
        {
            ManaBranch1.sprite = Eye0;
            ManaBranch2.enabled = false;
            ManaBranch3.enabled = false;
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.shieldDuration < 8)
        {
            ManaBranch3.enabled = false;
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.shieldDuration < 13)
        {
            ManaBranch4.enabled = false;
        }
        if (UpgradeValues.dodgeNeeded < 8)
        {
            ManaBranch2.enabled = true;
            ManaBranch2.sprite = Eye0;
        }
        if (UpgradeValues.dodgeNeeded  == 7)
        {
            ManaBranch1.sprite = Eye1;
        }
        if (UpgradeValues.dodgeNeeded == 6)
        {
            ManaBranch1.sprite = Eye2;
        }
        if (UpgradeValues.dodgeNeeded == 5)
        {
            ManaBranch1.sprite = Eye3;
        }
        if (UpgradeValues.shieldDuration == 6)
        {
            ManaBranch1.sprite = Eye4;
        }
        if (UpgradeValues.shieldDuration > 6)
        {
            ManaBranch1.sprite = Eye5;
        }
        if (UpgradeValues.shieldDuration > 7)
        {
            ManaBranch3.enabled = true;
        }
        if (UpgradeValues.shieldDuration == 8)
        {
            ManaBranch2.sprite = Eye1;
            ManaBranch3.sprite = Eye0;
        }
        if (UpgradeValues.shieldDuration == 9)
        {
            ManaBranch2.sprite = Eye2;
        }
        if (UpgradeValues.shieldDuration == 10)
        {
            ManaBranch2.sprite = Eye3;
        }
        if (UpgradeValues.shieldDuration == 11)
        {
            ManaBranch2.sprite = Eye4;
        }
        if (UpgradeValues.shieldDuration > 11)
        {
            ManaBranch2.sprite = Eye5;
        }
        if (UpgradeValues.shieldDuration > 12)
        {
            ManaBranch4.enabled = true;
        }
        if (UpgradeValues.shieldDuration == 13)
        {
            ManaBranch3.sprite = Eye1;
            ManaBranch4.sprite = Eye0;
        }
        if (UpgradeValues.shieldDuration == 14)
        {
            ManaBranch3.sprite = Eye2;
        }
        if (UpgradeValues.shieldDuration == 15)
        {
            ManaBranch3.sprite = Eye3;
        }
        if (UpgradeValues.shieldDuration == 16)
        {
            ManaBranch3.sprite = Eye4;
        }
        if (UpgradeValues.shieldDuration > 16)
        {
            ManaBranch3.sprite = Eye5;
        }


    }
}
