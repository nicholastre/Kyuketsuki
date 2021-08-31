using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum GameSongs
{
    MainTheme = 0,
    CityOverWorldTheme = 1,
    ForestTheme = 2,
    MineTheme = 3,
    ChurchTheme = 4,
    BattleTheme = 5,
    BossTheme = 6,
    GameOverTheme = 7,
    EndingTheme = 8
}

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    public AudioSource musicSource;
    public AudioMixerGroup masterMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup sfxMixer;
    public AudioClip[] availableMusic;

    private int currentMusic = 0;
    private float minVolume = -60.0f;
    private float maxVolume = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        int masterVolume, musicVolume, sfxVolume;
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetInt("MasterVolume");
        } else
        {
            masterVolume = 80;
            PlayerPrefs.SetInt("MasterVolume", masterVolume);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetInt("MusicVolume");
        }
        else
        {
            musicVolume = 60;
            PlayerPrefs.SetInt("MusicVolume", musicVolume);
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxVolume = PlayerPrefs.GetInt("SFXVolume");
            PlayerPrefs.SetInt("SFXVolume", sfxVolume);
        }
        else
        {
            sfxVolume = 40;
        }

        UpdateMixerVolumes(masterVolume, musicVolume, sfxVolume);
        musicSource.clip = availableMusic[(int)GameSongs.MainTheme];
        musicSource.Play();
        currentMusic = 0;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMixerVolumes(int masterVolume, int musicVolume, int sfxVolume)
    {
        float mappedMaster = MapToVolume(masterVolume);
        float mappedMusic = MapToVolume(musicVolume);
        float mappedSFX = MapToVolume(sfxVolume);

        masterMixer.audioMixer.SetFloat("MasterVolume", mappedMaster);
        musicMixer.audioMixer.SetFloat("MusicVolume", mappedMusic);
        sfxMixer.audioMixer.SetFloat("SFXVolume", mappedSFX);
    }

    public void PlaySong(GameSongs song)
    {
        if ((int)song != currentMusic)
        {
            musicSource.Stop();
            musicSource.clip = availableMusic[(int)song];
            musicSource.Play();
            currentMusic = (int)song;
        }
    }

    private float MapToVolume(float sliderVolume)
    {
        return minVolume + (sliderVolume - 0.0f) * (maxVolume - minVolume) / (100.0f - 0.0f);
    }

}
