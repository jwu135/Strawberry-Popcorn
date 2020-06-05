using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerRoom2 : MonoBehaviour
{
    public string music;
    AudioSource audio;
    float defaultVolume;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        //audio.volume = 0.3f;
        defaultVolume = audio.volume;
        audio.volume = defaultVolume*UpgradeValues.overallvolume;
        //Scene scene = SceneManager.GetActiveScene();
    }

    public void play()
    {
        audio.clip = Resources.Load("Sounds/Music/" + music) as AudioClip;
        audio.Play();
    }

    private void OnDestroy()
    {
        audio.Stop();
        CancelInvoke();
    }
    

}
