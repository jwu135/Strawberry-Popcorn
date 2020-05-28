using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADControlsHandler : MonoBehaviour
{
    public GameObject[] buttons = new GameObject[3];
    // Start is called before the first frame update
    private int index = 0;

    private void Awake()
    {
        swap(0);
    }

    private void swap(int i)
    {
        index = i;
        if(index == 0) {
            buttons[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            buttons[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            buttons[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        } else {
            buttons[0].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            buttons[1].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            buttons[2].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        //GetComponent<SpriteRenderer>().sprite = buttonImages[i];
        Debug.Log("swapped");
    }
    private void swapCheck()
    {
        if (GlobalVariable.usingController) {
            // usingController = true;
            if (index != 1)
                swap(1);
        } else if (!GlobalVariable.usingController) {
            //usingController = false;
            if (index != 0)
                swap(0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        swapCheck();
    }
}
