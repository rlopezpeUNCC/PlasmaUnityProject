using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour {
    [SerializeField]
    AudioMixerGroup mixerSFX, mixerMusic;
    [SerializeField]
    Sound[] sounds, music;
    public static AudioManager instance;
    int previousSong = -1;
    bool paused = false;
    void OnEnable() {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable() {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
        foreach (Sound sound in sounds) {
            sound.source.Stop();
        }
    }
    void Awake() {
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

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
        StartCoroutine(PlayMusic());
    }
    void Update() {
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

    public void Play(string name) {
        if (name == "ItemEquiped" || name == "PlayerHit") {
            int chance = UnityEngine.Random.Range(0, 2);
            if (chance == 0) {
                name += "1";
            } else {
                name += "2";
            }
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            print(name + ": is not found!");
            return;
        }
        s.source.Play();
        //print("Playing: " + name);      
    }
    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    void PlaySong(string name) {
        Sound s = Array.Find(music, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
        //print("Playing: " + name);
    }

    IEnumerator PlayMusic() {
        int randomSong;
        do {
            randomSong = UnityEngine.Random.Range(0, music.Length);
        } while (previousSong == randomSong);
        previousSong = randomSong;
        PlaySong(music[randomSong].name);
        yield return new WaitForSeconds(music[randomSong].clip.length + 1);
        StartCoroutine(PlayMusic());
    }
}
