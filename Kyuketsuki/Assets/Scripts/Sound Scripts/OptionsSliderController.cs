using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliderController : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private int masterValue;
    private int musicValue;
    private int sfxValue;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider.value = PlayerPrefs.GetInt("MasterVolume");
        musicSlider.value = PlayerPrefs.GetInt("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetInt("SFXVolume");

        masterValue = (int)masterSlider.value;
        musicValue = (int)musicSlider.value;
        sfxValue = (int)sfxSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMasterSlider()
    {
        PlayerPrefs.SetInt("MasterVolume", (int)masterSlider.value);
        masterValue = (int)masterSlider.value;
        MusicController.instance.UpdateMixerVolumes(masterValue, musicValue, sfxValue);
    }

    public void UpdateMusicSlider()
    {
        PlayerPrefs.SetInt("MusicVolume", (int)musicSlider.value);
        musicValue = (int)musicSlider.value;
        MusicController.instance.UpdateMixerVolumes(masterValue, musicValue, sfxValue);
    }

    public void UpdateSFXSlider()
    {
        PlayerPrefs.SetInt("SFXVolume", (int)sfxSlider.value);
        sfxValue = (int)sfxSlider.value;
        MusicController.instance.UpdateMixerVolumes(masterValue, musicValue, sfxValue);
    }
}
