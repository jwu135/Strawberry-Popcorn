using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public string levelNmae;
    private bool over = false;
    private void Update()
    {
        if (Input.GetButtonDown("Use") && over) {
            SceneManager.LoadScene(index);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            over = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            over = false;
        }
    }

}
