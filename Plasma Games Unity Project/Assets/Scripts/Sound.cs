/*
    Associates settings and names with each audio clip.
*/
using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sound {
    public string name; // The name of the audio clicp
    public AudioClip clip; // The audio file
    [Range (0f, 1f)]
    public float volume; // The volume
    [Range (.1f, 3f)]
    public float pitch; // The pitch
    [HideInInspector]
    public AudioSource source; // The source
    public bool loop, muteWhenPaused; // If the clip should loop, or pause when the game is paused
}
