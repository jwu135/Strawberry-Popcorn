﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public string startMusic;
    public string loopMusic;
    AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.volume = 0.3f;
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu" || scene.name == "Intro") {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

            if (objs.Length > 1) {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
        }
        if (!audio.isPlaying) {
            CancelInvoke();
            audio.clip = Resources.Load("Sounds/Music/"+startMusic) as AudioClip;
            audio.Play();
            Invoke("PlayRepeat", audio.clip.length-1f);
        }
        
    }

    private void OnDestroy()
    {
        audio.Stop();
        CancelInvoke();
    }
    private void PlayRepeat()
    {
        //audio.Stop();
        audio.clip = Resources.Load("Sounds/Music/" + loopMusic) as AudioClip;
        audio.Play();
        Invoke("PlayRepeat", audio.clip.length);
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MainMenu" || scene.name == "Intro") {

        } else {
            Destroy(this.gameObject);
        }
    }
}
