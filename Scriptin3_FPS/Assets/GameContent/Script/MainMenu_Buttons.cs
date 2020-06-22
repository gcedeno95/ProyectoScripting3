using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu_Buttons : MonoBehaviour
{

    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject optionsMenu;
    float maxTimer = 1;
    float timer;
    bool loading;

    private void Start()
    {
        loadingScreen.SetActive(false);
        optionsMenu.SetActive(false);
        timer = maxTimer;
    }

    public void Play()
    {
        Scene level = SceneManager.GetSceneByName("Level");
        loading = true;
        loadingScreen.SetActive(true);
    }

    private void Update()
    {
        if (loading)
        {
            if (timer <= 0)
                SceneManager.LoadSceneAsync(1);
            else
                timer -= Time.deltaTime;
        }
    }

    public void ShowOptions(bool value)
    {
        optionsMenu.SetActive(value);
    }

    public void Exit()
    {
        Application.Quit();
            
    }
}
