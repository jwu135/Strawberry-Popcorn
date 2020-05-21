using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

public class ButtonDoSomething : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public String scriptName = "ExitScript";
    private BoxCollider2D cursorBody;
    private BoxCollider2D buttonBody;
    System.Type MyScriptType;
    Component script;


    void Start()
    {
        cursorBody = GameObject.Find("MenuCursor").GetComponent<BoxCollider2D>();
        buttonBody = GetComponent<BoxCollider2D>();
        MyScriptType = System.Type.GetType(scriptName + ",Assembly-CSharp");
        script = gameObject.AddComponent(MyScriptType);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use") == true || Input.GetButtonDown("Jump") == true || Input.GetButtonDown("Fire1"))//if the player clicks
        {
            if (cursorBody.IsTouching(buttonBody))
            {
                gameObject.GetComponent(MyScriptType).ToString();
            }
        }
    }
}