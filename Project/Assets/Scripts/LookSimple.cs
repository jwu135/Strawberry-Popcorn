using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookSimple : MonoBehaviour
{
    [HideInInspector]
    public int usingController = 0; //mouse=0 controller=1;
    private GameObject[] armature;
    private UnityEngine.Transform armatureTransform;

    private GameObject player;
    private Transform playerTransform;
    // get player position
    private Vector3 playerPosition;
    private Camera m_camera;
    private float aimDeadzone;

    private Vector3 scaleVector;

    private bool switch1 = false;

    public Cannon Cannon;

    private void Start()
    {
        m_camera = Camera.main;
        player = GameObject.Find("Player");
        armature = GameObject.FindGameObjectsWithTag("ArmatureTag");
        armatureTransform = armature[0].GetComponent<Transform>();
        aimDeadzone = 0.9f;

        scaleVector = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void setScaleVector(float sc)
    {
        scaleVector.x = sc;
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        if (inputVector.magnitude > 0.3)
        {
            usingController = 1;
        }
        else if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usingController = 0;
        }

        playerTransform = player.transform;
        playerPosition = m_camera.WorldToScreenPoint(playerTransform.position);

        if (usingController == 0 && !Cannon.release) FaceMouse();

        // else if (usingController == 0 && PlayerCombat.LASER && !switch1)
        //   {
        //       switch1 = true;
        //       FaceMouseSlow();
        //    }
        else if (usingController == 1 && !Cannon.release) FaceController();


        //Debug.Log(usingController);

    }

    void FaceMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //if(transform.rotation == Quaternion.Euler(0, 90, 0))
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        armatureTransform.localScale = scaleVector;

        if (Input.mousePosition.x > playerPosition.x)
        {
            player.GetComponent<Movement>().flip(0, -0.5f,this);
        }
        else if (Input.mousePosition.x < playerPosition.x)
        {
            player.GetComponent<Movement>().flip(1, 0.5f, this);
        }

        //Debug.Log(transform.rotation);
    }


    void FaceController()
    {
        Vector2 aimAxis = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));

        if (aimAxis.magnitude > aimDeadzone)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        /*else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }*/

        if (Input.GetAxis("Aim_Horizontal") > 0.1f && aimAxis.magnitude > aimDeadzone)
        {
            player.GetComponent<Movement>().flip(1, 0.5f, this);
        }
        else if (Input.GetAxis("Aim_Horizontal") < -0.1f && aimAxis.magnitude > aimDeadzone)
        {
            player.GetComponent<Movement>().flip(0, -0.5f, this);
        }

        armatureTransform.localScale = scaleVector;
    }
}