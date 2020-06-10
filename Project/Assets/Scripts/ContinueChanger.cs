using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueChanger : MonoBehaviour
{
    public SpriteRenderer continueButton;
    public Sprite available;
    public Sprite notAvailable;
    public BoxCollider2D buttonCollider;
    // Start is called before the first frame update
    void Start()
    {
        if (UpgradeValues.deathCounter > 0)
        {
            continueButton.sprite = available;
            buttonCollider.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UpgradeValues.deathCounter == 0)
        {
            continueButton.sprite = notAvailable;
            buttonCollider.enabled = false;
        }
    }
}
