using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool usingController = false;
    public float distance = 10; // how far the reticle should be while aiming on controller
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        if (inputVector.magnitude > 0.3)
        {
            usingController = true;
        }
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usingController = false;
        }

        Cursor.visible = false;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (usingController == false)//if player is using mouse
        {
            transform.position = mousePos;
        }
        else if(usingController == true && inputVector.magnitude > 0.3) //if player is pushing a direction on the left stick
        {
            gameObject.SetActive(true);
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            float angle = Mathf.Atan2(-Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
            
            Vector3 aimPos = new Vector3(playerPos.x + distance * Mathf.Sin(angle), playerPos.y + distance * Mathf.Cos(angle));
            transform.position = aimPos;
        }
        else
        {
            Vector3 aimPos = new Vector3(0, 0, -100);
            transform.position = aimPos;
        }
    }

}
