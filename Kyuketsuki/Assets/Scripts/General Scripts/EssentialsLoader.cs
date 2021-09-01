using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour {

    // Variavel para controlar a partir de qual cena adicionar o jogador
    public bool shouldLoadPlayer;

    public GameObject player;
    public GameObject UIScreen;
    public GameObject gameMan;
    public GameObject musicMan;

	// Use this for initialization
	void Start () {
		if(UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }

        if(shouldLoadPlayer && PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        
        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameMan).GetComponent<GameManager>();
        }

       if (MusicController.instance == null)
        {
            MusicController.instance = Instantiate(musicMan).GetComponent<MusicController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
