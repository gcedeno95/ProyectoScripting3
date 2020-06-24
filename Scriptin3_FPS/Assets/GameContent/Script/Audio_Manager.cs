using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{

    [SerializeField] AudioSource[] audios;
    [SerializeField] float musicVolume = 0.5f;
    [SerializeField] float sfxVolume = 0.5f;

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);//
    }

    // Start is called before the first frame update
    void Start()
    {
        audios = GameObject.FindObjectsOfType<AudioSource>();
        SetVolumes();
    }

    private void OnLevelWasLoaded(int level)
    {
        audios = GameObject.FindObjectsOfType<AudioSource>();
        SetVolumes();
    }

    private void SetVolumes()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i].gameObject.GetComponent<VerySimplePistol>())
            {
                audios[i].volume = sfxVolume;
            }
            else
            {
                audios[i].volume = musicVolume;
            }
        }
    }

    public void SetMusicVolume(Slider volumeSlider)
    {
        musicVolume = volumeSlider.value;
        SetVolumes();
    }

    public void SetSfxVolume(Slider volumeSlider)
    {
        sfxVolume = volumeSlider.value;
        SetVolumes();
    }
}
