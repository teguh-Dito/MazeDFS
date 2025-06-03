using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StarterAssets
{
public class UIGameplayLogic1 : MonoBehaviour
{
    public UnityEngine.UI.Image HealthBar;
    public Text HealthText;

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        HealthBar.fillAmount = CurrentHealth/MaxHealth;
        HealthText.text = CurrentHealth.ToString();
        Debug.Log("Health Berkurang");
    }
}
}
