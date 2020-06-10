using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;


public class ButtonDoSomethingPause : MonoBehaviour
{
    // Start is called before the first frame update
    public String scriptName = "ExitScript";
    [HideInInspector]
    public BoxCollider2D cursorBody; //the box collider of the cursor
    private BoxCollider2D buttonBody; //the box collider of the button
    System.Type MyScriptType;
    Component script;
    public Button doWork;


    void Start()
    {
        cursorBody = GameObject.Find("CursorIns(Clone)").GetComponent<BoxCollider2D>();
        buttonBody = GetComponent<BoxCollider2D>();
        MyScriptType = System.Type.GetType(scriptName + ",Assembly-CSharp");
        script = gameObject.AddComponent(MyScriptType);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use") == true || Input.GetButtonDown("Jump") == true || Input.GetButtonDown("Fire1"))//if the player clicks
        {
            Debug.Log("hi");
            if (cursorBody.IsTouching(buttonBody))
            {
                //gameObject.GetComponent(MyScriptType).ToString();
                Debug.Log("hi3");
                doWork.onClick.Invoke();
            }
        }
    }
}