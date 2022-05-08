/*
    Manages and plays all of the SFX and music for the game.
*/
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour {
    [SerializeField]
    AudioMixerGroup mixerSFX, mixerMusic; // The mixers
    [SerializeField]
    Sound[] sounds, music; // The SFX and music sounds
    public static AudioManager instance;
    int previousSong = -1; // The previously played song.
    bool paused = false;
    void OnEnable() {
        // Tells 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        // Tells 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    // Stops all sounds when a new scene is loaded
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        foreach (Sound sound in sounds) {
            sound.source.Stop();
        }
    }

    void Awake() {
        // Makes sure there aren't duplicate Audio managers
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        // Sets up all of the sounds for use
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = mixerSFX;
            s.source.volume = s.volume;
            if (s.pitch != 0) {
                s.source.pitch = s.pitch;
            }
            s.source.loop = s.loop;
        }
        // Sets up all of the music for use
        foreach (Sound s in music) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = mixerMusic;
            s.source.volume = s.volume;
            if (s.pitch != 0) {
                s.source.pitch = s.pitch;
            }
            s.source.loop = s.loop;
        }
        // Starts playing music
        StartCoroutine(PlayMusic());
    }
    void Update() {
        // Pauses certain sounds when the game is pauses, and starts playing them again when the game is unpaused
        if (Time.timeScale == 0 && !paused) {
            foreach (Sound s in sounds) {
                if (s.muteWhenPaused)
                    s.source.volume = 0;
            }
            paused = true;
        } else if (paused && Time.timeScale != 0) {
            paused = false;
            foreach (Sound s in sounds) {
                if (s.muteWhenPaused)
                    s.source.volume = .2f;
            }
        }
    }
    // Plays a sound with a given name
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        // Warns if a sound isn't found
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            print(name + ": is not found!");
            return;
        }
        s.source.Play();     
    }
    // Stops a given sound
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    // Plays a given song
    void PlaySong(string name) {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    // Plays a new song after the previous song finishes playing
    IEnumerator PlayMusic() {
        int randomSong;
        do {
            randomSong = UnityEngine.Random.Range(0, music.Length);
        } while (previousSong == randomSong);
        previousSong = randomSong;
        PlaySong(music[randomSong].name);
        yield return new WaitForSeconds(music[randomSong].clip.length);
        StartCoroutine(PlayMusic());
    }
}
