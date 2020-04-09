using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] images = new Sprite[23];
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")){//|| Input.GetButtonDown("Fire1")) {
            index++;
            if(index>images.Length-1)
                SceneManager.LoadScene("Scenes/MainGameplay");
            else
                GetComponent<SpriteRenderer>().sprite = images[index];
        }
    }
}
