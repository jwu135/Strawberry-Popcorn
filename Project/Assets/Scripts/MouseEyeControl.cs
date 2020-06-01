using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseEyeControl : MonoBehaviour
{

    public SpriteRenderer MouseEye;
    public Image Image;
    public Sprite MouseEyeOpen;
    public Sprite MouseEyeClosed;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Button" )
        {
            Debug.Log("J key was pressed");
            MouseEye.sprite = MouseEyeOpen;
            Image.sprite = MouseEyeOpen;
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            Debug.Log("J key was pressed");
            MouseEye.sprite = MouseEyeClosed;
            Image.sprite = MouseEyeClosed;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
