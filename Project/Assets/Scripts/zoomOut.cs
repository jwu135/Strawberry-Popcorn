using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomOut : MonoBehaviour
{

    public Camera cam;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, 0, speed), Mathf.Lerp(cam.transform.position.y, 3.7f, speed), Mathf.Lerp(cam.transform.position.z, -10.17f, speed));
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 8.709762f, speed);
    }

}
