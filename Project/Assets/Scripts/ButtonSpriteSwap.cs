using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteSwap : MonoBehaviour
{
    public Sprite[] buttonImages;
    private int index = 0;
    bool usingController = false;
    public bool AD = false;

    private void swap(int i)
    {
        index = i;
        GetComponent<SpriteRenderer>().sprite = buttonImages[i];
    }

    private void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        if (inputVector.magnitude > 0.3) {
            usingController = true;
            Debug.Log("using controller");
            if (index != 1)
                swap(1);
        } else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            usingController = false;
            if (index != 0)
                swap(0);
        }
    }


}
