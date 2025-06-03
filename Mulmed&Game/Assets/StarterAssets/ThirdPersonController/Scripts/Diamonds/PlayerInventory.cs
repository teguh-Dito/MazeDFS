using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds {get; private set;}
    public GameObject specialObject; // Referensi ke objek yang ingin ditampilkan
    private bool hasWon = false;
    public UnityEvent<PlayerInventory> OnDiamondCollected;
    public AudioClip ClipKey;
    AudioSource SourceKey;
    public GameManagerScript gameManager;
    void Start()
    {
        SourceKey = gameObject.GetComponent<AudioSource>(); // Add this line
    }
    public void DiamondCollected(){
        NumberOfDiamonds++;

        if(NumberOfDiamonds > 0){
            SourceKey.clip = ClipKey;
            SourceKey.Play();
        }
        if (NumberOfDiamonds == 3)
        {    
            specialObject.SetActive(true); // Aktifkan objek
            Invoke("HideSpecialObject", 5f); // Nonaktifkan objek setelah 5 detik
        }
        // Check if the player has collected 10 diamonds
        if (NumberOfDiamonds >= 3 && !hasWon)
        {
            hasWon = true;
            Debug.Log("Kamu Menang"); // Display victory message
            // gameManager.gameOver();
        }
        OnDiamondCollected.Invoke(this);
    }
    void HideSpecialObject()
    {
        specialObject.SetActive(false); // Nonaktifkan objek
    }
    public void ResetWinningState()
    {
        hasWon = false;
    }
}
