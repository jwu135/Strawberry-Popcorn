using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] images = new Sprite[23];
    int index = 0;
    float cd = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (Input.GetButtonDown("interact")||cd<=0){//|| Input.GetButtonDown("Fire1")) {
            index++;
            if(index>images.Length-1)
                SceneManager.LoadScene("Scenes/MainMenu");
                //SceneManager.LoadScene("Scenes/Intro2");
            else
                GetComponent<SpriteRenderer>().sprite = images[index];
            cd = 2;
        }
    }
}
