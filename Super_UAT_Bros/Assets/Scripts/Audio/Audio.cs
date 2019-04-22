using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name; //Name for audio

    public AudioClip clip; //The actual audio clip

    //Setting volume and pitch to be adjusted
    [Range(0f, 1f)]
    public float volume;
    [Range(1f, 3f)]
    public float pitch;

    //To allow audio to be looped
    public bool enableLoop;

    //The audio's source
    [HideInInspector] public AudioSource source;
}

