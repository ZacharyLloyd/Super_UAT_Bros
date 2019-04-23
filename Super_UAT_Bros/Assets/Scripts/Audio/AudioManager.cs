using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager mastersounds; //This is for the background music to disable for win screen
    public Audio[] getAudio; //Getting the audio source

    //Called before the first frame
    void Awake()
    {
        mastersounds = this; //Setting the mastersounds
        DontDestroyOnLoad(this); //Don't destroy AudioManager
        foreach (Audio a in getAudio)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.enableLoop;
        }
    }
    // Update is called once per frame
    public void Play(string name)
    {
        Audio a = Array.Find(getAudio, sound => sound.name == name);
        if (a == null) { Debug.LogWarning("Sound name " + name + " was not found"); return; }
        a.source.Play();
    }
    // Update is called once per frame
    public void Stop(string name)
    {
        Audio a = Array.Find(getAudio, sound => sound.name == name);
        if (a == null) { Debug.LogWarning("Sound name " + name + " was not found"); return; }
        a.source.Stop();
    }
}