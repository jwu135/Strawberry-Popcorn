using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public GameObject interactSign;
    private bool over = false;
    public bool door;
    private void Start()
    {
        interactSign = GameObject.FindGameObjectWithTag("InteractSign");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            interactSign = GameObject.FindGameObjectWithTag("InteractSign");
            interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            over = true;
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("interact")&&over&&door) {
            SceneManager.LoadScene("Scenes/MainGameplay");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        interactSign.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        over = false;
    }
}