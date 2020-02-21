using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    private int usingController = 0; //mouse=0 controller=1;
    void Update()
    {
        if (Input.GetAxis("Aim_Vertical") != 0 || Input.GetAxis("Aim_Horizontal") != 0)
        {
            usingController = 1;
        }
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usingController = 0;
        }
        if (usingController == 0) FaceMouse();
        else if (usingController == 1) FaceController();

        //Debug.Log(usingController);
    }

    void FaceMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    void FaceController()
    {
        Vector2 aimAxis = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));

        if(aimAxis.magnitude > 0.1)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }
    }
}
