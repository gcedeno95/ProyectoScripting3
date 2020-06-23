using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu_Buttons : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionDropdown; 
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject optionsMenu;
    float maxTimer = 1;
    float timer;
    bool loading;
    Resolution[] resolutions; 

    private void Start()
    {
        loadingScreen.SetActive(false);
        optionsMenu.SetActive(false);
        timer = maxTimer;

        resolutions=Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0; 

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height;
            options.Add(option); 
            if(resolutions[i].width==Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue(); 
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

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); 
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex); 
    }
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 
    }
}
