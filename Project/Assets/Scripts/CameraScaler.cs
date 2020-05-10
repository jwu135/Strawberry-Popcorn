using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public Camera camera;
    private Resolution resolution;
    // Start is called before the first frame update
    void Start()
    {
        resize();
    }

    void resize()
    {
        resolution = Screen.currentResolution;
        float targetaspect = 16f / 9f;
        float windowaspect = (float)Screen.width / (float)Screen.height;
        camera.orthographicSize = camera.orthographicSize * (targetaspect / windowaspect);
    }

    // Update is called once per frame
    void Update()
    {
        if (!resolution.Equals(Screen.currentResolution)) {
            Debug.Log("resized");
            resize();   
        }
    }
}
