using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScene : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource ClickButton;
    public ThirdPersonController thirdPersonController;
    public TimerSetting timerSetting;
    public BulletTarget2 bulletTarget2;
    public PauseMenu pauseMenu;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        // Dapatkan komponen AudioSource dari GameObject ini
        audioSource = GetComponent<AudioSource>();
        ClickButton = GetComponent<AudioSource>();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Mulai memutar musik
        // audioSource.Play();

        // Atur musik untuk berulang
        // audioSource.loop = true;
    }
    public void SkipYesTutorial()
    {
        // SceneManager.LoadScene("MazeKlmpk");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioSource.Stop();
        thirdPersonController.falseMove = true;
        bulletTarget2.falseMove = true;
        pauseMenu.falsemOve = true;
        timerSetting.falseMove = true;
        playerController.falseMove = true;

    }
    public void NoSkipTutorial(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        audioSource.Stop();
    }
    public void SceneTutorial()
    {
        // SceneManager.LoadScene("MazeKlmpk");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        audioSource.Stop();
        thirdPersonController.falseMove = true;
        bulletTarget2.falseMove = true;
        pauseMenu.falsemOve = true;
        timerSetting.falseMove = true;
        playerController.falseMove = true;

    }
    public void Click_Button(){
        ClickButton.Play();
    }
    public void QuitGame(){
        Debug.Log("Quit");
        // audioSource.Stop(); // Hentikan musik saat QuitGame dijalankan
        Application.Quit(); 
    }
}
