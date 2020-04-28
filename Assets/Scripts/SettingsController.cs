using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour {
    public AudioMixer audioMixer;

    public void ChangeVolume(float volume) {
        audioMixer.SetFloat("MasterVol", Mathf.Log(volume) * 20);
    }
}
