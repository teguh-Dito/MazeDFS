using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip Alhamdulillah;
    private void OnTriggerEnter(Collider other) {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.DiamondCollected();
            gameObject.SetActive(false);
            audioSource.clip = Alhamdulillah;
            audioSource.Play();
        }
    }
}
