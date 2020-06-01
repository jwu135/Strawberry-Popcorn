using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScriptNew : MonoBehaviour
{
    public Vector2 parallaxMult;

    public Transform camera;
    private Vector3 lastCameraPos;
    // Start is called before the first frame update
    void Start()
    {
        lastCameraPos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // .1 offset to the left when mult is .1
        // .3 offfset to the left when mult is .3


        Vector3 deltaMovement = camera.position - lastCameraPos;
        Vector3 pos = transform.localPosition;
        pos += new Vector3(deltaMovement.x * parallaxMult.x, deltaMovement.y * parallaxMult.y);
        transform.localPosition = pos;

        lastCameraPos = camera.position;
    }
}
