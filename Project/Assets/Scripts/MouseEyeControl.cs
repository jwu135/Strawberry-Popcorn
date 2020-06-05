using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class MouseEyeControl : MonoBehaviour
{

    public SpriteRenderer MouseEye;
    public Image Image;
    public Sprite MouseEyeOpen;
    public Sprite MouseEyeClosed;
    public bool animated = false;
    public RuntimeAnimatorController MousEyeOpenAnim;
    public RuntimeAnimatorController MousEyeClosedAnim;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Button" )
        {
            Debug.Log("J key was pressed");
            MouseEye.sprite = MouseEyeOpen;
            Image.sprite = MouseEyeOpen;
            if (animated) {
                GetComponent<Animator>().runtimeAnimatorController = MousEyeOpenAnim;
            }
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            Debug.Log("J key was pressed");
            MouseEye.sprite = MouseEyeClosed;
            Image.sprite = MouseEyeClosed;
            if (animated) {
                GetComponent<Animator>().runtimeAnimatorController = MousEyeClosedAnim;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (animated) {
            //Image.sprite = MouseEye.sprite;
        }
    }
}
