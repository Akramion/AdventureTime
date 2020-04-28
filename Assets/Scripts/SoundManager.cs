using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

    bool isHear = false;
    private void Awake() {
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayOneShot(string name) {
        if(isHear == false) {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.loop = true;
            s.source.Play();
            isHear = true;
        }
    }

    public void StopSound(string name) {
        isHear = false;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
