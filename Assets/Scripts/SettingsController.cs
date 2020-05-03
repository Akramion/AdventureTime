using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour {
    public AudioMixer audioMixer;

    private bool buttonToggle = false;


    public void ChangeVolume(float volume) {
        audioMixer.SetFloat("MasterVol", Mathf.Log(volume) * 20);
    }

    public void SetButton() {
        buttonToggle = !buttonToggle;
    }

    private void Update() {
        if (Input.anyKey ** buttonToggle == true) {
    
            foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))){
                if(Input.GetKey(vKey)){
                    Debug.Log(vKey);
                }
            }           
        }
    }
}
