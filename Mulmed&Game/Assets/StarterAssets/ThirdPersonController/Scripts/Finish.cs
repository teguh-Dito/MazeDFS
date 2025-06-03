using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject CubeFinish;
    // GameObject Character;
    private MazeLogic11 mazeLogic11;
    public ThirdPersonController thirdPersonController;
    public TimerSetting timerSetting;
    public BulletTarget2 bulletTarget2;
    public PauseMenu pauseMenu;
    public PlayerController playerController;
    public GameObject gameWinning;
    public AudioSource audioSource;
    public AudioClip BacksoundWinning;
    public AudioMain audioMusic;
    public GameObject Character;
    private GameManagerScript managerScript;
    public GameObject text;
    public GameObject text1;
    // private AudioMain audioMain;
    
    // Update is called once per frame
    void Update()
    {
        if (gameWinning.activeInHierarchy)
        {
            audioMusic.StopAudio();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                MainMenu();
            }
        }
        else
    {
        if (Cursor.visible)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    }
    public void gameOver()
    {
        gameWinning.SetActive(true);
        Debug.Log("Berubah");
        // timerset.Timer = 0;

    }
    public void TextAppear(){
        text.SetActive(true);
        Debug.Log("muncul");
    }
    public void Restart()
    {
        Debug.Log("Restart");
        thirdPersonController.falseMove = false;
        bulletTarget2.falseMove = false;
        pauseMenu.falsemOve = false;
        timerSetting.falseMove = false;
        playerController.falseMove = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu");
        thirdPersonController.falseMove = false;
        bulletTarget2.falseMove = false;
        pauseMenu.falsemOve = false;
        timerSetting.falseMove = false;
        playerController.falseMove = false;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        thirdPersonController.falseMove = false;
        bulletTarget2.falseMove = false;
        pauseMenu.falsemOve = false;
        timerSetting.falseMove = false;
        playerController.falseMove = false;
        Application.Quit();
        Debug.Log("The Game Already quit");
    }
     private void OnTriggerEnter(Collider other)
    {
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

        if (other.gameObject == Character && playerInventory.NumberOfDiamonds >= 3)
        {
            Debug.Log("Berhasil");
            gameWinning.SetActive(true);
            text1.SetActive(true);
            thirdPersonController.falseMove = true;
            bulletTarget2.falseMove = true;
            pauseMenu.falsemOve = true;
            timerSetting.falseMove = true;
            timerSetting.s = 0;
            playerController.falseMove = true;
            audioSource.clip = BacksoundWinning;
            audioSource.Play();
            
        }
    }
}
