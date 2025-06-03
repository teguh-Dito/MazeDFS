using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;
    public TimerSetting timerset;
    private bool GameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
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
            if(Input.GetKeyDown(KeyCode.N))
            {
                QuitGame();
            }
             if (Input.GetKeyDown(KeyCode.P))
            {
                Pause();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                Continue();
            }
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void gameOver()
    {
        gameOverUI.SetActive(true);
        // timerset.Timer = 0;

    }
    public void Restart()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Debug.Log("Main Menu");
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("The Game Already quit");
    }
    public void Pause()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Continue()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
}
