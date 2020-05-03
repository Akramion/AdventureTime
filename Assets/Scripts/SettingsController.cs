using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
    public AudioMixer audioMixer;

    private bool buttonToggle = false;


    public void ChangeVolume(float volume) {
        audioMixer.SetFloat("MasterVol", Mathf.Log(volume) * 20);
    }

    public void ChangeVolumeMusic(float volume) {
        audioMixer.SetFloat("MusicVol", Mathf.Log(volume) * 20);
    }
}
