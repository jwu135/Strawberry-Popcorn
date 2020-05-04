using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingPosition;
    Vector3 endingPosition;
    Vector3 endingRotation;
    bool move = false;
    void Start()
    {
        startingPosition = transform.position;
        endingPosition = startingPosition + new Vector3(0f, 14f, 0f);
        endingRotation = new Vector3(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log(startingPosition + " " + endingPosition);
            Debug.Log(transform.rotation + " " + endingRotation);
            move = true;
        }
        if (move) {
            transform.position = Vector3.Lerp(transform.position, endingPosition, Time.time/500);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(endingRotation), Time.time/10);
        }
    }
}
