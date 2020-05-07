using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool usingController = false;
    public float distance = 10; // how far the reticle should be while aiming on controller
    private Vector3 pos1 = new Vector3(0, 0, -100);
    private Vector3 pos2 = new Vector3(0, 0, -100);
    private Vector3 pos3 = new Vector3(0, 0, -100);
    private Vector3 pos4 = new Vector3(0, 0, -100);
    private Vector3 pos5 = new Vector3(0, 0, -100);
    private Vector3 pos6 = new Vector3(0, 0, -100);
    private Vector3 pos7 = new Vector3(0, 0, -100);
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

            pos1 = pos2;
            pos2 = pos3;
            pos3 = pos4;
            pos4 = pos5;
            pos5 = pos6;
            pos6 = pos7;
            pos7 = new Vector3(playerPos.x + distance * Mathf.Sin(angle), playerPos.y + distance * Mathf.Cos(angle));
            Vector3 aimPos = new Vector3( (pos1.x + pos2.x + pos3.x + pos4.x + pos5.x + pos6.x + pos7.x) /7, (pos1.y + pos2.y + pos3.y + pos4.y + pos5.y + pos6.y + pos7.y) / 7);
            transform.position = aimPos;
        }
        else
        {
            Vector3 aimPos = new Vector3(0, 0, -100);
            transform.position = aimPos;
        }
    }

}
