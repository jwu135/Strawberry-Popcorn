using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxScript : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing = 1f;
    private Vector3 previousPos;

    // Start is called before the first frame update
    void Start()
    {
        previousPos = transform.position;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++) {
            float parallax = (previousPos.x - transform.position.x) * parallaxScales[i];

            float backgroundPosX = backgrounds[i].position.x + parallax;
            Vector3 backgroundPos = new Vector3(backgroundPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundPos, Time.deltaTime * smoothing);
        }
        previousPos = transform.position;

        // stuff for locking camera
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos = GameObject.Find("Player").transform.position;
        //pos.x += -7.63f;
        pos.y += 3.11f;
        pos.z = -100f;
        pos.x = Mathf.Clamp(pos.x, 2.02f, 37.58f);
        transform.position = pos;
        //Debug.Log(pos.x);

    }
}
