using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransition : MonoBehaviour
{
    public GameObject interactSign;
    private bool over = false;
    public bool door;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            interactSign.SetActive(true);
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
        interactSign.SetActive(false);
        over = false;
    }
}