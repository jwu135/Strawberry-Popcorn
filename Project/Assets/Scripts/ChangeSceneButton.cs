using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneToChangeTo;
    private BoxCollider2D cursorBody;
    private BoxCollider2D buttonBody;

    void Start()
    {
        cursorBody = GameObject.Find("MenuCursor").GetComponent<BoxCollider2D>();
        buttonBody = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Use") == true || Input.GetButtonDown("Jump") == true || Input.GetButtonDown("Fire1"))//if the player clicks
        {
            if(cursorBody.IsTouching(buttonBody) )
            {
                SceneManager.LoadScene(sceneToChangeTo);
            }
        }
    }
}
