using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henshin : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Camera cam2;
    public float speed;
    bool henshin = false;
    //public SpriteRenderer test1;
    //public SpriteRenderer test2;
    //public SpriteRenderer test3;
    private int counter = 0;

    public GameObject Player1;
    public GameObject Player2;
    public GameObject sp;


    void Start()
    {
        //cam = Camera.main;
       
        if (GlobalVariable.deathCounter > 0)
        {
            Player1.SetActive(false);
            cam.enabled = false;
            sp.SetActive(false);
        }
        else
        {
            Player2.SetActive(false);
            cam2.enabled = false;
        }


    }

    void Update()
    {
        if(henshin == true)
        {
            if(cam2.orthographicSize < 8.7f)
            {
                cam2.transform.position = new Vector3(Mathf.Lerp(cam2.transform.position.x, -3.33f, speed), Mathf.Lerp(cam2.transform.position.y, 2.5f, speed), Mathf.Lerp(cam2.transform.position.z, 0, speed));
                cam2.orthographicSize = Mathf.Lerp(cam2.orthographicSize, 8.709762f, speed);
            }

                Debug.Log(cam.orthographicSize);
            /*
        test1.color = new Color(1, 1, 1, 1);
        
        if (Input.GetButtonDown("Fire1") && counter == 1)
        {

            test3.color = new Color(1, 1, 1, 1);
            counter = 2;
        }
        else if (Input.GetButtonDown("Fire1") && counter == 0)
        {
            test2.color = new Color(1, 1, 1, 1);
            counter = 1;
        }
        else if (Input.GetButtonDown("Fire1") && counter == 2)
        {
            test1.enabled = false;
            test2.enabled = false;
            test3.enabled = false;
            counter = 3;
        }
        */

        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Use") && other.CompareTag("Player") && (henshin == false))
        {
            
            henshin = true;
            Player1.SetActive(false);
            cam2.enabled = true;
            cam.enabled = false;
            Player2.SetActive(true);
            cam2.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, -3.33f, speed), Mathf.Lerp(cam.transform.position.y, 2.5f, speed), Mathf.Lerp(cam.transform.position.z, 0, speed));
            cam2.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8.709762f, speed);
            sp.SetActive(false);


        }
    }

}
