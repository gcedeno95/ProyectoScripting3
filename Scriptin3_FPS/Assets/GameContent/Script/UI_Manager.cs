using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject victoryPanel; 

    private float score;

    [SerializeField] UnityStandardAssets.Characters.FirstPerson.MouseLook precioso;

    private static UI_Manager instance;
    public static UI_Manager Instance
    {
        get => instance;
    }

    private void Awake()
    {
        instance = this;
        deathPanel.SetActive(false);
        victoryPanel.SetActive(false); 
    }

    public void UpdateAmmo(float ammo)
    {
        ammoText.text = "Ammo: " + ammo;
    }

    public void UpdateScore(float newScore)
    {
        score += newScore;
        scoreText.text = "Score: " + score;
    }
    
    public void showDeath()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void showVictory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0; 
    }

    public void UpdateHealth(float current, float max)
    {
        healthBar.fillAmount = current / max;
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void ResetLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
