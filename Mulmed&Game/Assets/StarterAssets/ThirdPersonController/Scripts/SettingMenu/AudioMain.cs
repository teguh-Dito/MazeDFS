using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class AudioMain : MonoBehaviour
{
    private AudioSource audioSource;
    private TimerSetting timer;
    private ThirdPersonController personControl;
    private Finish finish;
    void Start()
    {
        // Dapatkan komponen AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
        finish = GetComponent<Finish>();

        // Dapatkan referensi ke TimerSetting dan ThirdPersonController
        timer = FindObjectOfType<TimerSetting>();
        personControl = FindObjectOfType<ThirdPersonController>();

        // Mulai memutar musik
        // audioSource.Play();

        // Atur musik untuk berulang
        // audioSource.loop = true;
    }
    public void StopAudio(){
        audioSource.Stop();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // audioSource.Stop();
            StopAudio();
        }
        if (timer.ActiveGame && timer.Timer <= 0)
        {
            audioSource.Stop();
        }
        if (personControl.hitPoints <= 0)
        {
            audioSource.Stop();
        }if(finish.gameWinning.activeInHierarchy){
            audioSource.Stop();
        }
        
    }
}
