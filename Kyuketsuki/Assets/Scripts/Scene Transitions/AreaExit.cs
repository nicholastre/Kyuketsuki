using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance theEntrance;

    public bool shouldFadeFromBlack;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;

    // Use this for initialization
    void Start()
    {
        if (theEntrance != null)
        {
            theEntrance.transitionName = areaTransitionName;
        }

        if (shouldFadeFromBlack)
        {
            UIFade.instance.FadeFromBlack(0.5f);
        }

        if (SceneManager.GetActiveScene().name == "overworldMap" || 
            SceneManager.GetActiveScene().name == "testemapa")
        {
            MusicController.instance.PlaySong(GameSongs.CityOverWorldTheme);
        } else if (SceneManager.GetActiveScene().name == "forestMap")
        {
            MusicController.instance.PlaySong(GameSongs.ForestTheme);
        } else if (SceneManager.GetActiveScene().name == "mineMap")
        {
            MusicController.instance.PlaySong(GameSongs.MineTheme);
        } else if (SceneManager.GetActiveScene().name == "monasteryMap")
        {
            MusicController.instance.PlaySong(GameSongs.ChurchTheme);
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //SceneManager.LoadScene(areaToLoad);
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;

            UIFade.instance.FadeToBlack(1f);
            PlayerController.instance.areaTransitionName = areaTransitionName;

            PlayerController.instance.canMove = false;
            PlayerController.instance.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
