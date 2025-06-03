using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameplayLogic : MonoBehaviour
{
    public UnityEngine.UI.Image HealthBar;
    public Text HealthText;
    public GameObject PanelGameResult;
    public Text GameResultText;

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        HealthBar.fillAmount = CurrentHealth/MaxHealth;
        HealthText.text = CurrentHealth.ToString();

        if (CurrentHealth <= 0) GameResult(false);
    }

    public void GameResult(bool win){
        PanelGameResult.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible= true;
        if (win)
        {
            GameResultText.color = Color.green;
            GameResultText.text = "Mission Complete!";
        }else
        {
            GameResultText.color = Color.red;
            GameResultText.text = "Game Over";
        }
    }

    public void GameResultDecision(bool TryAgain){
        if (TryAgain) SceneManager.LoadScene("Maze11");
        else SceneManager.LoadScene("MainMenu2");

    }

    // Start is called before the first frame update
    void Start()
    {
        // rb = this.GetComponent<Rigidbody>();
        // PlayerAudio = 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
