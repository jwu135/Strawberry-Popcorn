using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroHandler : MonoBehaviour
{
    public Sprite[] images = new Sprite[23];
    public Camera cutsceneCamera;
    public PlayableDirector outroCutscene;
    int index = 0;
    float cd = 3.5f;
    RuntimeAnimatorController AC;
    Vector3 defaultScale;
    Scene scene;
    bool started = false;
    // Start is called before the first frame update
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        defaultScale = transform.localScale;
        /*if(scene.name.Equals("Intro"))
            Screen.SetResolution(1280, 720, false);*/
        //AC = GetComponent<RuntimeAnimatorController>();
    }

    public void beginOutro()
    {
        cutsceneCamera.enabled = false;
        outroCutscene.Stop();
        cd = 3.5f;
        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        if (scene.name.Equals("Intro")) { 
            if (Input.GetButtonDown("interact") || cd <= 0) {//|| Input.GetButtonDown("Fire1")) {
                index++;
                if (index > images.Length - 1)
                    SceneManager.LoadScene("Scenes/MainMenu");
                else {
                    GetComponent<SpriteRenderer>().sprite = images[index];
                }
                cd = 3.5f;
            }
        }
        if (scene.name.Equals("Outro")&&started) { 
            if (Input.GetButtonDown("interact") || cd <= 0) {//|| Input.GetButtonDown("Fire1")) {
                index++;
                if (index == 7) {
                    GetComponent<Animator>().enabled = true;
                    transform.localScale = new Vector3(46.5f, 37f);
                    cd = 4;
                } else {
                    GetComponent<Animator>().enabled = false;
                    transform.localScale = defaultScale;
                    cd = 3.5f;
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
}
