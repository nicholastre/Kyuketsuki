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
        int tempMaster = PlayerPrefs.GetInt("MasterVolume");
        int tempMusic = PlayerPrefs.GetInt("MusicVolume");
        int tempSFX = PlayerPrefs.GetInt("SFXVolume");

        masterValue = tempMaster;
        musicValue = tempMusic;
        sfxValue = tempSFX;

        masterSlider.value = masterValue;
        musicSlider.value = musicValue;
        sfxSlider.value = sfxValue;
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
