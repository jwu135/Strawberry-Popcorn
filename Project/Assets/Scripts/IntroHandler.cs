using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] images = new Sprite[23];
    int index = 0;
    float cd = 1;
    RuntimeAnimatorController AC;
    Vector3 defaultScale;
    // Start is called before the first frame update
    private void Start()
    {
        defaultScale = transform.localScale;
        //AC = GetComponent<RuntimeAnimatorController>();
    }
    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (Input.GetButtonDown("interact")||cd<=0){//|| Input.GetButtonDown("Fire1")) {
            index++;
            if (index == 7) {
                GetComponent<Animator>().enabled = true;
                transform.localScale = new Vector3(46.5f, 37f);
                cd = 5;
            } else {
                GetComponent<Animator>().enabled = false;
                transform.localScale = defaultScale;
                cd = 2;
            }
            if (index > images.Length - 1)
                SceneManager.LoadScene("Scenes/MainMenu");
            //SceneManager.LoadScene("Scenes/Intro2");
            else {
                GetComponent<SpriteRenderer>().sprite = images[index];   
            }
        }
    }
}
