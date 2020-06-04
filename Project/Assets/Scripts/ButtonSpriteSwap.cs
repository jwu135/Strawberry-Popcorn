using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpriteSwap : MonoBehaviour
{
    public Sprite[] buttonImages;
    private int index = 0;
    private bool alreadyDead = false; // insert meme here
    bool usingController = false;
    public bool AD = false;
    public string input;

    private void Awake()
    {
        activeSwap();
    }

    private void swap(int i)
    {
        index = i;
        GetComponent<SpriteRenderer>().sprite = buttonImages[i];
        Debug.Log("swapped");
    }

    private void activeSwap()
    {
        
        if (GlobalVariable.usingController) {
            // usingController = true;
            if (index != 1)
                swap(1);
        } else if (!usingController) {
            //usingController = false;
            if (index != 0)
                swap(0);
        }
    }

    private void Update()
    {
        float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script.
        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));

        // if player moves, check to see if they're using controller
        if ((Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)&&mag>0.15) {
            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                GlobalVariable.usingController = false;
            else
                GlobalVariable.usingController = true;
        }

        // If someone moves their mouse while moving with controller, it would get jittery, so forget it,
        ////if player aims, check to see if they're using controller
        //if (inputVector.magnitude > 0.3)
        //    GlobalVariable.usingController = true;
        //else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        //    GlobalVariable.usingController = false;


        activeSwap();
        
        if (!alreadyDead) {
            if (input.Equals("Jump") || input.Equals("Horizontal") || input.Equals("Roll")) {
                if (UpgradeValues.deathCounter != 0) {
                    Destroy(gameObject);
                }

                destroyInput(input);
            }
        }
    }


    void destroyInput(string input)
    {
        float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. 
        bool moving = mag > 0.15f;
        if (Input.GetButtonDown(input)) {
            StartCoroutine("fullDestroy");
        }
        if ((Input.GetAxis(input) < 0)) {
            StartCoroutine("fullDestroy");
        }
        if (moving&&input.Equals("Horizontal")) {
            StartCoroutine("fullDestroy");
        }
    }
    IEnumerator fullDestroy()
    {
        alreadyDead = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
