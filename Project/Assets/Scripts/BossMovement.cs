using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class BossMovement : MonoBehaviour
{
    float nextFlicker = 0f;
    float flickerTimeTrack = 2f;
    float t_intensity = 0f;
    public bool smooth = true;
    void Update()
    {
        if (Time.timeScale != 0) {
            lookAround();
        }
    }
    // Update is called once per frame
    void lookAround()
    {
        Vector3 temp = transform.localPosition;
        temp.x = Mathf.Sin(Time.time / 2) * 4.5f;
        temp.y = Mathf.Sin(Time.time / 2) * Mathf.Cos(Time.time / 2);
        transform.localPosition = temp;


        Vector3 direction = (Vector2)(GameObject.Find("Player").transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        if (Input.GetKeyDown(KeyCode.Z)) {
            smooth = !smooth;
        }

        if (smooth) {
            float lightIntensity = GetComponent<Light2D>().intensity;
            if (flickerTimeTrack < Time.time) {
                Debug.Log("Flickering down");
                t_intensity = 0.20f;
                lightIntensity -= t_intensity;
                flickerTimeTrack = Random.Range(2f, 3f) + Time.time;
            }
            if (t_intensity != 0) {
                lightIntensity = Mathf.Clamp(t_intensity + lightIntensity, 0, 1);
                Debug.Log(lightIntensity);
                t_intensity += 0.01f;
                if (lightIntensity == 1)
                    t_intensity = 1;
            }

            GetComponent<Light2D>().intensity = lightIntensity;
        } else {
            if (flickerTimeTrack < Time.time) {
                GetComponent<Light2D>().intensity = 0;
            }
            if(flickerTimeTrack+0.1f < Time.time) {
                GetComponent<Light2D>().intensity = 1;
                flickerTimeTrack = Random.Range(2f, 3f) + Time.time;
            }
        }
    }
}