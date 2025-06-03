using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class TimerSetting : MonoBehaviour
{
    public Text TextTimer;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public GameObject BubbleTextBoss;
    public float Timer = 600; //01:30
    public float s;
    public bool ActiveGame = true;
    public GameObject GameOver, enemy, GameResult;
    public Transform target;
    private ThirdPersonController personControl;
    public bool falseMove = false;

    void setText()
    {
        int Minutes = Mathf.FloorToInt(Timer / 60); // 01
        int Seconds = Mathf.FloorToInt(Timer % 60); // 30
        TextTimer.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    void Start()
    {
        personControl = FindObjectOfType<ThirdPersonController>();
        audioSource1 = gameObject.AddComponent<AudioSource>(); // Add this line
        audioSource2 = gameObject.AddComponent<AudioSource>(); // Add this line
        audioSource3 = gameObject.AddComponent<AudioSource>(); // Add this line
    }

    // Update is called once per frame
    void Update()
    {
        setText();

        if (ActiveGame)
        {
            if(!falseMove){
                s += Time.deltaTime;
                if (s >= 1)
                {
                    Timer--;
                    s = 0;

                    

                    if (Mathf.FloorToInt(Timer / 60) == 9 && Mathf.FloorToInt(Timer % 60) == 55)
                    {
                        Debug.Log("Masuk audio 1");
                        audioSource1.clip = audioClip1;
                        audioSource1.Play();
                    }
                    // Check if it's the 6th minute and turn on the second audio source
                    // if (Mathf.FloorToInt(Timer / 60) == 6)
                    // {
                    // if (Mathf.FloorToInt(Timer / 60) == 6 && Mathf.FloorToInt(Timer % 60) == 45)
                    // {
                    //     Debug.Log("Masuk Audio 2");
                    //     audioSource2.clip = audioClip2;
                    //     audioSource2.Play();
                    // }

                    int minutes = Mathf.FloorToInt(Timer / 60);
                    int seconds = Mathf.FloorToInt(Timer % 60);

                    if (minutes == 6 && seconds >= 30 && seconds <= 45)
                    {
                        Debug.Log("Masuk Audio 2");
                        audioSource2.clip = audioClip2;
                        audioSource2.Play();
                    }

                    // Check if it's the 4th minute and turn on the first audio source
                    if (Mathf.FloorToInt(Timer / 60) == 4)
                    {
                        Debug.Log("Masuk Audio 3");
                        audioSource3.clip = audioClip3;
                        audioSource3.Play();
                    }
                    if (Mathf.FloorToInt(Timer / 60) == 9 && Mathf.FloorToInt(Timer % 60) == 45)
                    {
                        Debug.Log("Masuk Bubble Text Boss Tidak dikenali");
                        BubbleTextBoss.SetActive(true);
                        Invoke("HideBubbleTextBoss", 5f);  
                    }
                    if (Mathf.FloorToInt(Timer / 60) == 9 && Mathf.FloorToInt(Timer % 60) == 40)
                    {
                        Debug.Log("Player Terkejut");
                        audioSource4.clip = audioClip4;
                        audioSource4.Play();
                    }
                }
            }
            if (ActiveGame && Timer <= 0)
            {
                Debug.Log("You Lose");
                ActiveGame = false;
                GameOver.SetActive(true);
                // GameResult.SetActive(true);
                enemy.GetComponent<BulletTarget2>().TakeDamage(100f);
                Destroy(enemy, 3f);
                target.GetComponent<ThirdPersonController>().PlayerGetHit(100f);
            }

            if (personControl.hitPoints <= 0)
            {
                s = 0;
            }
        }
            
    }

    void HideBubbleTextBoss()
        {
            BubbleTextBoss.SetActive(false); // Nonaktifkan objek
        }
}
