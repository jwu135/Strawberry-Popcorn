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

    private void swap(int i)
    {
        index = i;
        GetComponent<SpriteRenderer>().sprite = buttonImages[i];
        Debug.Log("swapped");
    }

    private void Update()
    {
        // From one of Ethan's script
        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.

        if (!usingController) {
            usingController = (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D));
            
        }
        if (usingController) {
            usingController = !(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
        }
        
        if (!usingController)
            Debug.Log("stopped using controller");

        if (inputVector.magnitude > 0.3 || usingController ) {
           // usingController = true;
            if (index != 1)
                swap(1);
        } else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            //usingController = false;
            if (index != 0)
                swap(0);
        }
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
        float mag = new Vector2(Input.GetAxisRaw("Horizontal"), 0).magnitude; // technique from Ethan's script. Don't want to read it in from there yet to avoid making changes to other people's scripts. Making the deadzone variable public or adding a function call to add the value to this script would be fine for doing this.
        bool moving = mag > 0.15f;
        if (Input.GetButtonDown(input)) {
            StartCoroutine("fullDestroy");
        }
        if (Input.GetAxis(input)>0) {
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
    // need one for Horizontal left and right

    // need one for space

    // need one for shift

}
