using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] public int index;
    [SerializeField] public string levelNmae;

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetButtonDown("Use") && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(index);
        }
    }
}
