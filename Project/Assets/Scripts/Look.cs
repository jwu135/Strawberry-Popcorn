using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DragonBones;

public class Look : MonoBehaviour
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
    public float laserTurnSpeed = 35f; //when editing this variable from the inspector, the only one that matters is the one attached to the laser instance

    private Vector3 scaleVector;

    public PlayerCombat PlayerCombat;
    private bool switch1 = false;

    private void Start()
    {
        m_camera = Camera.main;
        player = GameObject.Find("Player");
        armature = GameObject.FindGameObjectsWithTag("ArmatureTag");
        armatureTransform = armature[0].GetComponent<Transform>();
        aimDeadzone = 0.9f;

        scaleVector = armatureTransform.localScale;//new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void setArmature()
    {
        armature[0] = GameObject.FindGameObjectWithTag("ArmatureTag");
        armatureTransform = armature[0].GetComponent<Transform>();

    }

    public void setScaleVector(float sc)
    {
        scaleVector.x = sc;
    }

    void Update()
    {
        m_camera = Camera.main;
        if(player.active == false)
        {
            player = GameObject.Find("Player2");
        }
        if (Time.timeScale != 0)
        {
            lookAround();
        }
    }

    void lookAround()
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

        if (usingController == 0 && !PlayerCombat.LASER) FaceMouse();
        else if (usingController == 1 && !PlayerCombat.LASER) FaceController();
        else if (PlayerCombat.LASER && gameObject.tag == "specialAttack2")
        {
            if (usingController == 0)
            {
                FaceMouseSlow();
            }
            else if (usingController == 1)
            {
                FaceControllerSlow();
            }
        }
        // else if (usingController == 0 && PlayerCombat.LASER && !switch1)
        //   {
        //       switch1 = true;
        //       FaceMouseSlow();
        //   }


        //Debug.Log(usingController);

    }

    void FaceMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        armatureTransform.localScale = scaleVector;

        if (Input.mousePosition.x > playerPosition.x)
        {
            player.GetComponent<Movement>().flip(0, -0.5f,this);
        }
        else if (Input.mousePosition.x < playerPosition.x)
        {
            player.GetComponent<Movement>().flip(1, 0.5f,this);
        }

        //Debug.Log(transform.rotation);
    }

    void FaceMouseSlow()
    {
        float step = laserTurnSpeed * Time.deltaTime;
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 axis;
        float currAngle;
        float finalAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Inverse(Quaternion.AngleAxis(finalAngle, Vector3.forward)), step);
        transform.rotation.ToAngleAxis(out currAngle, out axis);

        armatureTransform.localScale = scaleVector;

        if (axis.z > 0)
        {
            player.GetComponent<Movement>().flip(1, 0.5f,this);
        }
        else if (axis.z < 0)
        {
            player.GetComponent<Movement>().flip(0, -0.5f,this);
        }
    }
    void FaceController()
    {
        Vector2 aimAxis = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));

        if(aimAxis.magnitude > aimDeadzone)
        {
            float angle = Mathf.Atan2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical")) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        /*else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        }*/

        if(Input.GetAxis("Aim_Horizontal") > 0.1f && aimAxis.magnitude > aimDeadzone)
        {
            player.GetComponent<Movement>().flip(1, 0.5f,this);
        }
        else if(Input.GetAxis("Aim_Horizontal") < -0.1f && aimAxis.magnitude > aimDeadzone)
        {
            player.GetComponent<Movement>().flip(0, -0.5f,this);
        }

        armatureTransform.localScale = scaleVector;
    }

    void FaceControllerSlow()
    {
        float step = laserTurnSpeed * Time.deltaTime;
        Vector2 aimAxis = new Vector2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical"));
        if (aimAxis.magnitude > aimDeadzone)
        {
            float finalAngle = Mathf.Atan2(Input.GetAxis("Aim_Horizontal"), Input.GetAxis("Aim_Vertical")) * Mathf.Rad2Deg;
            Vector3 axis;
            float currAngle;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(finalAngle, Vector3.forward), step);
            transform.rotation.ToAngleAxis(out currAngle,out axis);
            //Debug.Log("#########################axis: " + axis.ToString());

            armatureTransform.localScale = scaleVector;


            if (axis.z > 0 && aimAxis.magnitude > aimDeadzone)
            {
                player.GetComponent<Movement>().flip(1, 0.5f,this);
            }
            else if (axis.z < 0 && aimAxis.magnitude > aimDeadzone)
            {
                player.GetComponent<Movement>().flip(0, -0.5f,this);
            }
        }
    }
}
