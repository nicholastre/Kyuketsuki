using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIFade.instance.FadeFromBlack(0.5f);

        if (SceneManager.GetActiveScene().name == "badEndScene")
        {
            MusicController.instance.musicSource.loop = false;
            MusicController.instance.PlaySong(GameSongs.GameOverTheme);
        } else if (SceneManager.GetActiveScene().name == "trueEndScene" || 
            SceneManager.GetActiveScene().name == "goodEndScene")
        {
            MusicController.instance.PlaySong(GameSongs.EndingTheme);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SwitchLoop()
    {
        MusicController.instance.musicSource.Stop();
        MusicController.instance.musicSource.loop = true;
    }
}
