﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;


	// Use this for initialization
	void Start () {
        instance = this;

        DontDestroyOnLoad(gameObject);

	}
	
	// Update is called once per frame
	void Update () {
        //Verifica os booleanos para travar o jogador
        if(gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }
}
