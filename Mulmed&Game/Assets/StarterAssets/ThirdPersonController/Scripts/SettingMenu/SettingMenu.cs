using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float volume){
        // Debug.Log(volume);
        audioMixer.SetFloat("volume1", volume);
    }
    public void SetVolumeSFX(float volume){
        audioMixer.SetFloat("volume2", volume);
    }
}
