using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    string masterVolumeKey = "MasterVolume";
    string musicVolumeKey = "MusicVolume";
    string sfxVolumeKey = "SFXVolume";

    public void Start()
    {
        // Set initial slider values based on saved PlayerPrefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat(masterVolumeKey, 0.5f);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(musicVolumeKey, 0.5f);
        sfxVolumeSlider.value = PlayerPrefs.GetFloat(sfxVolumeKey, 0.5f);

        // Apply volume settings immediately on start
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        SetVolume(masterVolumeKey, masterVolumeSlider.value);
        PlayerPrefs.SetFloat(masterVolumeKey, masterVolumeSlider.value);
    }

    public void SetMusicVolume()
    {
        SetVolume(musicVolumeKey, musicVolumeSlider.value);
        PlayerPrefs.SetFloat(musicVolumeKey, musicVolumeSlider.value);
    }

    public void SetSFXVolume()
    {
        SetVolume(sfxVolumeKey, sfxVolumeSlider.value);
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolumeSlider.value);
    }
    
    public void SetVolume(string groupName, float value)
    {
        // Prevent value 0 from causing issues with Log10
        if (value == 0)
        {
            audioMixer.SetFloat(groupName, -80f); // Set to a low value (effectively silence)
        }
        else
        {
            float adjustedVolume = Mathf.Log10(value) * 20f;
            audioMixer.SetFloat(groupName, adjustedVolume);
        }
    }
}
