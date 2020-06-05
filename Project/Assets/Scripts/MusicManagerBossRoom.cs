using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManagerBossRoom : MonoBehaviour
{
    public string startMusic;
    public string loopMusic;
    AudioSource audio;
    float defaultVolume;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        defaultVolume = audio.volume;
        audio.volume = defaultVolume*UpgradeValues.overallvolume;
    }

    public void play()
    {
        CancelInvoke();
        audio.clip = Resources.Load("Sounds/Music/" + startMusic) as AudioClip;
        audio.Play();
        Invoke("PlayRepeat", audio.clip.length - 1f);
    }

    private void OnDestroy()
    {
        audio.Stop();
        CancelInvoke();
    }
    private void PlayRepeat()
    {
        Debug.Log("looped");
        audio.clip = Resources.Load("Sounds/Music/" + loopMusic) as AudioClip;
        audio.Play();
        Invoke("PlayRepeat", audio.clip.length);
    }
}
